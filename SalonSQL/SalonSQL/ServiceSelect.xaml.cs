using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace SalonSQL
{
    public partial class ServiceSelect : Window
    {
        public static string conString { get; set; }
        public class Service
        {
            public string Service_name { get; set; }

        }

        const string checkboxErrorMessage = "Ошибка. Должна быть выбрана хотя бы одна услуга";

        public int count;

        public int CurrentAppID;

        public string CalculatedPrice;

        public static int Current_Master_id { get; set; }

        List<Service> services = new List<Service>();

        //Список услуг
        public CheckBox[,] checkboxestemp = new CheckBox[11, 2];

        //Метод для выполнения хранимой процедуры получения списка услуг
        public void GetServices()
        {
            string cmdString = "GetMastersServices";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Current_Master_id
                };
                cmd.Parameters.Add(idParam);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Service st = new Service();
                    st.Service_name = reader[0].ToString();
                    services.Add(st);
                }
                reader.Close();
                count = services.Count();
            }
        }

        //Метод для выполнения хранимой процедуры для получения ID заказа
        public void GetCurrentAppId()
        {
            string cmdString = "GetAppID";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                CurrentAppID = (int)cmd.ExecuteScalar();
                con.Close();
            }
        }

        //Метод для отрисовки чекбоксов
        public void MakeCheckboxes()
        {

            GetServices();

            for (var j = 0; j < services.Count(); j++)
            {
                ServiceText.Text = ServiceText.Text + services[j].Service_name + "\n\n";
            }

            CheckBox[,] checkboxes = new CheckBox[services.Count() + 1, 3];
            for (var i = 0; i < services.Count(); i++)
            {
                checkboxes[i, 2] = new CheckBox();
                Grid.SetColumn(checkboxes[i, 2], 2);
                Grid.SetRow(checkboxes[i, 2], i);
                Field.Children.Add(checkboxes[i, 2]);
            }

            checkboxestemp = checkboxes;
        }

        //Метод для выполнения хранимой процедуры для получения ID услуги
        public int GetServiceId(string name)
        {
            string cmdString = "GetServiceID";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = name
                };
                cmd.Parameters.Add(nameParam);

                con.Open();
                int id = (int)cmd.ExecuteScalar();
                con.Close();
                return id;
            }
        }

        //Метод для выполнения хранимой процедуры для получения услуг мастера
        public void AddService(int id)
        {
            string cmdString = "AddService";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter masterIDParam = new SqlParameter
                {
                    ParameterName = "@masterID",
                    Value = Current_Master_id
                };
                cmd.Parameters.Add(masterIDParam);

                SqlParameter serviceIDParam = new SqlParameter
                {
                    ParameterName = "@serviceID",
                    Value = id
                };
                cmd.Parameters.Add(serviceIDParam);

                SqlParameter appIDParam = new SqlParameter
                {
                    ParameterName = "@appID",
                    Value = CurrentAppID
                };
                cmd.Parameters.Add(appIDParam);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Метод для выполнения хранимой процедуры для получения цен услуг
        public string GetPrice()
        {
            string cmdString = "CountPrice";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = CurrentAppID
                };
                cmd.Parameters.Add(idParam);

                con.Open();
                string price = cmd.ExecuteScalar().ToString();
                con.Close();
                return price;
            }
        }

        //МЕтод для выполнения хранимой процедуры для добавления цены услуги
        public void AddPrice(string price)
        {
            string cmdString = "AddPrice";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter priceParam = new SqlParameter
                {
                    ParameterName = "@price",
                    Value = price
                };
                cmd.Parameters.Add(priceParam);

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = CurrentAppID
                };
                cmd.Parameters.Add(idParam);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public ServiceSelect()
        {
            InitializeComponent();

            //Отрисовываем чекбоксы
            MakeCheckboxes();
        }
        private void Service_Select_Button_Click(object sender, RoutedEventArgs e)
        {
            int uncheckedCount = 0;
            for (var i = 0; i < count; i++)
            {
                //Считаем неотмеченные чекбоксы
                if (checkboxestemp[i, 2].IsChecked == false)
                {
                    uncheckedCount++;
                }
            }

            //Если хотя мы один чекбокс отмечен
            if (uncheckedCount != count)
            {
                //Получаем ID записи
                GetCurrentAppId();
                //Передаём в окно чека
                Checkout.Current_App_id = CurrentAppID;

                //Для каждого отмеченного чекбокса
                for (var i = 0; i < count; i++)
                {
                    if (checkboxestemp[i, 2].IsChecked == true)
                    {
                        //Получаем ID услуги
                        int sID = GetServiceId(services[i].Service_name);
                        //ДОбавляем услугу в заказ
                        AddService(sID);
                        //Получаем цену, переводим в строку и обрезаем
                        CalculatedPrice = GetPrice();
                        CalculatedPrice = CalculatedPrice.Substring(0, CalculatedPrice.Length - 5);
                        //Добавляем цену в заказ
                        AddPrice(CalculatedPrice);
                    }
                }
                //Передаём в окно чека ID мастера
                Checkout.Current_Master_id = Current_Master_id;
                //Открываем окно чека
                Checkout checkout1 = new Checkout();
                checkout1.Show();
                Close();
            }
            else //Если ни один чекбокс не отмечен
            {
                MessageBox.Show(checkboxErrorMessage);
            }
        }
    }
}
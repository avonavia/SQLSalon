using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;
using System.IO;
using System.Text;
using System.Data;
using Microsoft.Win32;

namespace SalonSQL
{
    public partial class Checkout : Window
    {
        public static int Current_App_id { get; set; }
        public static int Current_Master_id { get; set; }

        public static string conString { get; set; }
        public Checkout()
        {
            InitializeComponent();
            //Заполняем DataGRid
            FillDataGrid();
            //Заполняем Lable'ы
            FillLables();
        }

        public class ReceitInfo
        {
            public string Service_name { get; set; }
            public string Price { get; set; }
        }

        //Список услуг и цен
        List<ReceitInfo> info = new List<ReceitInfo>();

        //МЕтод для заполнения DataGrid
        public void FillDataGrid()
        {
            //Хранимая процедура получения списка услуг клиента
            string cmdString = "GetServiceNameFromApp";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Current_App_id
                };
                cmd.Parameters.Add(idParam);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ReceitInfo st = new ReceitInfo();
                    st.Service_name = reader[0].ToString();
                    st.Price = reader[1].ToString();
                    //Добавляем в список
                    info.Add(st);
                }
                reader.Close();
                ServicesGrid.ItemsSource = info;
            }
        }

        //Метод для выполнения хранимой процедуры для получения времени записи 
        public void GetTimeFromApp()
        {
            string cmdString = "GetTimeFromApp";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Current_App_id
                };
                cmd.Parameters.Add(idParam);

                con.Open();

                //Записываем в Lable
                TimeLabel.Content = cmd.ExecuteScalar().ToString();
                con.Close();
            }
        }

        //Метод для выполнения хранимой процедуры для получения цены заказа
        public void GetPriceFromApp()
        {
            string cmdString = "GetPriceFromApp";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Current_App_id
                };
                cmd.Parameters.Add(idParam);

                con.Open();

                //Записываем в Lable
                PriceLabel.Content = cmd.ExecuteScalar().ToString();
                con.Close();
            }
        }

        //Метод для выполнения хранимой процедуры для получения даты записи
        public void GetDateFromApp()
        {
            string cmdString = "GetDateFromApp";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Current_App_id
                };
                cmd.Parameters.Add(idParam);

                con.Open();

                //Записываем в Lable
                DateLabel.Content = cmd.ExecuteScalar().ToString();
                con.Close();
            }
        }

        //Метод для выполнения хранимой процедуры для получения фамилии мастера
        public void GetMasterSurname()
        {
            string cmdString = "GetMasterSurname";

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

                //Записываем в Lable
                MasterLabel.Content = MasterLabel.Content + cmd.ExecuteScalar().ToString() + " ";
                con.Close();
            }
        }

        //Метод для выполнения хранимой процедуры для получения имени мастера
        public void GetMasterName()
        {
            string cmdString = "GetMasterName";

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

                //Записываем в Lable
                MasterLabel.Content = MasterLabel.Content + cmd.ExecuteScalar().ToString() + " ";
                con.Close();
            }
        }

        //Метод для выполнения хранимой процедуры для получения отчества мастера
        public void GetMasterLName()
        {
            string cmdString = "GetMasterLName";

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

                //Записываем в Lable
                MasterLabel.Content = MasterLabel.Content + cmd.ExecuteScalar().ToString() + " ";
                con.Close();
            }
        }

        //Метод для выполнения хранимой процедуры для получения фото мастера
        public void GetMasterPicture()
        {
            string cmdString = "GetMasterPicture";

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

                //Отображаем картинку
                MasterPicture.Source = new BitmapImage(new Uri(cmd.ExecuteScalar().ToString(), UriKind.Relative));
                con.Close();
            }
        }

        //Метод для заполнения Lable'ов
        public void FillLables()
        {
            AppNOLabel.Content = Current_App_id;
            GetTimeFromApp();
            GetPriceFromApp();
            GetDateFromApp();
            GetMasterSurname();
            GetMasterName();
            GetMasterLName();
            GetMasterPicture();
        }

        //Кнопка назад
        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            //Открываем начальное окно
            MainWindow mainwindow1 = new MainWindow();
            mainwindow1.Show();
            Close();
        }

        //Кнопка сохранения чека
        private void GetReceitButton_Click(object sender, RoutedEventArgs e)
        {
            //Формируем чек и записываем в файл

            var dstEncoding = Encoding.UTF8;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            var desktopPath = openFileDialog.FileName;

            string TrimmedDate = DateLabel.Content.ToString().Substring(0, DateLabel.Content.ToString().Length - 8);

            string AppNO = "Номер заказа: " + AppNOLabel.Content.ToString() + "\n";
            string AppDate = "Дата: " + TrimmedDate + "\n";
            string AppTime = "Время: " + TimeLabel.Content.ToString() + "\n";
            string Master = "Мастер: " + MasterLabel.Content.ToString() + "\n";

            string AllInfo = AppNO + AppDate + AppTime + Master;

            try
            {
                var sr = new StreamWriter(desktopPath, append: false, encoding: dstEncoding);
                sr.WriteLine(AllInfo);
                sr.WriteLine("Услуги:");
                foreach (var item in info)
                {
                    sr.WriteLine(item.Service_name + ": " + item.Price + " руб.");
                }
                string fullPrice = "Итого: " + PriceLabel.Content.ToString() + " руб.";
                sr.WriteLine("\n" + fullPrice);
                sr.Close();
                Close();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка");
            }
        }
    }
}

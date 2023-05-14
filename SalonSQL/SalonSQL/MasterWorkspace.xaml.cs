using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace SalonSQL
{
    public partial class MasterWorkspace : Window
    {
        public static string conString { get; set; }
        public static int Master_id { get; set; }

        public static int CurrentID { get; set; }
        public MasterWorkspace()
        {
            InitializeComponent();
            GetClientsList();
        }

        public class ClientInfo
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Last_name { get; set; }
            public string Gender { get; set; }
            public string Date_ { get; set; }
            public string Time_ { get; set; }

        }

        //Список клиентов
        List<ClientInfo> info = new List<ClientInfo>();
        //Список завершённых заказов
        List<ClientInfo> finishedInfo = new List<ClientInfo>();

        //Метод для выполнения хранимой процедуры для получения списка клиентов мастера
        public void GetClientsList()
        {
            string cmdString = "GetClientsList";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Master_id
                };
                cmd.Parameters.Add(idParam);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClientInfo st = new ClientInfo();
                    st.ID = (int)reader[0];
                    st.Name = reader[1].ToString();
                    st.Surname = reader[2].ToString();
                    st.Last_name = reader[3].ToString();
                    st.Gender = reader[4].ToString();
                    st.Date_ = reader[5].ToString().Substring(0, reader[5].ToString().Length - 8);
                    st.Time_ = reader[6].ToString();
                    //Добавляем в список
                    info.Add(st);

                    //Сортируем список по дате и времени
                    info.Sort(delegate (ClientInfo inf1, ClientInfo inf2)
                    { 
                        return inf1.Time_.CompareTo(inf2.Time_); 
                    });
                    info.Sort(delegate (ClientInfo inf1, ClientInfo inf2)
                    {
                        return inf1.Date_.CompareTo(inf2.Date_);
                    });
                }
                reader.Close();
                Grid_.ItemsSource = info;
            }
        }

        //Метод для выполнения хранимой процедуры для получения списка завершённых заказов
        public void GetFinishedOrdersList()
        {
            string cmdString = "GetFinishedOrdersList";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Master_id
                };
                cmd.Parameters.Add(idParam);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClientInfo st = new ClientInfo();
                    st.ID = (int)reader[0];
                    st.Name = reader[1].ToString();
                    st.Surname = reader[2].ToString();
                    st.Last_name = reader[3].ToString();
                    st.Gender = reader[4].ToString();
                    st.Date_ = reader[5].ToString().Substring(0, reader[5].ToString().Length - 8);
                    st.Time_ = reader[6].ToString();
                    //Добавляем в список
                    finishedInfo.Add(st);

                    //Сортируем список по дате и времени
                    finishedInfo.Sort(delegate (ClientInfo inf1, ClientInfo inf2)
                    {
                        return inf1.Time_.CompareTo(inf2.Time_);
                    });
                    finishedInfo.Sort(delegate (ClientInfo inf1, ClientInfo inf2)
                    {
                        return inf1.Date_.CompareTo(inf2.Date_);
                    });
                }
                reader.Close();
                Grid_.ItemsSource = null;
                Grid_.ItemsSource = finishedInfo;
            }
        }

        public int Client_id;

        //Метод для выполнения хранимой процедуры для получени ID заказа
        public void GetAppIDFromClient()
        {
            string cmdString = "GetAppIDFromClient";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Client_id
                };
                cmd.Parameters.Add(idParam);
                con.Open();

                CurrentID = (int)cmd.ExecuteScalar();
                con.Close();
            }
        }

        //Метод для выполнения хранимой процедуры для изменения статуса заказа на "Выполнен"
        public void RemoveClient()
        {
            GetAppIDFromClient();

            string cmdString = "RemoveClient";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter clientIdParam = new SqlParameter
                {
                    ParameterName = "@client_id",
                    Value = Client_id
                };
                cmd.Parameters.Add(clientIdParam);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();

                info.Clear();
                Grid_.ItemsSource = null;

                GetClientsList();
            }
        }

        //Список услуг
        List<string> servicelist = new List<string>();

        //МЕтод для выполнения хранимой процедуры для получения названий услуг, которые выбрал клиент
        public void GetServiceNameFromClient()
        {
                string cmdString = "GetServiceNameFromClient";

                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand(cmdString, con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@id",
                        Value = Client_id
                    };
                    cmd.Parameters.Add(idParam);

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string servicename = reader[0].ToString();
                        servicelist.Add(servicename);
                    }
                    reader.Close();
                con.Close();
            }
        }

        //Кнопка смены статуса
        private void Delete_button_Click(object sender, RoutedEventArgs e)
        {
            //Если выбран заказ из списка
            if (Grid_.SelectedItem != null)
            {
                //Изменяем статус заказа
                RemoveClient();
                Grid_.SelectedItem = null;
            }
        }

        //Кнопка выхода из аккаунта
        private void Exit_button_Click(object sender, RoutedEventArgs e)
        {
            //Открываем начальное окно
            MainWindow mainWindow1 = new MainWindow();
            mainWindow1.Show();
            Close();
        }

        //Выбранный заказ поменялся
        private void Grid__SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            ClientInfo client = new ClientInfo();
            foreach (var obj in Grid_.SelectedItems)
            {
                client = obj as ClientInfo;
                //Получаем ID
                Client_id = client.ID;
            }
        }

        //Кнопка для отображения списка услуг
        private void Get_service_button_Click(object sender, RoutedEventArgs e)
        {
            //Если выбран заказ из списка
            if (Grid_.SelectedItem != null)
            {
                //Получаем список названий услуг
                GetServiceNameFromClient();
                string servicenames = string.Empty;
                //Формируем одну строку
                foreach (var item in servicelist)
                {
                    servicenames = servicenames + item + "\n";
                }
                //Показываем
                MessageBox.Show(servicenames);
                Grid_.SelectedItem = null;
                //Очижаем список услуг
                servicelist.Clear();
            }
        }

        //Чекбокс для отображения списка завершённых заказов отмечен
        private void SwitchOrdersList_Checked(object sender, RoutedEventArgs e)
        {
            //Очищаем список завершённых заказов
            finishedInfo.Clear();
            //Получаем новый список завершённых заказов
            GetFinishedOrdersList();
            //Скрываем кнопку смены статуса
            Delete_button.Visibility = Visibility.Hidden;
        }

        //Чекбокс для отображения списка завершённых заказов не отмечен
        private void SwitchOrdersList_Unchecked(object sender, RoutedEventArgs e)
        {
            //Очищаем список заказов
            info.Clear();
            //Получаем новый список заказов
            GetClientsList();
            //Показываем кнопку смены статуса
            Delete_button.Visibility = Visibility.Visible;
        }
    }
}

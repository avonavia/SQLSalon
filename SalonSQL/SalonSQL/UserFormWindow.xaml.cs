using System.Data.SqlClient;
using System.Windows;
using System.Data;

namespace SalonSQL
{
    public partial class UserFormWindow : Window
    {
        public static string conString { get; set; }
        public UserFormWindow()
        {
            InitializeComponent();
        }

        //Метод для выполнения хранимой процедуры добавления клиента
        public void AddNewClient()
        {
            string cmdString = "AddClient";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter surnameParam = new SqlParameter
                {
                    ParameterName = "@Surname",
                    Value = Surname_box.Text
                };
                cmd.Parameters.Add(surnameParam);

                SqlParameter firstnameParam = new SqlParameter
                {
                    ParameterName = "@First_name",
                    Value = First_name_box.Text
                };
                cmd.Parameters.Add(firstnameParam);

                SqlParameter lastnameParam = new SqlParameter
                {
                    ParameterName = "@Last_name",
                    Value = Last_name_box.Text
                };
                cmd.Parameters.Add(lastnameParam);

                SqlParameter genderParam = new SqlParameter
                {
                    ParameterName = "@Gender",
                    Value = Client_Gender
                };
                cmd.Parameters.Add(genderParam);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Метод для выполнения хранимой процедуры добавления новой записи
        public void AddCurrentClient()
        {
            string cmdString = "AddClientToApp";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Метод для выполнения хранимой процедуры получения ID клиента
        public int GetCurrentClient()
        {
            string cmdString = "SelectCurrentID";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                int id = (int)cmd.ExecuteScalar();
                con.Close();
                return id;
            }
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            //При нажатии кнопки Назад открываем начальное окно
            MainWindow MainWindow1 = new MainWindow();
            MainWindow1.Show();
            Close();
        }

        //Массив служебных символов
        char[] unacceptable = { '"', '/', '\\', ',', '.', ' ', '$', '*', '#', '№', '&', '!', '?', '(', ')', '{', '}', '[', ']', '`', '~' };

        string Client_Gender = "";

        //Текст ошибок
        const string usingUnacceptableCharachtersError = "Использвание служебных символов запрещено";
        const string emptyFieldsError = "Поля 'Имя' и 'Фамилия' должны быть заполнены";
        const string tooManySymbolsMessage = "Одно или несколько полей содержит более 50 символов (разрешённое количество символов - не более 50)";

        private void Enter_button_Click(object sender, RoutedEventArgs e)
        {
            //Если все поля заполнены
            if ((Client_Gender != "") && (Surname_box.Text != "") && (First_name_box.Text != ""))
            {
                //Добавляем нового клиента
                AddNewClient();
                Client_Gender = "";
                //Добавляем новую запись
                AddCurrentClient();
                Gender_button_m.IsChecked = false;
                Gender_button_zh.IsChecked = false;
                
                //Получаем ID клиента
                MasterSelectWindow.Current_id = GetCurrentClient();

                //Открываем окно выбора мастера
                MasterSelectWindow MasterSelectWindow1 = new MasterSelectWindow();
                MasterSelectWindow1.Show();
                Close();
            }
            else
            {
                MessageBox.Show(emptyFieldsError);
            }

            //Если в каком-то из полей слишком много символов
            if ((Surname_box.Text.Length > 50 || First_name_box.Text.Length > 50 || Last_name_box.Text.Length > 50))
            {
                MessageBox.Show(tooManySymbolsMessage);
                Client_Gender = null;
                Surname_box.Text = null;
                First_name_box.Text = null;
                Last_name_box.Text = null;
                Gender_button_m.IsChecked = false;
                Gender_button_zh.IsChecked = false;
            }

            //Проверяем наличие служебных символов
            for (var i = 0; i < unacceptable.Length; i++)
            {
                if (Surname_box.Text.Contains(unacceptable[i].ToString()) || First_name_box.Text.Contains(unacceptable[i].ToString()) || Last_name_box.Text.Contains(unacceptable[i].ToString()))
                {
                    MessageBox.Show(usingUnacceptableCharachtersError);
                    Client_Gender = null;
                    Surname_box.Text = null;
                    First_name_box.Text = null;
                    Last_name_box.Text = null;
                    Gender_button_m.IsChecked = false;
                    Gender_button_zh.IsChecked = false;
                }
            }
        }

        //Выбор пола клиента
        private void Gender_button_m_Checked(object sender, RoutedEventArgs e)
        {
            Client_Gender = "М";
        }

        private void Gender_button_zh_Checked(object sender, RoutedEventArgs e)
        {
            Client_Gender = "Ж";
        }
    }
}

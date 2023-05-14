using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace SalonSQL
{
    public partial class LogInWindow : Window
    {
        public static string conString { get; set; }
        public LogInWindow()
        {
            InitializeComponent();
        }

        const string wrongLoginPasswordError = "Неверный логин или пароль";

        //Метод для выполнения хранимой процедуры для проверки логина и пароля 
        public int CheckLoginExists()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string cmdString = "CheckLoginExists";
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter loginParam = new SqlParameter
                {
                    ParameterName = "@login",
                    Value = login_box.Text
                };
                cmd.Parameters.Add(loginParam);

                SqlParameter passwordParam = new SqlParameter
                {
                    ParameterName = "@password",
                    Value = password_box.Text
                };
                cmd.Parameters.Add(passwordParam);

                int result = (int)cmd.ExecuteScalar();

                con.Close();
                return result;
            }
        }

        //Метод для выполнения хранимой процедуры для получения  ID мастера по логину и паролю 
        public void GetMasterFromLogin()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string cmdString = "GetMasterFromLogin";
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter loginParam = new SqlParameter
                {
                    ParameterName = "@login",
                    Value = login_box.Text
                };
                cmd.Parameters.Add(loginParam);

                SqlParameter passwordParam = new SqlParameter
                {
                    ParameterName = "@password",
                    Value = password_box.Text
                };
                cmd.Parameters.Add(passwordParam);

                MasterWorkspace.Master_id = (int)cmd.ExecuteScalar();

                con.Close();
            }
        }

        //Кнопка ввести
        private void Enter_button_Click(object sender, RoutedEventArgs e)
        {
            //Если мастер с таким логином и паролем существует
            if (CheckLoginExists() == 1)
            {
                //Получаем его ID
                GetMasterFromLogin();
                //Открываем окно личного кабинета мастера
                MasterWorkspace masterWorkspace1 = new MasterWorkspace();
                masterWorkspace1.Show();
                Close();
            }
            else
            {
                MessageBox.Show(wrongLoginPasswordError);
            }
        }

        //Кнопка назад
        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            //ОТкрываем начальное окно
            MainWindow mainWindow1 = new MainWindow();
            mainWindow1.Show();
            Close();
        }
    }
}

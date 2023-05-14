using System;
using System.Windows;
using System.Data.SqlClient;
using System.Data;


namespace SalonSQL
{
    public partial class GetReceitWindow : Window
    {
        public static string conString { get; set; }
        public GetReceitWindow()
        {
            InitializeComponent();
        }

        //Кнопка назад
        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            //Открываем начальное окно
            MainWindow mainwindow1 = new MainWindow();
            mainwindow1.Show();
            Close();
        }

        //Метод для выполнения хранимой процедуры для проверки ID записи
        public int CheckAppID()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string cmdString = "CheckAppID";
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Convert.ToInt32(ReceitNumberBox.Text)
                };
                cmd.Parameters.Add(idParam);

                int result = (int)cmd.ExecuteScalar();

                con.Close();
                return result;
            }
        }

        //Метод для выполнения хранимой процедуры для получения ID мастера по номеру записи
        public void GetMasterFromApp()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string cmdString = "GetMasterFromApp";
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Convert.ToInt32(ReceitNumberBox.Text)
                };
                cmd.Parameters.Add(idParam);

                //Передаём в окно чека
                Checkout.Current_Master_id = (int)cmd.ExecuteScalar();

                con.Close();
            }
        }

        const string orderNotFoundError = "Заказ не найден";

        //Кнопка ввода номера заказа
        private void Enter_ReceitNO_Button_Click(object sender, RoutedEventArgs e)
        {
            //Если поле ввода не пустое и число
            if (ReceitNumberBox.Text != "" && int.TryParse(ReceitNumberBox.Text, out int result) == true)
            {
                //Если заказ с введённым номером существует
                if (CheckAppID() == 1)
                {
                    //Получаем ID мастера по номеру заказа
                    GetMasterFromApp();
                    //Передаём в окно чека
                    Checkout.Current_App_id = Convert.ToInt32(ReceitNumberBox.Text);
                    //Открываем окно чека
                    Checkout checkout1 = new Checkout();
                    checkout1.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show(orderNotFoundError);
                }
            }
        }
    }
}

using System.Windows;

namespace SalonSQL
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Передаём строку подключения во все окна
            UserFormWindow.conString = conString;
            LogInWindow.conString = conString;
            GetReceitWindow.conString = conString;
            TimeAndDateSelect.conString = conString;
            ServiceSelect.conString = conString;
            MasterWorkspace.conString = conString;
            MasterSelectWindow.conString = conString;
            Checkout.conString = conString;
        }

        //Строка подклюючения
        static string conString = @"Data Source=.\SQLEXPRESS; Initial Catalog=Salon; Integrated Security=true;";
        
        //Кнопка записи
        private void choose_button_Click(object sender, RoutedEventArgs e)
        {
            //Открываем окно записи
            UserFormWindow UserFormWindow1 = new UserFormWindow();
            UserFormWindow1.Show();
            Close();

        }

        //Кнопка входа в аккаунт
        private void login_button_Click(object sender, RoutedEventArgs e)
        {
            //Открываем окно входа в аккаунт
            LogInWindow LogInWindow1 = new LogInWindow();
            LogInWindow1.Show();
            Close();
        }

        //Кнопка получения чека
        private void GetReceitButton_Click(object sender, RoutedEventArgs e)
        {
            //Открываем окно чека
            GetReceitWindow getreceitwindow1 = new GetReceitWindow();
            getreceitwindow1.Show();
            Close();
        }
    }
}

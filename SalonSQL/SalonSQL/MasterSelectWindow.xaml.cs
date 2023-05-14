using System;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;
using System.Data;
using System.Collections.Generic;

namespace SalonSQL
{
    public partial class MasterSelectWindow : Window
    {
        public MasterSelectWindow()
        {
            InitializeComponent();
            //Добавляем элементы в список фото мастеров
            imglist.Add(image1);
            imglist.Add(image2);
            imglist.Add(image3);
            imglist.Add(image4);
            imglist.Add(image5);

            int i = 1;

            foreach(var item in imglist)
            {
                //Получаем картинки из БД
                GetMasterPicture(item, i);
                i++;
            }
        }
        public static string conString { get; set; }
        public static int Current_id { get; set; }

        int Master_id = 0;

        //Список фото мастеров
        List<System.Windows.Controls.Image> imglist = new List<System.Windows.Controls.Image>();

        //Метод для выполнения хранимой процедуры для получения фото мастеров
        public void GetMasterPicture(System.Windows.Controls.Image img, int masterID)
        {
            string cmdString = "GetMasterPicture";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = masterID
                };
                cmd.Parameters.Add(idParam);

                con.Open();

                img.Source = new BitmapImage(new Uri(cmd.ExecuteScalar().ToString(), UriKind.Relative));
                con.Close();
            }
        }

        //Нажатие кнопки выбора мастера
        private void Select_Master_button_Click(object sender, RoutedEventArgs e)
        {
            //Если мастер выбран
            if (Master_id != 0)
            {
                //Для каждого мастера передаём его ID в окно выбора даты и времени. Также передаём текущий ID 
                if (Master_id == 1)
                {
                    TimeAndDateSelect.Current_id = Current_id;
                    TimeAndDateSelect.Master_id = 1;
                    TimeAndDateSelect TimeAndDateSelectWindow_1 = new TimeAndDateSelect();
                    TimeAndDateSelectWindow_1.Show();
                }
                if (Master_id == 2)
                {
                    TimeAndDateSelect.Current_id = Current_id;
                    TimeAndDateSelect.Master_id = 2;
                    TimeAndDateSelect TimeAndDateSelectWindow_2 = new TimeAndDateSelect();
                    TimeAndDateSelectWindow_2.Show();
                }
                if (Master_id == 3)
                {
                    TimeAndDateSelect.Current_id = Current_id;
                    TimeAndDateSelect.Master_id = 3;
                    TimeAndDateSelect TimeAndDateSelectWindow_3 = new TimeAndDateSelect();
                    TimeAndDateSelectWindow_3.Show();
                }
                if (Master_id == 4)
                {
                    TimeAndDateSelect.Current_id = Current_id;
                    TimeAndDateSelect.Master_id = 4;
                    TimeAndDateSelect TimeAndDateSelectWindow_4 = new TimeAndDateSelect();
                    TimeAndDateSelectWindow_4.Show();
                }
                if (Master_id == 5)
                {
                    TimeAndDateSelect.Current_id = Current_id;
                    TimeAndDateSelect.Master_id = 5;
                    TimeAndDateSelect TimeAndDateSelectWindow_5 = new TimeAndDateSelect();
                    TimeAndDateSelectWindow_5.Show();
                }
                Close();
            }

        }

        private void SelectMaster1_Checked(object sender, RoutedEventArgs e)
        {
            Master_id = 1;
        }

        private void SelectMaster2_Checked(object sender, RoutedEventArgs e)
        {
            Master_id = 2;
        }

        private void SelectMaster3_Checked(object sender, RoutedEventArgs e)
        {
            Master_id = 3;
        }

        private void SelectMaster4_Checked(object sender, RoutedEventArgs e)
        {
            Master_id = 4;
        }

        private void SelectMaster5_Checked(object sender, RoutedEventArgs e)
        {
            Master_id = 5;
        }
    }
}
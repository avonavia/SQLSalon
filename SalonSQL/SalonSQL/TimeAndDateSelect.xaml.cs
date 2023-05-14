using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace SalonSQL
{
    public partial class TimeAndDateSelect : Window
    {
        public TimeAndDateSelect()
        {
            InitializeComponent();

            //Дата, с которой начинается отображение дат календаря
            Calendar.DisplayDateStart = DateTime.Today;
            //Дата, которой кончается отображение дат календаря
            Calendar.DisplayDateEnd = DateTime.Today.AddMonths(1);

            //Получаем рабочие дни мастера
            GetWorkingDays();

            //Зачёркиваем даты, в которые мастер не работает
            AddBlackoutDates();

            //Обновляем список времени записей
            TimeList.ItemsSource = timelist;
        }

        //Список времени
        List<TimeSpan> timelist = new List<TimeSpan>();

        //Список времени перерывов
        List<TimeSpan> breaktimelist = new List<TimeSpan>();

        //Список времени, на которое уже есть записи
        List<TimeSpan> occupiedtimelist = new List<TimeSpan>();

        //Список рабочих дней мастера
        List<int> workingdayslist = new List<int>();

        public static string conString { get; set; }

        public static int Current_id { get; set; }

        public static int Master_id { get; set; }

        //Метод для выполнения хранимой процедуры получения рабочих дней мастера
        public void GetWorkingDays()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                string cmdString = "GetWorkingDays";
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Master_id
                };
                cmd.Parameters.Add(idParam);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //Добавляем полученные в список
                    workingdayslist.Add((int)reader[0]);
                }
                reader.Close();

                con.Close();
            }
        }

        //МЕтод для зачёркивания дат в календаре
        public void AddBlackoutDates()
        {
            //День, с которого начнём проверку на рабочие дни
            DateTime dayStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            
            //Пока с начального дня не прошёл месяц, проверяем каждый день
            while (dayStart.Month == Calendar.DisplayDateStart.Value.Month)
            {
                //Если этот день не содержится в списке рабочих - зачёркиваем
                if (!workingdayslist.Contains((int)dayStart.DayOfWeek))
                {
                    Calendar.BlackoutDates.Add(new CalendarDateRange(dayStart));
                }
                //Переходим к следующему дню
                dayStart = dayStart.AddDays(1);
            }

            //День, на котором заканчивается проверка на рабочие дни
            DateTime dayEnd = new DateTime(DateTime.Today.Year, DateTime.Today.AddMonths(1).Month, 1);

            // Пока с конечного дня не прошёл месяц, проверяем каждый день
            while (dayEnd.Month == Calendar.DisplayDateEnd.Value.Month)
            {
                //Если этот день не содержится в списке рабочих - зачёркиваем
                if (!workingdayslist.Contains((int)dayEnd.DayOfWeek))
                {
                    Calendar.BlackoutDates.Add(new CalendarDateRange(dayEnd));
                }
                //Переходим к следующему дню
                dayEnd = dayEnd.AddDays(1);
            }
        }

        //Метод для получения часов, в которые мастер работает
        public void GetWorkingHours(DateTime startDate, DateTime endDate)
        {
            //Очищаем список времени
            timelist.Clear();
            var i = 0;
            DateTime start = startDate;
            DateTime next = start.AddHours(1);

            //Пока не проверили все даты
            while (next <= endDate)
            {
                //Если время содержится в списке перерывов или в списке занятых - пропускаем
                if (breaktimelist.Contains(start.TimeOfDay) || occupiedtimelist.Contains(start.TimeOfDay))
                {
                    i++;
                    start = next;
                    next = start.AddHours(1);
                }
                //Иначе добавляем в список времени
                else
                {
                    timelist.Add(start.TimeOfDay);
                    i++;
                    start = next;
                    next = start.AddHours(1);
                }
            }
        }

        //Метод для получения времени перерывов
        public void GetNotWorkingHours(DateTime startDate, DateTime endDate)
        {
            //Очищаем список времени перерывов
            breaktimelist.Clear();
            var i = 0;
            DateTime start = startDate;
            DateTime next = start.AddHours(1);
            //Заполняем список
            while (next <= endDate)
            {
                breaktimelist.Add(start.TimeOfDay);
                i++;
                start = next;
                next = start.AddHours(1);
            }
        }

        //Метод для выполнения хранимой процедуры получения начала рабочего дня мастера
        public DateTime GetStartTime(DayOfWeek day)
        {
            string cmdString = "GetStartTime";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Master_id
                };
                cmd.Parameters.Add(idParam);

                SqlParameter dayParam = new SqlParameter
                {
                    ParameterName = "@day",
                    Value = day
                };
                cmd.Parameters.Add(dayParam);

                var result = DateTime.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
                return result;
            }
        }

        //Метод для выполнения хранимой процедуры получения конца рабочего дня мастера
        public DateTime GetEndTime(DayOfWeek day)
        {
            string cmdString = "GetEndTime";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Master_id
                };
                cmd.Parameters.Add(idParam);

                SqlParameter dayParam = new SqlParameter
                {
                    ParameterName = "@day",
                    Value = day
                };
                cmd.Parameters.Add(dayParam);

                var result = DateTime.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
                return result;
            }
        }

        //Метод для выполнения хранимой процедуры получения начала перерыва мастера
        public DateTime GetBreakStartTime(DayOfWeek day)
        {
            string cmdString = "GetBreakStartTime";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Master_id
                };
                cmd.Parameters.Add(idParam);

                SqlParameter dayParam = new SqlParameter
                {
                    ParameterName = "@day",
                    Value = day
                };
                cmd.Parameters.Add(dayParam);

                var result = DateTime.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
                return result;
            }
        }

        //Метод для выполнения хранимой процедуры получения конца перерыва мастера
        public DateTime GetBreakEndTime(DayOfWeek day)
        {
            string cmdString = "GetBreakEndTime";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Master_id
                };
                cmd.Parameters.Add(idParam);

                SqlParameter dayParam = new SqlParameter
                {
                    ParameterName = "@day",
                    Value = day
                };
                cmd.Parameters.Add(dayParam);

                var result = DateTime.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
                return result;
            }
        }

        //Метод для выполнения хранимой процедуры получения занятого времени мастера
        public void GetOccupiedTime(DateTime date)
        {
            //Очищаем список занятого времени
            occupiedtimelist.Clear();
            var date_ = date.Date;
            string cmdString = "GetOccupiedTime";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Master_id
                };
                cmd.Parameters.Add(idParam);

                SqlParameter dateParam = new SqlParameter
                {
                    ParameterName = "@date",
                    Value = date
                };
                cmd.Parameters.Add(dateParam);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //Добавляем время в список
                    occupiedtimelist.Add(DateTime.Parse(reader[0].ToString()).TimeOfDay);
                }
                reader.Close();

                con.Close();
            }
        }

        //МЕтод для выполнения хранимой процедуры добавления даты и времен записи
        public void AddAppDateTime(string date, string time)
        {
            string cmdString = "AddAppointmentDateTime";

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter dateParam = new SqlParameter
                {
                    ParameterName = "@date_",
                    Value = date
                };
                cmd.Parameters.Add(dateParam);

                SqlParameter timeParam = new SqlParameter
                {
                    ParameterName = "@time_",
                    Value = time
                };
                cmd.Parameters.Add(timeParam);

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = Current_id
                };
                cmd.Parameters.Add(idParam);

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Кнопка выбора дыты
        private void Select_Date_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Calendar.SelectedDate != null && TimeList.SelectedItem != null)
            {
                DateTime? selectedDate = Calendar.SelectedDate;

                AddAppDateTime(selectedDate.ToString().Remove(10, 8), TimeList.SelectedItem.ToString());

                ServiceSelect.Current_Master_id = Master_id;

                ServiceSelect ServiceSelect1 = new ServiceSelect();

                ServiceSelect1.Show();
                Close();
            }
        }

        //Если дата в календаре изменилось
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //Очищаем выпадающий список
            TimeList.ItemsSource = null;

            //Показываем новую дату
            DateTime? selectedDate = Calendar.SelectedDate;
            showDateBox.Text = selectedDate.ToString().Remove(10, 8);

            //День недели
            var weekDay = selectedDate.Value.DayOfWeek;

            //Вызов метода получения занятого времени
            GetOccupiedTime((DateTime)selectedDate);

            //Получаем начало и конец выбранного рабочего дня
            var start = GetStartTime(weekDay);
            var end = GetEndTime(weekDay);

            //Получаем начало и конец перерыва мастера
            var breakstart = GetBreakStartTime(weekDay);
            var breakend = GetBreakEndTime(weekDay);

            //Получаем не рабочие часы мастера
            GetNotWorkingHours(breakstart, breakend);
            //Получаем забочие часы мастера
            GetWorkingHours(start, end);

            //Добавляем только рабочие часы в выпадающий список
            TimeList.ItemsSource = timelist;
        }
    }
}

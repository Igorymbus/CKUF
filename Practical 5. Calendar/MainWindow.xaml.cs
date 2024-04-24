using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Practical_5._Calendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int daysInMonth;
        private List<DaysMonth> daysMonths_list = new List<DaysMonth>();

        List<UserChoicePerDay> userChoicesPerAllDays = De_Serealize_.Deserialize<UserChoicePerDay>(way);
        public static string way = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Choices-Data";

        public static DateTime selectedDate;
        public MainWindow()
        {
            InitializeComponent();

            PictureAndDateInitialization();
            HowMuchDaysInMonth();
        }

        private void HowMuchDaysInMonth()
        {
            daysInMonth = DateTime.DaysInMonth(datePicker.SelectedDate.Value.Year, datePicker.SelectedDate.Value.Month);
            Fill_DaysInMonth();
        }
        private void Fill_DaysInMonth()
        {
            string IconWay = "";
            int counter = 0;
            bool setted = false;
            daysMonths_list.Clear();

            for (int i = 1; i <= daysInMonth; i++)
            {
                IconWay = "dots.png";
                foreach (UserChoicePerDay item in userChoicesPerAllDays)
                {
                    counter = 0;
                    if (item.Date == new DateTime(datePicker.SelectedDate.Value.Year, datePicker.SelectedDate.Value.Month, i))
                    {
                        for (int x = 0; x < item.userChoice_list.Count; x++)
                        {
                            if (item.userChoice_list[x].isSelected == true)
                            {
                                IconWay = item.userChoice_list[x].Icon.ToString();
                                setted = true;
                                break;
                            }
                        }
                    }
                    else counter++;

                    if (setted)
                    {
                        setted = false;
                        break;
                    }
                    if (counter == userChoicesPerAllDays.Count) IconWay = "dots.png";
                }
                daysMonths_list.Add(new DaysMonth { Day = i.ToString(), IconSource = new BitmapImage(new Uri(IconWay, UriKind.Relative)) });
            }

            WrapPanel.Children.Clear();
            foreach (DaysMonth day in daysMonths_list)
            {
                day.Margin = new Thickness(5);
                day.Cursor = Cursors.Hand;
                WrapPanel.Children.Add(day);
            }
            Create_Events();
        }
        private void PictureAndDateInitialization()
        {
            Uri image = new Uri("calendar_icon.png", UriKind.Relative);
            Calendar_Icon.Source = new BitmapImage(image);

            datePicker.SelectedDate = DateTime.Today;
        }

        private void Button_Click(object sender, RoutedEventArgs e) { datePicker.IsDropDownOpen = true; }
        private void NextMonth_BTN_Click(object sender, RoutedEventArgs e) { DataPicker_DateChanged(1); }

        private void PreviousMonth_BTN_Click(object sender, RoutedEventArgs e) { DataPicker_DateChanged(-1); }

        private void DataPicker_DateChanged(int monthAdd)
        {
            if (datePicker.SelectedDate.HasValue)
            {
                datePicker.SelectedDate = datePicker.SelectedDate.Value.AddMonths(monthAdd);

                HowMuchDaysInMonth();
            }
        }
        private void Create_Events()
        {
            foreach (var element in WrapPanel.Children)
            {
                if (element is DaysMonth daysMonthElement)
                {
                    daysMonthElement.MouseLeftButtonUp += DaysMonthElement_MouseLeftButtonUp;
                }
            }
        }
        private void DaysMonthElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            selectedDate = new DateTime(datePicker.SelectedDate.Value.Year, datePicker.SelectedDate.Value.Month, Convert.ToInt32((sender as DaysMonth).Day));
            datePicker.SelectedDate = selectedDate;
            TXT_Block_2.Visibility = Visibility.Visible;
            TXT_Block_2.Text = selectedDate.ToString("D");

            TXT_Block.Visibility = Visibility.Collapsed;
            NextMonth_BTN.Visibility = PreviousMonth_BTN.Visibility = datePicker_BTN.Visibility = Visibility.Collapsed;
            Back_BTN.Visibility = Visibility.Visible;

            myFrame.Content = new ChoicePage();
        }

        private void Back_BTN_Click(object sender, RoutedEventArgs e)
        {
            NextMonth_BTN.Visibility = PreviousMonth_BTN.Visibility = TXT_Block.Visibility = Visibility.Visible;
            Back_BTN.Visibility = TXT_Block_2.Visibility = Visibility.Collapsed;

            myFrame.Content = null;

            userChoicesPerAllDays.Clear();
            userChoicesPerAllDays = De_Serealize_.Deserialize<UserChoicePerDay>(way);
            HowMuchDaysInMonth();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM_Base
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();     
            
        }

    }

    class BoolToVisibilityConverter : IValueConverter
    {
        // Прямое конвертирование
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility ReturnValue = Visibility.Collapsed;
            if (value == null)
            {
                ReturnValue = Visibility.Hidden;
                return ReturnValue;
            }
            if (value != null)
            {
                ReturnValue = Visibility.Visible;
                return ReturnValue;
            }
            switch ((bool) value)
            {
                case true: ReturnValue = Visibility.Visible; break;
                case false: ReturnValue = Visibility.Hidden; break;
            }

            return ReturnValue;
        }

        // Обратное
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool ReturnValue = false;

            switch ((Visibility) value)
            {
                case Visibility.Visible: ReturnValue = true; break;
                case Visibility.Collapsed: ReturnValue = false; break;
            }

            return ReturnValue;
        }
    }

    public class HoursMinutesTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            // TODO something like:
            return ((TimeSpan) value).ToString("hh':'mm':'ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            // TODO something like:
            return TimeSpan.ParseExact(value.ToString(), "hh':'mm':'ss", System.Globalization.CultureInfo.CurrentCulture);
        }
    }
}

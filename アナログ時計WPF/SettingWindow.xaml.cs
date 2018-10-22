using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace アナログ時計WPF
{
    /// <summary>
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    slider1.Value = double.Parse(textBox1.Text);
                }
                catch (Exception)
                {
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    slider2.Value = double.Parse(textBox2.Text);
                }
                catch (Exception)
                {
                }
            }
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.Owner == null)
            {
                return;
            }
            this.Owner.Width = slider1.Value;
            this.Owner.Height = slider1.Value;
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.Owner == null)
            {
                return;
            }
            this.Owner.Opacity = slider2.Value / 100.0;
        }

        private void toukaCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)((CheckBox)sender).IsChecked)
            {
                SolidColorBrush colorBrush = ((MainWindow)this.Owner).clockCircle.Fill as SolidColorBrush;
                colorBrush.Color = Color.FromArgb(0, 0xff, 0xff, 0xff);
            }
            else
            {
                SolidColorBrush colorBrush = ((MainWindow)this.Owner).clockCircle.Fill as SolidColorBrush;
                colorBrush.Color = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            }
        }

        private void secCheckBox_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)this.Owner).secHandCheck = (bool)((CheckBox)sender).IsChecked;
        }

        private void minCheckBox_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)this.Owner).minHandCheck = (bool)((CheckBox)sender).IsChecked;
        }

        private void dayCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)((CheckBox)sender).IsChecked)
            {
                ((MainWindow)Owner).changeTextVisible(Visibility.Visible);
            }
            else
            {
                ((MainWindow)Owner).changeTextVisible(Visibility.Collapsed);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }

    class DoubleToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToInt32(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d;
            return double.TryParse(value.ToString(), out d) ? d : 0;
        }

    }
}

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
using System.Windows.Shapes;

namespace Wallpaper_Wizard
{

    public partial class InputDialog : Window
    {
        public InputDialog()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            api.key = txtAnswer.Text;
            MainWindow.UpdateAppSettings("apiKey", txtAnswer.Text);
            ((MainWindow)this.Owner).url = "https://api.openweathermap.org/data/2.5/weather?q=" + ((MainWindow)this.Owner).city + "&units=metric&APPID=" + api.key;
            this.Close();
        }

        private void btnDialogClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

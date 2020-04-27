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
using System.Configuration;

namespace Wallpaper_Wizard
{

    public partial class LocationInput : Window
    {

        protected string apiKey = "925292eacc28e46a8f18441961b87c60";

        public LocationInput()
        {
            InitializeComponent();
        }

        private void SubmitChange(object sender, RoutedEventArgs e)
        {
            if (InputBox.Text != null)
            {

                MainWindow.UpdateAppSettings("Location", InputBox.Text);
                ((MainWindow)this.Owner).LocationText.Text = "Location:\n" + InputBox.Text;
                ((MainWindow)this.Owner).city = ConfigurationManager.AppSettings.Get("Location");
                ((MainWindow)this.Owner).url = "https://api.openweathermap.org/data/2.5/weather?q=" + ((MainWindow)this.Owner).city + "&units=metric&APPID=" + apiKey;
                this.Close();
            }
        }
    }
}

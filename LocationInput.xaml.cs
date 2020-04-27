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

    public partial class LocationInput : Window
    {
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
                this.Close();
            }
        }
    }
}

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
using System.IO.Compression;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wallpaper_Wizard
{
    public partial class ThemeManager : Window
    {

        public ThemeManager()
        {
            InitializeComponent();
        }
        
        public bool ImportTheme(string fullPath, string shortenedPath)
        {
            ImportText.Text = "Import " + shortenedPath;
            FileStream unopened = new FileStream(fullPath, FileMode.Open);
            ImportBar.Value = 10;
            ZipArchive archive = new ZipArchive(unopened, ZipArchiveMode.Update);
            ImportBar.Value = 30;
            var outline = archive.GetEntry("theme.json");
            if (outline == null)
            {
                MessageBox.Show("No theme.json file was found.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
                Close();
                return false;
            }
            unopened.Close();
            string dir = System.AppDomain.CurrentDomain.BaseDirectory + "/assets/" + System.IO.Path.GetFileNameWithoutExtension(shortenedPath);
            if (Directory.Exists(dir))
            {
                MessageBox.Show("Could not extract. Folder named " + System.IO.Path.GetFileNameWithoutExtension(shortenedPath) + " already exists in Assets folder.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
                Close();
                return false;
            }
            ImportBar.Value = 60;
            Directory.CreateDirectory(dir);
            ZipFile.ExtractToDirectory(fullPath, dir);
            ImportBar.Value = 80;
            Close();
            return true;
        }
    }
}

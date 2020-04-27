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
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using System.Net.Http;
using System.Net;

namespace Wallpaper_Wizard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string SelectedTheme = ConfigurationManager.AppSettings.Get("SelectedTheme");
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public string city = ConfigurationManager.AppSettings.Get("Location");
        protected string apiKey = "925292eacc28e46a8f18441961b87c60";
        protected string url;
        string[] priority = {"Thunderstorm", "Rain", "Snow", "Night", "Clouds", "Fog", "Sunset", "Day", "Clear"};
        public Dictionary<string, Dictionary<string, string>> themes = new Dictionary<string, Dictionary<string, string>>();

        public MainWindow()
        {
            InitializeComponent();
            RefreshThemes();

            LocationText.Text = "Location:\n" + city;
        }

        private void SelectTheme(object sender, RoutedEventArgs e)
        {
            SelectedTheme = (sender as Button).Name.ToString();
        }

        private void ApplyTheme(object sender, RoutedEventArgs e)
        {
            UpdateAppSettings("SelectedTheme", SelectedTheme);
            this.WindowState = System.Windows.WindowState.Minimized;
            Hide();
            dispatcherTimer.Stop();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            WallpaperApi.EnableTransitions();
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            city = ConfigurationManager.AppSettings.Get("Location");
            url = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&APPID=" + apiKey;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string j = string.Empty;
            dynamic json;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                j = reader.ReadToEnd();
                json = JsonSerializer.Deserialize<dynamic>(j);
            }
            string weather_name = json.GetProperty("weather")[0].GetProperty("main").ToString();
            Int32 time = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Int32 sunrise = json.GetProperty("sys").GetProperty("sunrise").GetInt32();
            Int32 sunset = json.GetProperty("sys").GetProperty("sunset").GetInt32();
            string period;

            if (600 <= Math.Abs(time - sunrise) || 600 <= Math.Abs(time - sunset))
            {
                period = "Sunset";
            } else if ((sunrise + 600) < time && time < (sunset - 600))
            {
                period = "Day";
            } else
            {
                period = "Night";
            }

            string chosen = priority[Math.Min(Array.IndexOf(priority, period), Array.IndexOf(priority, weather_name))];
            Console.WriteLine(themes[SelectedTheme]["Path"] + "\\" + themes[SelectedTheme][chosen]);
            WallpaperApi.SetWallpaper(themes[SelectedTheme]["Path"] + "\\" + themes[SelectedTheme][chosen]);
        }

            

        public void OpenWindow(object sender, RoutedEventArgs e)
        {
            Show();
            this.WindowState = System.Windows.WindowState.Normal; 
        }

        public static void UpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                settings[key].Value = value;
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        private void RunBackground(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = System.Windows.WindowState.Minimized;
            Hide();
        }

        private void PickThemeZip(object sender, RoutedEventArgs e)
        {
            OpenFileDialog filePicker = new OpenFileDialog();
            filePicker.Filter = "Zip files (.zip)|*.zip";
            if (filePicker.ShowDialog() == true)
            {
                ThemeManager themeManager = new ThemeManager();
                themeManager.Show();
                bool result = themeManager.ImportTheme(filePicker.FileName, filePicker.SafeFileName);

                if (result)
                {
                    RefreshThemes();
                }
            }
        }

        private bool CheckFiles(Dictionary<string, string> themeJSON, string directory)
        {
            string[] keys = { "Title", "Day", "Night", "Sunset", "Rain", "Snow", "Clouds", "Fog", "Thunderstorm" };
            foreach (string k in keys)
            {
                if (!themeJSON.ContainsKey(k))
                {
                    MessageBox.Show("Key " + k + " does not exist in theme.json",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
                    return false;
                }
                if (k != "Title" && !File.Exists(directory + "/" + themeJSON[k]))
                {
                    MessageBox.Show("File " + themeJSON[k] + " does not exist in directory " + directory,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
                    return false;
                }
            }
            return true;
        }

        private void RefreshThemes()
        {
            themeList.Children.Clear();
            themes.Clear();
            foreach (string path in Directory.GetDirectories(System.AppDomain.CurrentDomain.BaseDirectory + "assets"))
            {
                StreamReader s;
                string serializedJSON;
                Dictionary<string, string> theme;
                try
                {
                    s = new StreamReader(path + "/theme.json");
                    serializedJSON = s.ReadToEnd();
                    theme = JsonSerializer.Deserialize<Dictionary<string, string>>(serializedJSON);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    continue;
                }
                if (!CheckFiles(theme, path))
                {
                    continue;
                }
                themes.Add(theme["Title"], theme);
                themes[theme["Title"]].Add("Path", path);
                {
                    string src = theme["Day"];
                    Button buttonTemplate = new Button
                    {
                        Height = 182,
                        Width = 314,
                        Margin = new Thickness(5),
                        Name = theme["Title"],
                        Content = new Image
                        {
                            Height = 182,
                            Width = 314,
                            Stretch = Stretch.UniformToFill,
                            Source = new BitmapImage(new Uri(path + "/" + src))
                        }
                    };
                    buttonTemplate.Click += new RoutedEventHandler(SelectTheme);
                    themeList.Children.Add(buttonTemplate);
                }
            }
        }

        public void ChangeLocation(object sender, RoutedEventArgs e)
        {
            LocationInput locationInput = new LocationInput();
            locationInput.Owner = this;
            locationInput.Show();
        }

        public void UpdateLocationText(string location)
        {
            city = location;
            LocationText.Text = "Location:\n" + city;
        }

        public void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}

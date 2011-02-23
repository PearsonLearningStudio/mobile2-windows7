using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;

namespace eCollegeWP7.Views
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var settings = IsolatedStorageSettings.ApplicationSettings;
            bool remember;

            if (settings.TryGetValue<bool>("remember", out remember))
            {
                if (remember)
                {
                    TxtUsername.Text = settings["username"] as string;
                    TxtPassword.Password = settings["password"] as string;
                    TxtDomain.Text = settings["domain"] as string;
                    ChkRememberMe.IsChecked = true;
                }
                else
                {
                    ChkRememberMe.IsChecked = false;
                }
            }
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (ChkRememberMe.IsChecked != true) {
                var settings = IsolatedStorageSettings.ApplicationSettings;
                settings["remember"] = false;
                settings.Remove("username");
                settings.Remove("password");
                settings.Remove("domain");
                settings.Save();
            }

            var username = TxtUsername.Text;
            var password = TxtPassword.Password;
            var domain = TxtDomain.Text;
            var remember = ChkRememberMe.IsChecked;

            App.AppViewModel.Login(domain,username, password, me =>
            {
                if (remember == true)
                {
                    var settings = IsolatedStorageSettings.ApplicationSettings;
                    settings["remember"] = true;
                    settings["username"] = username;
                    settings["password"] = password;
                    settings["domain"] = domain;
                    settings.Save();
                }
                this.NavigationService.Navigate(new Uri("/Views/MainPage.xaml",UriKind.Relative));
            });
        }
    }
}
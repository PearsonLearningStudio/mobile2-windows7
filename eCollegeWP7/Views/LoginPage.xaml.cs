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
using eCollegeWP7.ViewModels;

namespace eCollegeWP7.Views
{
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            bool remember;
            SessionViewModel session;

            if (settings.TryGetValue<bool>("remember", out remember) && settings.TryGetValue<SessionViewModel>("session",out session))
            {
                if (remember)
                {
                    TxtUsername.Text = session.Username;
                    TxtPassword.Password = session.Password;
                    TxtDomain.Text = session.Domain;
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
                settings.Remove("session");
                settings.Remove("remember");
                settings.Save();
            }

            App.Model.Session = new SessionViewModel
            {
                Username = TxtUsername.Text,
                Password = TxtPassword.Password,
                Domain = TxtDomain.Text
            };
            var remember = ChkRememberMe.IsChecked;

            if (remember == true)
            {
                var settings = IsolatedStorageSettings.ApplicationSettings;
                settings["remember"] = true;
                settings["session"] = App.Model.Session;
                settings.Save();
            }

            App.Model.Session.Login(result =>
            {
                if (remember == true)
                {
                    var settings = IsolatedStorageSettings.ApplicationSettings;
                    settings["remember"] = true;
                    settings["session"] = App.Model.Session;
                    settings.Save();
                }
                this.NavigationService.Navigate(new Uri("/Views/MainPage.xaml", UriKind.Relative));
            });
        }
    }
}
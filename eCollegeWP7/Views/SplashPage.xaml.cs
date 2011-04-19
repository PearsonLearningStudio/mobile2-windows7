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
    public partial class SplashPage : BasePage
    {
        public SplashPage() : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;

            string grantToken;
            if (settings.TryGetValue<string>("grantToken", out grantToken))
            {
                App.Model.Client.SetupAuthentication(grantToken);
                App.Model.Login(grantToken, 
                    () => this.NavigationService.Navigate(new Uri("/Views/MainPage.xaml", UriKind.Relative)), 
                    (ex) => this.NavigationService.Navigate(new Uri("/Views/LoginPage.xaml", UriKind.Relative)));
            }
            else
            {
                if (AppResources.UseSSO.Equals("true"))
                {
                    this.NavigationService.Navigate(new Uri("/Views/SingleSignonPage.xaml", UriKind.Relative));
                } else
                {
                    this.NavigationService.Navigate(new Uri("/Views/LoginPage.xaml", UriKind.Relative));
                }

            }
        }
    }
}
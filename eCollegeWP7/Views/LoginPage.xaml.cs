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

namespace eCollegeWP7.Views
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            App.AppViewModel.Login(TxtUsername.Text, TxtPassword.Password, me =>
            {
                this.NavigationService.Navigate(new Uri("/Views/MainPage.xaml",UriKind.Relative));
            });
        }
    }
}
﻿using System;
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
using ECollegeAPI.Exceptions;

using Coding4Fun.Phone.Controls;

namespace eCollegeWP7.Views
{
    public partial class LoginPage : BasePage
    {
        public LoginPage() : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TxtUsername.Text) || String.IsNullOrEmpty(TxtPassword.Password))
            {
                App.Model.ShowAlert("Unable to Login","You must provide both a username and password");
                return;
            }

            bool remember = ChkRememberMe.IsChecked.Value;

            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (!remember)
            {
                settings.Remove("grantToken");
                settings.Save();
            }

            App.Model.Login(TxtUsername.Text, TxtPassword.Password, 
                () =>
                {
                    if (remember)
                    {
                        settings["grantToken"] = App.Model.Client.GrantToken;
                        settings.Save();
                    }
                    this.NavigationService.Navigate(new Uri("/Views/MainPage.xaml", UriKind.Relative));
                },
                (ex) =>
                {
                    if (ex is AuthenticationException)
                    {
                        ex.IsHandled = true;
                        App.Model.ShowAlert("Login Failed","Incorrect username and/or password");
                    }
                });
        }
    }
}
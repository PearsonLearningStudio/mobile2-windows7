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
using System.Diagnostics;
using eCollegeWP7.Util;

namespace eCollegeWP7.Views
{
    public partial class MainFrame : PhoneApplicationFrame
    {

        public MainFrame() : base()
        {
            InitializeComponent();
        }

        private void PhoneApplicationFrame_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = App.Model;
            Debug.WriteLine("frame loaded");
        }

        private void TxtCurrentUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Navigate(new Uri("/Views/ProfilePage.xaml", UriKind.Relative));
        }

        private void TxtCurrentUser_Loaded(object sender, RoutedEventArgs e)
        {
            var TxtCurrentUser = sender as TextBlock;
            TxtCurrentUser.Style = Application.Current.Resources["PhoneTextCurrentUserStyle"] as Style;
        }
    }
}

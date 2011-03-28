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
using ECollegeAPI.Model;
using eCollegeWP7.Util;
using eCollegeWP7.ViewModels;

namespace eCollegeWP7.Views
{
    public partial class ProfilePage : BasePage
    {

        public ProfilePage() : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            var cvm = new CoursesViewModel();
            this.DataContext = cvm;
            cvm.Load();
        }

        private void BtnOpenCourse_Click(object sender, RoutedEventArgs e)
        {
            var course = (sender as Button).DataContext as Course;
            this.NavigationService.Navigate(new Uri("/Views/CoursePage.xaml?courseId=" + course.ID, UriKind.Relative));
        }

        private void BtnSignOut_Click(object sender, RoutedEventArgs e)
        {
            App.SignOut();
        }

    }
}
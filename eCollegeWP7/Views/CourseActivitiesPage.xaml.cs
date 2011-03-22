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
    public partial class CourseActivitiesPage : BasePage
    {

        public ActivitiesViewModel Model { get { return this.DataContext as ActivitiesViewModel; } }

        public CourseActivitiesPage()
            : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            int courseId = Int32.Parse(parameters["courseId"]);
            this.DataContext = new ActivitiesViewModel();
            Model.CourseID = courseId;
            Model.Load(false);
        }

        private void BtnLoadMore_Click(object sender, RoutedEventArgs e)
        {
            Model.Load(true);
        }

        private void BtnActivity_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var vm = (ActivityViewModel)btn.DataContext;

            if (vm.NavigationPath != null)
            {
                this.NavigationService.Navigate(new Uri(vm.NavigationPath, UriKind.Relative));
            }
        }

    }
}
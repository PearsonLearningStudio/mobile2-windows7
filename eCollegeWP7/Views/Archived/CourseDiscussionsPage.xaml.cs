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

namespace eCollegeWP7.Views.Archived
{
    public partial class CourseDiscussionsPage : BasePage
    {
        public CourseDiscussionsViewModel Model { get { return this.DataContext as CourseDiscussionsViewModel; } }

        public CourseDiscussionsPage()
            : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            int courseId = Int32.Parse(parameters["courseId"]);
            this.DataContext = new CourseDiscussionsViewModel(courseId);
        }

        private void BtnDiscussion_Click(object sender, RoutedEventArgs e)
        {
            var dis = (sender as Button).DataContext as DiscussionViewModel;
            this.NavigationService.Navigate(new Uri(dis.NavigationPath, UriKind.Relative));
        }
    }
}
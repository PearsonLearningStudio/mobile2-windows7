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
    public partial class CoursePage : BasePage
    {

        public CourseViewModel Model { get { return this.DataContext as CourseViewModel; } }

        public CoursePage()
            : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            int courseId = Int32.Parse(parameters["courseId"]);
            this.DataContext = new CourseViewModel(courseId);
        }

        private void BtnCourseLink_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as HyperlinkButton;
            var link = btn.DataContext as LinkViewModel;

            if (link.NavigationPath != null)
            {
                this.NavigationService.Navigate(new Uri(link.NavigationPath, UriKind.Relative));
            }
        }

        private void BtnLatestAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            var ann = Model.AnnouncementVM.Announcements[0];

            this.NavigationService.Navigate(
                new Uri("/Views/AnnouncementPage.xaml?courseId=" + Model.CourseID + "&announcementId=" + ann.ID,
                        UriKind.Relative));
        }

    }
}
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
using System.Collections.ObjectModel;
using ECollegeAPI.Model;
using eCollegeWP7.Util;
using eCollegeWP7.Views.Dialogs;
using eCollegeWP7.Exceptions;
using eCollegeWP7.ViewModels;

namespace eCollegeWP7.Views
{
    public partial class SecondaryPage : BasePage
    {

        protected DiscussionsViewModel DiscussionsViewModel { get; set; }

        // Constructor
        public SecondaryPage()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            string defaultPanoramaItem;
            {
                defaultPanoramaItem = "PanCourses";
            }

            var defaultItem = PanMain.FindName(defaultPanoramaItem);
            PanMain.DefaultItem = defaultItem;
        }

        private void BtnOpenCourse_Click(object sender, RoutedEventArgs e)
        {
            var course = (sender as Button).DataContext as Course;
            this.NavigationService.Navigate(new Uri("/Views/CoursePage.xaml?courseId=" + course.ID, UriKind.Relative));
        }

        private void BtnDiscussion_Click(object sender, RoutedEventArgs e)
        {
            var theader = (sender as Button).DataContext as DiscussionTopicHeader;
            this.NavigationService.Navigate(new Uri("/Views/DiscussionPage.xaml?topicId=" + theader.Topic.ID + "&topicHeaderId=" + theader.ID, UriKind.Relative));
        }

        private void LspFilterDiscussions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var LspFilterDiscussions = sender as ListPicker;
            DiscussionsViewModel.DiscussionCourseFilter = LspFilterDiscussions.SelectedItem as Course;
        }

        private void LspFilterPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var LspFilterPeople = sender as ListPicker;
            //Model.PeopleCourseFilter = LspFilterPeople.SelectedItem as Course;
        }

        private void PanDiscussions_Loaded(object sender, RoutedEventArgs e)
        {
            if (DiscussionsViewModel == null)
            {
                DiscussionsViewModel = new DiscussionsViewModel();
                DiscussionsViewModel.Load();
                (sender as PanoramaItem).DataContext = DiscussionsViewModel;
            }
        }

        private void PanCourses_Loaded(object sender, RoutedEventArgs e)
        {
            var panCourses = sender as PanoramaItem;
            if (panCourses.DataContext == null || !(panCourses.DataContext is CoursesViewModel))
            {
                panCourses.DataContext = App.Model.Courses;
            }
        }
    }
}
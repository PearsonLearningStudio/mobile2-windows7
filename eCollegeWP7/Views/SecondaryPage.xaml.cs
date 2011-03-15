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
        public SecondaryPage() : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            string defaultPanoramaItem;

            if (!parameters.TryGetValue("defaultPanoramaItem", out defaultPanoramaItem))
            {
                defaultPanoramaItem = "PanHome";
            }

            var defaultItem = PanMain.FindName(defaultPanoramaItem) as PanoramaItem;
            PanMain.DefaultItem = defaultItem;
            UpdateSelectedPanoramaItem(defaultItem);
        }

        private void BtnOpenCourse_Click(object sender, RoutedEventArgs e)
        {
            var course = (sender as Button).DataContext as Course;
            this.NavigationService.Navigate(new Uri("/Views/CoursePage.xaml?courseId=" + course.ID, UriKind.Relative));
        }

        private void BtnDiscussion_Click(object sender, RoutedEventArgs e)
        {
            var theader = (sender as Button).DataContext as UserDiscussionTopic;
            this.NavigationService.Navigate(new Uri("/Views/DiscussionPage.xaml?topicId=" + theader.Topic.ID + "&userTopicId=" + theader.ID, UriKind.Relative));
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

        protected void UpdateSelectedPanoramaItem(PanoramaItem selectedItem)
        {
            if (selectedItem != null)
            {
                if (selectedItem.Name == "PanDiscussions")
                {
                    if (DiscussionsViewModel == null)
                    {
                        DiscussionsViewModel = new DiscussionsViewModel();
                        DiscussionsViewModel.Load();
                        selectedItem.DataContext = DiscussionsViewModel;
                    }
                }
                else if (selectedItem.Name == "PanCourses")
                {
                    if (selectedItem.DataContext == null || !(selectedItem.DataContext is CoursesViewModel))
                    {
                        selectedItem.DataContext = App.Model.Courses;
                    }
                }
            }
        }

        private void PanMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSelectedPanoramaItem(PanMain.SelectedItem as PanoramaItem);
        }
    }
}
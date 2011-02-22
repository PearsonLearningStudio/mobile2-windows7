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

namespace eCollegeWP7.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainViewModel Model { get { return this.DataContext as MainViewModel; } }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = new MainViewModel();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
             */
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Model.LoadData();
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
            Model.DiscussionCourseFilter = LspFilterDiscussions.SelectedItem as Course;
        }

        private void LspFilterPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var LspFilterPeople = sender as ListPicker;
            Model.PeopleCourseFilter = LspFilterPeople.SelectedItem as Course;
        }
    }
}
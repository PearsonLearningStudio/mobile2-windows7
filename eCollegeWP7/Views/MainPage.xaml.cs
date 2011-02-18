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

namespace eCollegeWP7.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
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
            App.ViewModel.API.FetchMyCurrentCourses(result =>
            {
                var oc = new ObservableCollection<Course>();
                foreach (var c in result) { oc.Add(c); }
                App.ViewModel.MyCourses = oc;
            });

        }

        private void BtnOpenCourse_Click(object sender, RoutedEventArgs e)
        {
            var course = (sender as Button).DataContext as Course;
            this.NavigationService.Navigate(new Uri("/Views/CoursePage.xaml?courseId=" + course.ID, UriKind.Relative));
        }
    }
}
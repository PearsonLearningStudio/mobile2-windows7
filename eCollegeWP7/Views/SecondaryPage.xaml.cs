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
        protected DiscussionsViewModel _discussionsViewModel = new DiscussionsViewModel();
        protected bool _alreadyNavigatedTo = false;

        // Constructor
        public SecondaryPage() : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (!_alreadyNavigatedTo)
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
                _alreadyNavigatedTo = true;
            } else
            {
                //quick hack until i can figure out why the list isn't loading on back button
                var oldDiscussionsVM = _discussionsViewModel;
                _discussionsViewModel = new DiscussionsViewModel();
                _discussionsViewModel.DiscussionCourseFilter = oldDiscussionsVM.DiscussionCourseFilter;
                if (oldDiscussionsVM.LoadStarted) _discussionsViewModel.Load();
                PanDiscussions.DataContext = _discussionsViewModel;
            }
        }

        private void BtnOpenCourse_Click(object sender, RoutedEventArgs e)
        {
            var course = (sender as Button).DataContext as Course;
            this.NavigationService.Navigate(new Uri("/Views/CoursePage.xaml?courseId=" + course.ID, UriKind.Relative));
        }

        private void BtnDiscussion_Click(object sender, RoutedEventArgs e)
        {
            var dis = (sender as Button).DataContext as DiscussionViewModel;
            this.NavigationService.Navigate(new Uri(dis.NavigationPath, UriKind.Relative));
        }

        private void LspFilterDiscussions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var LspFilterDiscussions = sender as ListPicker;

            if (LspFilterDiscussions.Tag == null)
            {
                LspFilterDiscussions.Tag = "NotFirstInit";
                var lspItems = LspFilterDiscussions.ItemsSource as ObservableCollection<Course>;
                if (_discussionsViewModel.DiscussionCourseFilter != CoursesViewModel.AllCoursesPlaceholder && lspItems != null)
                {
                    for (int i = 0; i < lspItems.Count; i++)
                    {
                        if (lspItems[i] == _discussionsViewModel.DiscussionCourseFilter)
                        {
                            LspFilterDiscussions.SelectedIndex = i;
                            LspFilterDiscussions.SelectedItem = lspItems[i];
                            return;
                        }
                    }
                }
            }
            _discussionsViewModel.DiscussionCourseFilter = LspFilterDiscussions.SelectedItem as Course;
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
                    _discussionsViewModel.Load();
                    selectedItem.DataContext = _discussionsViewModel;
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

        private void LspFilterDiscussions_Loaded(object sender, RoutedEventArgs e) {
            var LspFilterDiscussions = sender as ListPicker;

        }

        private void PnlListHeader_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as StackPanel).DataContext = _discussionsViewModel;
        }

        private void BtnSeeAll_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var dataContext = btn.DataContext as Group<DiscussionViewModel>;

            if (dataContext != null)
            {
                this.NavigationService.Navigate(new Uri("/Views/CourseDiscussionsPage.xaml?courseId=" + dataContext.GroupId, UriKind.Relative));
            }
        }
    }
}
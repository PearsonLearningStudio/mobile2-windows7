using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using ECollegeAPI.Model;
using ECollegeAPI.Services.Discussions;
using eCollegeWP7.Util;
using ECollegeAPI.Services.Announcements;
using ECollegeAPI.Services.Courses;

namespace eCollegeWP7.ViewModels
{
    public class CourseViewModel : ViewModelBase
    {

        private Course _Course;
        public Course Course
        {
            get { return _Course; }
            set { _Course = value; this.OnPropertyChanged(() => this.Course); }
        }
        
        private long _CourseID;
        public long CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value; this.OnPropertyChanged(() => this.CourseID); }
        }

        private ObservableCollection<User> _Instructors;
        public ObservableCollection<User> Instructors
        {
            get { return _Instructors; }
            set { _Instructors = value; this.OnPropertyChanged(() => this.Instructors); }
        }

        private ObservableCollection<LinkViewModel> _CourseLinks;
        public ObservableCollection<LinkViewModel> CourseLinks
        {
            get { return _CourseLinks; }
            set { _CourseLinks = value; this.OnPropertyChanged(() => this.CourseLinks); }
        }

        private AnnouncementsViewModel _AnnouncementVM;
        public AnnouncementsViewModel AnnouncementVM
        {
            get { return _AnnouncementVM; }
            set { _AnnouncementVM = value; this.OnPropertyChanged(() => this.AnnouncementVM); }
        }
        
        public CourseViewModel(long courseId)
        {
            this.CourseID = courseId;
            this.Course = App.Model.Courses.CourseIdMap[courseId];

            App.BuildService(new FetchInstructorsForCourseService(courseId)).Execute(service =>
            {
                this.Instructors = service.Result.ToObservableCollection();
            });

            this.AnnouncementVM = new AnnouncementsViewModel(courseId);

            var links = new ObservableCollection<LinkViewModel>();

            links.Add(new LinkViewModel()
            {
                Title = "announcements",
                NavigationPath = "/Views/CourseAnnouncementsPage.xaml?courseId=" + courseId,
                IconTemplate = "IconAnnouncementsLink"
            });

            //links.Add(new LinkViewModel()
            //{
            //    Title = "activity",
            //    NavigationPath = "/Views/CourseActivitiesPage.xaml?courseId=" + courseId
            //});

            //links.Add(new LinkViewModel()
            //{
            //    Title = "dropbox",
            //    NavigationPath = "/Views/CourseDropboxMessagesPage.xaml?courseId=" + courseId
            //});

            //links.Add(new LinkViewModel()
            //{
            //    Title = "discussions",
            //    NavigationPath = "/Views/CourseDiscussionsPage.xaml?courseId=" + courseId,
            //    IconTemplate = "IconDiscussions"
            //});

            links.Add(new LinkViewModel()
            {
                Title = "gradebook",
                NavigationPath = "/Views/CourseGradebookPage.xaml?courseId=" + courseId,
                IconTemplate = "IconGradebookLink"
            });

            links.Add(new LinkViewModel()
            {
                Title = "people",
                NavigationPath = "/Views/CoursePeoplePage.xaml?courseId=" + courseId,
                IconTemplate = "IconPeopleLink"
            });

            CourseLinks = links;

        }

    }
}
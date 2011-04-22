using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
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
using ECollegeAPI.Services.Multimedia;
using eCollegeWP7.Util;
using System.Linq;
using ECollegeAPI.Services.Users;
using eCollegeWP7.Util.Converters;

namespace eCollegeWP7.ViewModels
{
    public class HtmlContentViewModel : ViewModelBase
    {

        private long _CourseID;
        public long CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value;
                this.OnPropertyChanged(() => this.CourseID); this.OnPropertyChanged(() => this.Course); }
        }

        private Course _Course;
        public Course Course
        {
            get { return AppViewModel.Courses.CourseIdMap[CourseID]; }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; this.OnPropertyChanged(() => this.Title); }
        }

        private string _HtmlContent;
        public string HtmlContent
        {
            get { return _HtmlContent; }
            set { _HtmlContent = value; this.OnPropertyChanged(() => this.HtmlContent); }
        }

        private long _HtmlID;
        public long  HtmlID
        {
            get { return _HtmlID; }
            set { _HtmlID = value; this.OnPropertyChanged(() => this.HtmlID); }
        }
        

        public HtmlContentViewModel(long courseId, long htmlId, string title)
        {
            this.CourseID = courseId;
            this.HtmlID = htmlId;
            this.Title = title;
            App.Model.BuildService(new FetchHtmlByIdService(CourseID, HtmlID)).Execute((service) =>
            {
                HtmlContent = HtmlToTextConverter.StripHtmlBody(service.Result);
            });
        }

    }
}
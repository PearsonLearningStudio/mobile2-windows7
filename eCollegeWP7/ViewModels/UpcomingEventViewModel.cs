using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
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
using eCollegeWP7.Util;
using ECollegeAPI.Services.Discussions;
using eCollegeWP7.Util.Converters;

namespace eCollegeWP7.ViewModels
{

    public class UpcomingEventViewModel : ViewModelBase
    {
        public string NavigationPath { get; set; }
        public string IconTemplate { get; set; }
        public string Title { get; set; }
        public string ScheduleInfo { get; set; }

        public UpcomingEventViewModel(UpcomingEventItem item)
        {
            Title = item.Title;
            if (item.EventType == UpcomingEventType.Html)
            {
                NavigationPath = "/Views/HtmlContentPage.xaml?courseId=" + item.CourseID + "&htmlId=" + item.MultimediaID + "&title=" + HttpUtility.UrlEncode(item.Title);
                IconTemplate = "IconPerson"; //need better icon
            } else if (item.EventType == UpcomingEventType.Thread)
            {
                NavigationPath = "/Views/ThreadPage.xaml?courseId=" + item.CourseID + "&threadId=" + item.ThreadID;
                IconTemplate = "IconDiscussionsNoResponses";
            } else if (item.EventType == UpcomingEventType.QuizExamTest)
            {
                IconTemplate = "IconGrade"; //need better icon?
            }
            if (item.CategoryType == CategoryType.Start)
            {
                ScheduleInfo = "Starts at " + DateTimeUtil.LongFriendlyDate(item.When.Time);
            } else if (item.CategoryType == CategoryType.End)
            {
                ScheduleInfo = "Ends at " + DateTimeUtil.LongFriendlyDate(item.When.Time);
            } else if (item.CategoryType == CategoryType.Due)
            {
                ScheduleInfo = "Due at " + DateTimeUtil.LongFriendlyDate(item.When.Time);
            }
        }


    }
}
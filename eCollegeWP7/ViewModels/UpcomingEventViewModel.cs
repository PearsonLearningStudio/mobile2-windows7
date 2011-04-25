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


            if (item.CategoryType == CategoryType.Start)
            {
                ScheduleInfo = "Starts at " + DateTimeUtil.LongFriendlyDate(item.When.Time);
            }
            else if (item.CategoryType == CategoryType.End)
            {
                ScheduleInfo = "Ends at " + DateTimeUtil.LongFriendlyDate(item.When.Time);
            }
            else if (item.CategoryType == CategoryType.Due)
            {
                ScheduleInfo = "Due at " + DateTimeUtil.LongFriendlyDate(item.When.Time);
            }

            if (item.EventType == UpcomingEventType.Html)
            {
                NavigationPath = "/Views/HtmlContentPage.xaml?courseId=" + item.CourseID + "&htmlId=" + item.MultimediaID + "&title=" + HttpUtility.UrlEncode(item.Title);
                NavigationPath += "&scheduleInfo=" + HttpUtility.UrlEncode(ScheduleInfo);
                IconTemplate = "IconPerson"; //need better icon
            } else if (item.EventType == UpcomingEventType.Thread)
            {
                NavigationPath = "/Views/ThreadPage.xaml?courseId=" + item.CourseID + "&threadId=" + item.ThreadID;
                NavigationPath += "&scheduleInfo=" + HttpUtility.UrlEncode(ScheduleInfo);
                IconTemplate = "IconDiscussionsNoResponses";
            } else if (item.EventType == UpcomingEventType.QuizExamTest)
            {
                IconTemplate = "IconGrade"; //need better icon?
            }
        }

        public static string ParseDateGroup(UpcomingEventItem item)
        {
            var dt = item.When.Time;

            if (dt < DateTime.Today.AddDays(1)) return "Today";
            if (dt < DateTime.Today.AddDays(2)) return "Tomorrow";
            if (dt < DateTime.Today.AddDays(3)) return "In 2 Days";
            if (dt < DateTime.Today.AddDays(4)) return "In 3 Days";
            if (dt < DateTime.Today.AddDays(5)) return "In 4 Days";
            if (dt < DateTime.Today.AddDays(6)) return "In 5 Days";
            return "Later";
        }


    }
}
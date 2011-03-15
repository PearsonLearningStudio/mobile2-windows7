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
using eCollegeWP7.Util;
using System.Linq;

namespace eCollegeWP7.ViewModels
{
    public class ActivityViewModel : ViewModelBase
    {
        private ActivityStreamItem item;

        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string LineThree { get; set; }
        public string IconPath { get; set; }
        public string FriendlyDate { get; set; }
        public string NavigationPath { get; set; }

        public ActivityViewModel(ActivityStreamItem item) : base()
        {
            SetupFromItem(item);
        }

        protected void SetupFromItem(ActivityStreamItem item)
        {
            FriendlyDate = DateTimeUtil.FriendlyDate(item.PostedTime);

            if ("thread-topic" == item.Object.ObjectType)
            {
                LineOne = "Title: " + item.Target.Title;
                IconPath = "/Resources/Icons/ic_menu_help.png";
                NavigationPath = "/Views/DiscussionPage.xaml?topicId=" + item.Object.ReferenceId;
            }
            else if ("thread-post" == item.Object.ObjectType)
            {
                LineOne = "Re: " + item.Target.Title;
                IconPath = "/Resources/Icons/ic_menu_help.png";
                NavigationPath = "/Views/DiscussionPage.xaml?responseId=" + item.Object.ReferenceId;
            } 
            else if ("grade" == item.Object.ObjectType) 
            {
                LineOne = "Grade: " + item.Target.Title;
                IconPath = "/Resources/Icons/ic_menu_help.png";
                NavigationPath = "/Views/GradePage.xaml?courseId=" + item.Object.CourseId + "&gradebookItemGuid=" + item.Target.ReferenceId;
            }
            else if ("dropbox-submission" == item.Object.ObjectType)
            {
                LineOne = "Dropbox: " + item.Target.Title;
                IconPath = "/Resources/Icons/ic_menu_help.png";
                NavigationPath = "/Views/DropboxMessagePage.xaml?courseId=" + item.Object.CourseId + "&basketId=" + item.Target.ReferenceId + "&messageId=" + item.Object.ReferenceId;
            }
            else
            {
                LineOne = item.Object.ObjectType + ": " + item.Target.Title;
                IconPath = "/Resources/Icons/ic_menu_help.png";
            }

            LineTwo = item.Object.Summary;

            Course c;
            if (AppViewModel.Courses.CourseIdMap.TryGetValue(item.Object.CourseId, out c))
            {
                LineThree = c.Title + " (" + c.DisplayCourseCode + ")";
            }
            else
            {
                LineThree = "Unknown Course";
            }
        }

    }
}
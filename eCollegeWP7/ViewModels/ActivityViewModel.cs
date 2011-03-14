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
        private ActivityStreamItem _item;

        public string LineOne
        {
            get
            {
                if ("thread-topic" == _item.Object.ObjectType) return "Title: " + _item.Target.Title;
                if ("thread-post" == _item.Object.ObjectType) return "Re: " + _item.Target.Title;
                if ("grade" == _item.Object.ObjectType) return "Grade: " + _item.Target.Title;
                if ("dropbox-submission" == _item.Object.ObjectType) return "Dropbox: " + _item.Target.Title;
                return _item.Object.ObjectType + ": " + _item.Target.Title;
            }
        }

        public string LineTwo
        {
            get
            {
                return _item.Object.Summary;
            }
        }
        public string LineThree
        {
            get
            {
                Course c;
                if (AppViewModel.Courses.CourseIdMap.TryGetValue(_item.Object.CourseId, out c))
                {
                    return c.Title + " (" + c.DisplayCourseCode + ")";
                }
                else
                {
                    return "Unknown Course";
                }
            }
        }

        public string IconPath
        {
            get
            {
                if ("thread-topic" == _item.Object.ObjectType) return "/Resources/Icons/ic_menu_help.png";
                if ("thread-post" == _item.Object.ObjectType) return "/Resources/Icons/ic_menu_help.png";
                if ("grade" == _item.Object.ObjectType) return "/Resources/Icons/ic_menu_help.png";
                if ("dropbox-submission" == _item.Object.ObjectType) return "/Resources/Icons/ic_menu_help.png";
                return "/Resources/Icons/ic_menu_help.png";
            }
        }

        public string FriendlyDate { get; set; }

        public ActivityViewModel(ActivityStreamItem item) : base()
        {
            _item = item;
            FriendlyDate = "yesterday";
        }

    }
}
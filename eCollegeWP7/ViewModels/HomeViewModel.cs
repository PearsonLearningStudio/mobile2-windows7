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
using eCollegeWP7.Util;
using ECollegeAPI.Model;
using ECollegeAPI;
using System.Linq;

namespace eCollegeWP7.ViewModels
{
    public class HomeLink
    {
        public string Title { get; set; }
        public string IconPath { get; set; }
        public string PanoramaItemName { get; set; }
        public string LinkPath { get; set; }
    }

    public class HomeViewModel : ViewModelBase
    {

        public ObservableCollection<HomeLink> HomeLinks { get; set; }
        public HomeViewModel()
        {

            var links = new ObservableCollection<HomeLink>();

            links.Add(new HomeLink()
            {
                Title = "activity",
                IconPath = "/Resources/Icons/ic_menu_activity.png",
                PanoramaItemName = "PanActivity"
            });
            links.Add(new HomeLink()
            {
                Title = "what's due",
                IconPath = "/Resources/Icons/ic_menu_whats_due.png",
                PanoramaItemName = "PanWhatsDue"
            });
            links.Add(new HomeLink()
            {
                Title = "discussions",
                IconPath = "/Resources/Icons/ic_menu_discussions.png",
                LinkPath = "/Views/SecondaryPage.xaml?defaultPanoramaItem=PanDiscussions"
            });
            links.Add(new HomeLink()
            {
                Title = "courses",
                IconPath = "/Resources/Icons/ic_menu_courses.png",
                LinkPath = "/Views/SecondaryPage.xaml?defaultPanoramaItem=PanCourses"
            });
            links.Add(new HomeLink()
            {
                Title = "people",
                IconPath = "/Resources/Icons/ic_menu_people.png",
                LinkPath = "/Views/SecondaryPage.xaml?defaultPanoramaItem=PanPeople"
            });
            links.Add(new HomeLink()
            {
                Title = "my profile",
                IconPath = "/Resources/Icons/ic_menu_profile.png",
                LinkPath = "/Views/ProfilePage.xaml"
            });
            links.Add(new HomeLink()
            {
                Title = "help",
                IconPath = "/Resources/Icons/ic_menu_help.png",
                LinkPath = "/Views/HelpPage.xaml"
            });
            links.Add(new HomeLink()
            {
                Title = "settings",
                IconPath = "/Resources/Icons/ic_menu_settings.png",
                LinkPath = "/Views/SettingsPage.xaml"
            });

            HomeLinks = links;
        }
    }
}
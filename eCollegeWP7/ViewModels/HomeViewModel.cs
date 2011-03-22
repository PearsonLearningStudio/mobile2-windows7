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
        public string IconTemplate { get; set; }
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
                IconTemplate = "IconActivity",
                PanoramaItemName = "PanActivity"
            });
            links.Add(new HomeLink()
            {
                Title = "what's due",
                IconTemplate = "IconWhatsDue",
                PanoramaItemName = "PanWhatsDue"
            });
            links.Add(new HomeLink()
            {
                Title = "discussions",
                IconTemplate = "IconDiscussions",
                LinkPath = "/Views/SecondaryPage.xaml?defaultPanoramaItem=PanDiscussions"
            });
            links.Add(new HomeLink()
            {
                Title = "courses",
                IconTemplate = "IconCourses",
                LinkPath = "/Views/SecondaryPage.xaml?defaultPanoramaItem=PanCourses"
            });
            links.Add(new HomeLink()
            {
                Title = "people",
                IconTemplate = "IconPeople",
                LinkPath = "/Views/SecondaryPage.xaml?defaultPanoramaItem=PanPeople"
            });
            links.Add(new HomeLink()
            {
                Title = "my profile",
                IconTemplate = "IconProfile",
                LinkPath = "/Views/ProfilePage.xaml"
            });
            links.Add(new HomeLink()
            {
                Title = "help",
                IconTemplate = "IconHelp",
                LinkPath = "/Views/HelpPage.xaml"
            });
            links.Add(new HomeLink()
            {
                Title = "settings",
                IconTemplate = "IconSettings",
                LinkPath = "/Views/SettingsPage.xaml"
            });

            HomeLinks = links;
        }
    }
}
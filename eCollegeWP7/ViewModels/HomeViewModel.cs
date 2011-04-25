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
    public class HomeViewModel : ViewModelBase
    {

        public ObservableCollection<LinkViewModel> HomeLinks { get; set; }

        public HomeViewModel()
        {

            var links = new ObservableCollection<LinkViewModel>();

            links.Add(new LinkViewModel()
            {
                Title = "activity",
                IconTemplate = "IconActivity",
                PanoramaItemName = "PanActivity"
            });
            links.Add(new LinkViewModel()
            {
                Title = "upcoming",
                IconTemplate = "IconUpcoming",
                PanoramaItemName = "PanUpcoming"
            });
            //links.Add(new LinkViewModel()
            //{
            //    Title = "upcoming",
            //    IconTemplate = "IconUpcoming",
            //    PanoramaItemName = "PanUpcoming"
            //});
            links.Add(new LinkViewModel()
            {
                Title = "discussions",
                IconTemplate = "IconDiscussions",
                NavigationPath = "/Views/SecondaryPage.xaml?defaultPanoramaItem=PanDiscussions"
            });
            links.Add(new LinkViewModel()
            {
                Title = "courses",
                IconTemplate = "IconCourses",
                NavigationPath = "/Views/SecondaryPage.xaml?defaultPanoramaItem=PanCourses"
            });
            //links.Add(new LinkViewModel()
            //{
            //    Title = "people",
            //    IconTemplate = "IconPeople",
            //    NavigationPath = "/Views/SecondaryPage.xaml?defaultPanoramaItem=PanPeople"
            //});
            links.Add(new LinkViewModel()
            {
                Title = "my profile",
                IconTemplate = "IconProfile",
                NavigationPath = "/Views/ProfilePage.xaml"
            });
            //links.Add(new LinkViewModel()
            //{
            //    Title = "help",
            //    IconTemplate = "IconHelp",
            //    NavigationPath = "/Views/HelpPage.xaml"
            //});
            //links.Add(new LinkViewModel()
            //{
            //    Title = "settings",
            //    IconTemplate = "IconSettings",
            //    NavigationPath = "/Views/SettingsPage.xaml"
            //});
            links.Add(new LinkViewModel()
            {
                Title = "sign out",
                IconTemplate = "IconSignOut",
                LinkForeground = "PhoneAlertBrush",
                LinkAction = (() => App.SignOut())
            });

            HomeLinks = links;
        }
    }
}
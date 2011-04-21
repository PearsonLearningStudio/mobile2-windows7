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
    public class ScratchThreadsViewModel : ViewModelBase
    {

        public ObservableCollection<LinkViewModel> ThreadLinks { get; set; }

        public ScratchThreadsViewModel()
        {

            var links = new ObservableCollection<LinkViewModel>();

            links.Add(new LinkViewModel()
            {
                Title = "t1",
                NavigationPath = "/Views/ThreadPage.xaml?courseId=4855840&threadId=100126516639"
            });
            links.Add(new LinkViewModel()
            {
                Title = "t2",
                NavigationPath = "/Views/ThreadPage.xaml?courseId=4855840&threadId=100126516715"
            });
            links.Add(new LinkViewModel()
            {
                Title = "t3",
                NavigationPath = "/Views/ThreadPage.xaml?courseId=4855840&threadId=100126516698"
            });
            links.Add(new LinkViewModel()
            {
                Title = "t4",
                NavigationPath = "/Views/ThreadPage.xaml?courseId=4855840&threadId=100126516678"
            });
            links.Add(new LinkViewModel()
            {
                Title = "t5",
                NavigationPath = "/Views/ThreadPage.xaml?courseId=4855840&threadId=100126516659"
            });
            links.Add(new LinkViewModel()
            {
                Title = "t6",
                NavigationPath = "/Views/ThreadPage.xaml?courseId=4855840&threadId=100126516567"
            });
            links.Add(new LinkViewModel()
            {
                Title = "t7",
                NavigationPath = "/Views/ThreadPage.xaml?courseId=4855840&threadId=100126516617"
            });
            links.Add(new LinkViewModel()
            {
                Title = "t8",
                NavigationPath = "/Views/ThreadPage.xaml?courseId=4855840&threadId=100126516593"
            });

            ThreadLinks = links;
        }
    }
}
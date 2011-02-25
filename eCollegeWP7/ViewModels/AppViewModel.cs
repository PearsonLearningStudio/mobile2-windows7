using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ECollegeAPI;
using ECollegeAPI.Model;
using System.Diagnostics;

namespace eCollegeWP7.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        private SessionViewModel _Session;
        public SessionViewModel Session
        {
            get { return _Session; }
            set { _Session = value; this.OnPropertyChanged(() => this.Session); }
        }
        
    }
}

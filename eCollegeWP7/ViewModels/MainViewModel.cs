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

namespace eCollegeWP7
{
    public class MainViewModel : ViewModelBase
    {
        private ECollegeAPI _api;
        public ECollegeAPI API { get { return _api; } }

        private AuthenticatedUser _AuthenticatedUser;
        public AuthenticatedUser AuthenticatedUser
        {
            get { return _AuthenticatedUser; }
            set { _AuthenticatedUser = value; this.OnPropertyChanged(() => this.AuthenticatedUser); }
        }

        private ObservableCollection<Course> _MyCourses;
        public ObservableCollection<Course> MyCourses
        {
            get { return _MyCourses; }
            set { _MyCourses = value; this.OnPropertyChanged(() => this.MyCourses); }
        }
        

        public MainViewModel()
        {
        }

        public void Login(string username, string password, Action<AuthenticatedUser> callback)
        {
            _api = new ECollegeAPI("ctstate",username, password, "30bb1d4f-2677-45d1-be13-339174404402");

            _api.FetchToken(t =>
            {
                Debug.WriteLine("Token is: " + t.AccessToken);
                _api.FetchMe(me =>
                {
                    this.AuthenticatedUser = me;
                    Debug.WriteLine("Current User is: " + me.FirstName + " " + me.LastName);
                    callback(me);
                });
            });
        }


    }
}
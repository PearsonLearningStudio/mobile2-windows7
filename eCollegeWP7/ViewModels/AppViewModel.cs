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
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using ECollegeAPI.Services.Users;

namespace eCollegeWP7.ViewModels
{
    public class AppViewModel : ViewModelBase
    {

        private int _PendingServiceCalls = 0;
        public int PendingServiceCalls
        {
            get { return _PendingServiceCalls; }
            set { _PendingServiceCalls = value; this.OnPropertyChanged(() => this.PendingServiceCalls); }
        }


        private User _CurrentUser;
        public User CurrentUser
        {
            get { return _CurrentUser; }
            set { _CurrentUser = value; this.OnPropertyChanged(() => this.CurrentUser); }
        }
        
        public CoursesViewModel Courses { get; set; }

        public ECollegeClient Client { get; set; }

        public AppViewModel()
        {
            Client = new ECollegeClient(AppResources.ClientString, AppResources.ClientID);
        }

        protected void Reactivate(IDictionary<string,object> state)
        {

        }

        protected void Deactivate(IDictionary<string, object> state)
        {

        }

        protected void FetchInitialUserData(Action<bool> callback)
        {
            App.BuildService(new FetchMeService()).Execute(service =>
            {
                CurrentUser = service.Result;
                Courses = new CoursesViewModel();
                Courses.Load(res => callback(true));
            });
        }

        public void Login(String grantToken, Action<bool> callback)
        {
            Client.SetupAuthentication(grantToken);
            FetchInitialUserData(callback);
        }

        public void Login(String username, String password, Action<bool> callback)
        {
            Client.SetupAuthentication(username, password);
            FetchInitialUserData(callback);
        }
        
    }
}

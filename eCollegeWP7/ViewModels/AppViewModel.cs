using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Coding4Fun.Phone.Controls;
using ECollegeAPI;
using ECollegeAPI.Model;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using ECollegeAPI.Services;
using ECollegeAPI.Services.Users;
using RestSharp;
using ECollegeAPI.Exceptions;
using eCollegeWP7.Util;

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

        private string _ServiceCacheGrantToken;
        private IsolatedStorageResponseCache _ServiceCache;


        public AppViewModel()
        {
            Client = new ECollegeClient(AppResources.ClientString, AppResources.ClientID);
            Client.UnhandledExceptionHandler = (ex) => HandleError(ex);
        }

        public void HandleError(Exception ex)
        {
            if (ex is ServerErrorException)
            {
                ShowErrorPrompt("An error occurred while communicating with the server");
            } else if (ex is ClientErrorException)
            {
                ShowErrorPrompt("An error occurred while trying to communicate with the server");
            } else if (ex is DeserializationException)
            {
                ShowErrorPrompt("An error occurred while decoding the server's response");
            } else
            {
                ShowErrorPrompt("An unknown error has occurred");
            }
#if DEBUG
            Debugger.Break();
#endif
        }

        public void ShowAlert(string title, string message)
        {
            var t = new ToastPrompt();
            t.Background = App.Current.Resources["PhoneBackgroundBrush"] as Brush;
            t.Foreground = App.Current.Resources["PhoneForegroundBrush"] as Brush;
            t.Title = title;
            t.Message = message;
            t.TextOrientation = Orientation.Vertical;
            t.Show();
        }

        public void ShowErrorPrompt( string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK);
        }

        public void Activate(IDictionary<string,object> state)
        {
            object storedCurrentUser;
            if (state.TryGetValue("CurrentUser", out storedCurrentUser))
            {
                CurrentUser = state["CurrentUser"] as User;
            }
            object storedCourses;
            if (state.TryGetValue("Courses", out storedCourses))
            {
                Courses = state["Courses"] as CoursesViewModel;
            } 
            object grantToken;
            if (state.TryGetValue("grantToken", out grantToken) && grantToken != null)
            {
                Client.SetupAuthentication(grantToken.ToString());
            }
        }

        public void Deactivate(IDictionary<string, object> state)
        {
            state["grantToken"] = Client.GrantToken;
            state["CurrentUser"] = CurrentUser;
            state["Courses"] = Courses;
        }

        public ServiceCallTask<T> BuildService<T>(T service) where T : BaseService
        {
            if ((_ServiceCacheGrantToken == null && Client.GrantToken != null) ||   //service cache not yet created for grant token
                (_ServiceCacheGrantToken != null && !_ServiceCacheGrantToken.Equals(Client.GrantToken)))  //need a new service cache since grant token changed
            {
                _ServiceCacheGrantToken = Client.GrantToken;
                _ServiceCache = new IsolatedStorageResponseCache(_ServiceCacheGrantToken);
                DoBackgroundWork(() => _ServiceCache.PurgeOldSessions());
            }
            return new ServiceCallTask<T>(Client,_ServiceCache,service);
        }

        public void DoBackgroundWork(Action work)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (s, e) => work();
            worker.RunWorkerAsync();
        }

        public void InvalidateCache(string scope)
        {
            if (_ServiceCache != null)
            {
                DoBackgroundWork(() => _ServiceCache.Invalidate(scope));
            }
        }

        public void InvalidateCache<T>(T service) where T : BaseService
        {
            if (_ServiceCache != null)
            {
                DoBackgroundWork(() => _ServiceCache.Invalidate(service.CacheScope,service.GetCacheKey()));
            }
        }

        protected void FetchInitialUserData(Action successCallback,Action<ServiceException> failureCallback)
        {
            var call = BuildService(new FetchMeService()).SetExpiration(TimeSpan.FromDays(1.0));
            call.AddFailureHandler(failureCallback);
            call.Execute(service =>
            {
                CurrentUser = service.Result;
                Courses = new CoursesViewModel();
                Courses.Load(successCallback);
            });
        }

        public void Login(String grantToken, Action successCallback, Action<ServiceException> failureCallback)
        {
            Client.SetupAuthentication(grantToken);
            FetchInitialUserData(successCallback, failureCallback);
        }

        public void Login(String username, String password, Action successCallback, Action<ServiceException> failureCallback)
        {
            Client.SetupAuthentication(username, password);
            FetchInitialUserData(successCallback, failureCallback);
        }
        
    }
}

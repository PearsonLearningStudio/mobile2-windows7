using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using eCollegeWP7.ViewModels;
using ECollegeAPI.Exceptions;
using Coding4Fun.Phone.Controls;
using eCollegeWP7.Util;
using System.Diagnostics;

namespace eCollegeWP7.Views
{
    public partial class SingleSignonPage : BasePage
    {
        private const string RedirectUrl = "http://localhost/catch_this_url.html";
        private Stack<Uri> NavigationStack = new Stack<Uri>();

        public SingleSignonPage()
            : base()
        {
            InitializeComponent();

            string url = AppResources.SSOUrl + "?redirect_url=" + RedirectUrl;

            WebSingleSignon.IsScriptEnabled = true;
            WebSingleSignon.Navigating += new EventHandler<NavigatingEventArgs>(WebSingleSignon_Navigating);
            WebSingleSignon.Navigated += new EventHandler<System.Windows.Navigation.NavigationEventArgs>(WebSingleSignon_Navigated);
            WebSingleSignon.LoadCompleted += new System.Windows.Navigation.LoadCompletedEventHandler(WebSingleSignon_LoadCompleted);
            WebSingleSignon.Source = new Uri(url, UriKind.Absolute);
        }

        void WebSingleSignon_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            App.Model.PendingServiceCalls = 0;
        }

        void WebSingleSignon_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationStack.Push(e.Uri);
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
        }

        void WebSingleSignon_Navigating(object sender, NavigatingEventArgs e)
        {
            App.Model.PendingServiceCalls++;

            if (e.Uri.ToString().StartsWith(RedirectUrl))
            {
                var queryParams = QueryStringHelper.ParseQueryString(e.Uri.Query);
                string grantToken;
                if (queryParams.TryGetValue("grant_token", out grantToken))
                {
                    e.Cancel = true;
                    App.Model.PendingServiceCalls = 0;
                    var settings = IsolatedStorageSettings.ApplicationSettings;
                    //grantToken = HttpUtility.UrlDecode(grantToken);
                    //grantToken = grantToken.Replace("%7", "|");
                    //var regex = new Regex("grant_token=([^&]*)");

                    settings["grantToken"] = grantToken;
                    settings.Save();
                    this.NavigationService.Navigate(new Uri("/Views/SplashPage.xaml", UriKind.Relative));
                }
            }
        }

        protected override void BasePageNew_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (NavigationStack.Count > 1)
            {
                NavigationStack.Pop();
                WebSingleSignon.InvokeScript("eval", "history.go(-1)");
                e.Cancel = true;
            } else
            {
                base.BasePageNew_BackKeyPress(sender,e);
            }
        }
    }
}
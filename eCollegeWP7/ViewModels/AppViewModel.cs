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
        public User CurrentUser { get; set; }

        public ECollegeClient Client { get; set; }

        public AppViewModel()
        {
            Client = new ECollegeClient(AppResources.ClientString, AppResources.ClientID);
        }


        public void Login(String grantToken, Action<bool> callback)
        {
            Client.SetupAuthentication(grantToken);
            Client.FetchMe(me =>
            {
                callback(true);
            });
        }

        public void Login(String username, String password, Action<bool> callback)
        {
            Client.SetupAuthentication(username, password);
            Client.FetchMe(me =>
            {
                callback(true);
            });
        }
        
    }
}

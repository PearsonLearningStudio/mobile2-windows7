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
        public ECollegeClient API { get; set; }

        private AuthenticatedUser _AuthenticatedUser;
        public AuthenticatedUser AuthenticatedUser
        {
            get { return _AuthenticatedUser; }
            set { _AuthenticatedUser = value; this.OnPropertyChanged(() => this.AuthenticatedUser); }
        }


        public void Login(string username, string password, Action<AuthenticatedUser> callback)
        {
            API = new ECollegeClient("ctstate", username, password, "30bb1d4f-2677-45d1-be13-339174404402");

            API.FetchToken(t =>
            {
                Debug.WriteLine("Token is: " + t.AccessToken);
                API.FetchMe(me =>
                {
                    this.AuthenticatedUser = me;
                    Debug.WriteLine("Current User is: " + me.FirstName + " " + me.LastName);
                    callback(me);
                });
            });
        }
    }
}

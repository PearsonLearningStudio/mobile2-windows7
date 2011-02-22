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

        private User _User;
        public User User
        {
            get { return _User; }
            set { _User = value; this.OnPropertyChanged(() => this.User); }
        }


        public void Login(string username, string password, Action<User> callback)
        {
            API = new ECollegeClient("ctstate", username, password, "30bb1d4f-2677-45d1-be13-339174404402");

            API.FetchToken(t =>
            {
                Debug.WriteLine("Token is: " + t.AccessToken);
                API.FetchMe(me =>
                {
                    this.User = me;
                    Debug.WriteLine("Current User is: " + me.FirstName + " " + me.LastName);
                    callback(me);
                });
            });
        }
    }
}

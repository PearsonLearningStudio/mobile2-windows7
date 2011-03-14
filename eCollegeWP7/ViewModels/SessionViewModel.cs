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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;

namespace eCollegeWP7.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {

        public SessionViewModel()
            : base()
        {
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public User CurrentUser { get; set; }

        private Token _CurrentToken;
        public Token CurrentToken
        {
            get
            {
                return _CurrentToken;
            }
            set
            {
                _CurrentToken = value;
                if (_CurrentToken != null)
                {
                    _Client = new ECollegeClient(value);
                }
            }
        }

        private ECollegeClient _Client;
        public ECollegeClient Client() { return _Client; }

        public void Login(Action<bool> callback)
        {
            _Client = new ECollegeClient(AppResources.ClientString, Username, Password, AppResources.ClientID);
            Client().FetchToken(t =>
            {
                this.CurrentToken = t;
                if (this.CurrentUser == null)
                {
                    Client().FetchMe(me =>
                    {
                        this.CurrentUser = me;
                        Debug.WriteLine("Current User is: " + me.FirstName + " " + me.LastName);
                        callback(true);
                    });
                }
                else
                {
                    callback(true);
                }
            });
        }

    }
}

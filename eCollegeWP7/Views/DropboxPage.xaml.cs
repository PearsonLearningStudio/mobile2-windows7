using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using ECollegeAPI.Model;
using eCollegeWP7.Util;

namespace eCollegeWP7.Views
{
    public partial class DropboxPage : PhoneApplicationPage
    {
        protected DropboxViewModel Model { get { return this.DataContext as DropboxViewModel; } }

        public DropboxPage()
        {
            InitializeComponent();
        } 

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;
            this.DataContext = new DropboxViewModel(Convert.ToInt64(parameters["courseId"]),Convert.ToInt64(parameters["basketId"]));
        }

    }
}
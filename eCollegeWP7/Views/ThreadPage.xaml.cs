using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using eCollegeWP7.ViewModels;
using System.Diagnostics;

namespace eCollegeWP7.Views
{
    public partial class ThreadPage : BasePage
    {
        public ThreadViewModel Model { get { return this.DataContext as ThreadViewModel; } }

        public ThreadPage()
            : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            long courseId = Int64.Parse(parameters["courseId"]);
            long threadId = Int64.Parse(parameters["threadId"]);
            this.DataContext = new ThreadViewModel(courseId,threadId);
        }

        private void BtnOpenTopic_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
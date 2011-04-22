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
    public partial class HtmlContentPage : BasePage
    {
        public HtmlContentViewModel Model { get { return this.DataContext as HtmlContentViewModel; } }

        public HtmlContentPage()
            : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            long courseId = Int64.Parse(parameters["courseId"]);
            long htmlId = Int64.Parse(parameters["htmlId"]);
            string title = parameters["title"];
            this.DataContext = new HtmlContentViewModel(courseId, htmlId, title);
        }
    }
}
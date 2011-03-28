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
using eCollegeWP7.ViewModels;

namespace eCollegeWP7.Views
{
    public partial class DropboxMessagePage : BasePage
    {
        protected DropboxMessageViewModel Model { get { return this.DataContext as DropboxMessageViewModel; } }

        public DropboxMessagePage()
            : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;
            this.DataContext = new DropboxMessageViewModel(Convert.ToInt64(parameters["courseId"]), Convert.ToInt64(parameters["basketId"]), Convert.ToInt64(parameters["messageId"]));
            this.LblTitle.Text = parameters["title"];
        }

        private void BtnViewAllCourseSubmissions_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/CourseDropboxMessagesPage.xaml?courseId=" + Model.CourseID, UriKind.Relative));
        }

    }
}
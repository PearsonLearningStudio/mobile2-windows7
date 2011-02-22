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

namespace eCollegeWP7.Views
{
    public partial class CoursePage : PhoneApplicationPage
    {
        public CourseViewModel Model { get { return this.DataContext as CourseViewModel; } }

        public CoursePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            IDictionary<string,string> parameters = this.NavigationContext.QueryString;

            int courseId = Int32.Parse(parameters["courseId"]);
            this.DataContext = new CourseViewModel(courseId);
        }

        private void BtnDiscussion_Click(object sender, RoutedEventArgs e)
        {
            var theader = (sender as Button).DataContext as DiscussionTopicHeader;
            this.NavigationService.Navigate(new Uri("/Views/DiscussionPage.xaml?topicId=" + theader.Topic.ID + "&topicHeaderId=" + theader.ID, UriKind.Relative));
        }

        private void BtnDropboxBasket_Click(object sender, RoutedEventArgs e)
        {
            var BtnDropboxBasket = sender as Button;
            var basket = BtnDropboxBasket.DataContext as DropboxBasket;
            this.NavigationService.Navigate(new Uri("/Views/DropboxPage.xaml?courseId=" + Model.CourseID + "&basketId=" + basket.ID, UriKind.Relative));
        }

    }
}
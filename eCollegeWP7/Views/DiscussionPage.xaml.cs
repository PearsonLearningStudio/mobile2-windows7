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
    public partial class DiscussionPage : PhoneApplicationPage
    {
        protected Grid GrdResponse { get { return this.FindVisualChild<Grid>("GrdResponse"); } }
        protected Button BtnShowAddResponse { get { return this.FindVisualChild<Button>("BtnShowAddResponse"); } }
        protected Button BtnAddResponse { get { return this.FindVisualChild<Button>("BtnAddResponse"); } }
        protected TextBox TxtResponse { get { return this.FindVisualChild<TextBox>("TxtResponse"); } }
        protected TextBox TxtResponseTitle { get { return this.FindVisualChild<TextBox>("TxtResponseTitle"); } }
        protected DiscussionViewModel Model { get { return this.DataContext as DiscussionViewModel; } }

        public DiscussionPage()
        {
            InitializeComponent();
        } 

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            if (parameters.ContainsKey("topicHeaderId"))
            {
                this.DataContext = new DiscussionViewModel(parameters["topicHeaderId"],parameters["topicId"],DiscussionType.TopicAndResponses);
            } else if (parameters.ContainsKey("responseHeaderId")) {
                this.DataContext = new DiscussionViewModel(parameters["responseHeaderId"],parameters["responseId"],DiscussionType.ResponseAndResponses);
            }

        }

        private void BtnResponse_Click(object sender, RoutedEventArgs e)
        {
            var rheader = (sender as Button).DataContext as DiscussionResponseHeader;
            this.NavigationService.Navigate(new Uri("/Views/DiscussionPage.xaml?responseId=" + rheader.Response.ID + "&responseHeaderId=" + rheader.ID, UriKind.Relative));
        }

        private void BtnShowAddResponse_Click(object sender, RoutedEventArgs e)
        {
            GrdResponse.Visibility = Visibility.Visible;
            BtnShowAddResponse.Visibility = Visibility.Collapsed;
        }

        private void BtnCancelResponse_Click(object sender, RoutedEventArgs e)
        {
            GrdResponse.Visibility = Visibility.Collapsed;
            BtnShowAddResponse.Visibility = Visibility.Visible;
        }

        private void BtnAddResponse_Click(object sender, RoutedEventArgs e)
        {
            GrdResponse.Visibility = Visibility.Collapsed;
            BtnShowAddResponse.Visibility = Visibility.Visible;
            var responseTitle = TxtResponseTitle.Text;
            TxtResponseTitle.Text = "";
            var responseText = TxtResponse.Text;
            TxtResponse.Text = "";
            Model.PostResponse(responseTitle, responseText);
        }

    }
}
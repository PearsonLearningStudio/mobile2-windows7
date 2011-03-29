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
using System.Windows.Controls.Primitives;

namespace eCollegeWP7.Views
{
    public partial class DiscussionPage : BasePage
    {
        protected Grid GrdResponse { get { return this.FindVisualChild<Grid>("GrdResponse"); } }
        protected Button BtnShowPostResponse { get { return this.FindVisualChild<Button>("BtnShowPostResponse"); } }
        protected Button BtnPostResponse { get { return this.FindVisualChild<Button>("BtnPostResponse"); } }
        protected TextBox TxtResponse { get { return this.FindVisualChild<TextBox>("TxtResponse"); } }
        protected TextBox TxtResponseTitle { get { return this.FindVisualChild<TextBox>("TxtResponseTitle"); } }
        protected DiscussionViewModel Model { get { return this.DataContext as DiscussionViewModel; } }

        public DiscussionPage() : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            if (parameters.ContainsKey("topicId"))
            {
                this.DataContext = new DiscussionViewModel(parameters["topicId"],DiscussionType.TopicAndResponses);
            } else if (parameters.ContainsKey("responseId")) {
                this.DataContext = new DiscussionViewModel(parameters["responseId"],DiscussionType.ResponseAndResponses);
            }
            Model.FetchResponses();
        }

        private void BtnResponse_Click(object sender, RoutedEventArgs e)
        {
            var dvm = (sender as Button).DataContext as DiscussionViewModel;
            this.NavigationService.Navigate(new Uri(dvm.NavigationPath, UriKind.Relative));
        }

        private void BtnShowPostResponse_Click(object sender, RoutedEventArgs e)
        {
            GrdResponse.Visibility = Visibility.Visible;
            BtnShowPostResponse.Visibility = Visibility.Collapsed;

            var scroller = this.FindVisualChild<ScrollViewer>();

            if (scroller != null)
            {
                var t = BtnShowPostResponse.TransformToVisual(scroller);
                var offset = t.Transform(new Point(0, 0));

                scroller.ScrollToVerticalOffset(offset.Y - 30);
            }

            //TxtResponseTitle.Focus();
        }

        private void BtnCancelResponse_Click(object sender, RoutedEventArgs e)
        {
            GrdResponse.Visibility = Visibility.Collapsed;
            BtnShowPostResponse.Visibility = Visibility.Visible;
        }

        private void BtnPostResponse_Click(object sender, RoutedEventArgs e)
        {
            GrdResponse.Visibility = Visibility.Collapsed;
            BtnShowPostResponse.Visibility = Visibility.Visible;
            var responseTitle = TxtResponseTitle.Text;
            TxtResponseTitle.Text = "";
            var responseText = TxtResponse.Text;
            TxtResponse.Text = "";
            Model.PostResponse(responseTitle, responseText);
        }

        private object _originalDiscussionOpacityMask;
        private object _originalDiscussionMaxHeight;

        private void TglExpandDescription_Click(object sender, RoutedEventArgs e)
        {
            var TglExpandDescription = sender as ToggleButton;
            var BdrDescription = this.FindVisualChild<Border>("BdrDescription");
            var LblDescription = this.FindVisualChild<TextBlock>("LblDescription");

            if (TglExpandDescription.IsChecked.Value)
            {
                _originalDiscussionOpacityMask = BdrDescription.GetValue(Border.OpacityMaskProperty);
                _originalDiscussionMaxHeight = LblDescription.GetValue(TextBlock.MaxHeightProperty);
                BdrDescription.SetValue(Border.OpacityMaskProperty, DependencyProperty.UnsetValue);
                LblDescription.SetValue(TextBlock.MaxHeightProperty, DependencyProperty.UnsetValue);
            } else {
                BdrDescription.SetValue(Border.OpacityMaskProperty, _originalDiscussionOpacityMask);
                LblDescription.SetValue(TextBlock.MaxHeightProperty, _originalDiscussionMaxHeight);
            }
        }

    }
}
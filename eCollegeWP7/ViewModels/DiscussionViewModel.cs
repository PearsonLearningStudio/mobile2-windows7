using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using ECollegeAPI.Model;
using eCollegeWP7.Util;

namespace eCollegeWP7.ViewModels
{
    public enum DiscussionType
    {
        TopicAndResponses,
        ResponseAndResponses
    }

    public class DiscussionViewModel : ViewModelBase
    {
        private string _TopicHeaderID;
        public string TopicHeaderID
        {
            get { return _TopicHeaderID; }
            set { _TopicHeaderID = value; this.OnPropertyChanged(() => this.TopicHeaderID); }
        }

        private string _TopicID;
        public string TopicID { 
            get { return _TopicID; }
            set { _TopicID = value; this.OnPropertyChanged(()=>this.TopicID); }
        }

        private DiscussionTopicHeader _TopicHeader;
        public DiscussionTopicHeader TopicHeader
        {
            get { return _TopicHeader; }
            set { _TopicHeader = value; this.OnPropertyChanged(() => this.TopicHeader); }
        }

        private string _ResponseID;
        public string ResponseID
        {
            get { return _ResponseID; }
            set { _ResponseID = value; this.OnPropertyChanged(() => this.ResponseID); }
        }

        private string _ResponseHeaderID;
        public string ResponseHeaderID
        {
            get { return _ResponseHeaderID; }
            set { _ResponseHeaderID = value; this.OnPropertyChanged(() => this.ResponseHeaderID); }
        }

        private DiscussionResponseHeader _ResponseHeader;
        public DiscussionResponseHeader ResponseHeader
        {
            get { return _ResponseHeader; }
            set { _ResponseHeader = value; this.OnPropertyChanged(() => this.ResponseHeader); }
        }

        private ObservableCollection<DiscussionResponseHeader> _Responses;
        public ObservableCollection<DiscussionResponseHeader> Responses
        {
            get { return _Responses; }
            set { _Responses = value; this.OnPropertyChanged(() => this.Responses); }
        }

        private DiscussionType _CurrentDiscussionType;
        public DiscussionType CurrentDiscussionType
        {
            get { return _CurrentDiscussionType; }
            set { _CurrentDiscussionType = value; this.OnPropertyChanged(() => this.CurrentDiscussionType); }
        }

        private string _DiscussionTitle;
        public string DiscussionTitle
        {
            get { return _DiscussionTitle; }
            set { _DiscussionTitle = value; this.OnPropertyChanged(() => this.DiscussionTitle); }
        }

        private string _DiscussionDescription;
        public string DiscussionDescription
        {
            get { return _DiscussionDescription; }
            set { _DiscussionDescription = value; this.OnPropertyChanged(() => this.DiscussionDescription); }
        }

        private long _DiscussionResponseCount;
        public long DiscussionResponseCount
        {
            get { return _DiscussionResponseCount; }
            set { _DiscussionResponseCount = value; this.OnPropertyChanged(() => this.DiscussionResponseCount); }
        }

        public DiscussionViewModel(string discussionHeaderId, string discussionId, DiscussionType dt)
        {
            CurrentDiscussionType = dt;

            if (CurrentDiscussionType == DiscussionType.TopicAndResponses)
            {
                this.TopicHeaderID = discussionHeaderId;
                this.TopicID = discussionId;

                AppViewModel.Client.FetchMyDiscussionTopicById(discussionHeaderId, (result) =>
                {
                    this.TopicHeader = result;
                    this.DiscussionTitle = result.Topic.Title;
                    this.DiscussionDescription = result.Topic.Description;
                    this.DiscussionResponseCount = result.ChildResponseCounts.TotalResponseCount;
                });
            }

            if (CurrentDiscussionType == DiscussionType.ResponseAndResponses)
            {
                this.ResponseHeaderID = discussionHeaderId;
                this.ResponseID = discussionId;

                AppViewModel.Client.FetchMyDiscussionResponseById(discussionHeaderId, (result) =>
                {
                    this.ResponseHeader = result;
                    this.DiscussionTitle = result.Response.Title;
                    this.DiscussionDescription = result.Response.Description;
                    this.DiscussionResponseCount = result.ChildResponseCounts.TotalResponseCount;
                });
            }

            FetchResponses();
        }

        public void PostResponse(string responseTitle, string responseText)
        {
            if (CurrentDiscussionType == DiscussionType.TopicAndResponses)
            {
                AppViewModel.Client.PostMyResponseToTopic(this.TopicID, responseTitle, responseText, (result) =>
                {
                    if (result.ResponseStatus == RestSharp.ResponseStatus.Completed)
                    {
                        FetchResponses();
                    }
                });
            }
            else if (CurrentDiscussionType == DiscussionType.ResponseAndResponses)
            {
                AppViewModel.Client.PostMyResponseToResponse(this.ResponseID, responseTitle, responseText, (result) =>
                {
                    if (result.ResponseStatus == RestSharp.ResponseStatus.Completed)
                    {
                        FetchResponses();
                    }
                });
            }
        }

        protected void FetchResponses()
        {
            if (CurrentDiscussionType == DiscussionType.TopicAndResponses)
            {
                AppViewModel.Client.FetchMyDiscussionResponsesByTopic(TopicID, (result) =>
                {
                    var formattedResult = new ObservableCollection<DiscussionResponseHeader>();
                    foreach (var r in result) formattedResult.Add(r);
                    this.Responses = formattedResult;
                });
            }
            else if (CurrentDiscussionType == DiscussionType.ResponseAndResponses)
            {
                AppViewModel.Client.FetchMyDiscussionResponsesByResponse(ResponseID, (result) =>
                {
                    var formattedResult = new ObservableCollection<DiscussionResponseHeader>();
                    foreach (var r in result) formattedResult.Add(r);
                    this.Responses = formattedResult;
                });
            }

        }

    }
}
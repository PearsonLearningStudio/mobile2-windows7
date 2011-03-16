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
using ECollegeAPI.Services.Discussions;

namespace eCollegeWP7.ViewModels
{
    public enum DiscussionType
    {
        TopicAndResponses,
        ResponseAndResponses
    }

    public class DiscussionViewModel : ViewModelBase
    {
        private string _UserTopicID;
        public string UserTopicID
        {
            get { return _UserTopicID; }
            set { _UserTopicID = value; this.OnPropertyChanged(() => this.UserTopicID); }
        }

        private string _TopicID;
        public string TopicID { 
            get { return _TopicID; }
            set { _TopicID = value; this.OnPropertyChanged(()=>this.TopicID); }
        }

        private UserDiscussionTopic _UserTopic;
        public UserDiscussionTopic UserTopic
        {
            get { return _UserTopic; }
            set { _UserTopic = value; this.OnPropertyChanged(() => this.UserTopic); }
        }

        private string _ResponseID;
        public string ResponseID
        {
            get { return _ResponseID; }
            set { _ResponseID = value; this.OnPropertyChanged(() => this.ResponseID); }
        }

        private string _UserResponseID;
        public string UserResponseID
        {
            get { return _UserResponseID; }
            set { _UserResponseID = value; this.OnPropertyChanged(() => this.UserResponseID); }
        }

        private UserDiscussionResponse _UserResponse;
        public UserDiscussionResponse UserResponse
        {
            get { return _UserResponse; }
            set { _UserResponse = value; this.OnPropertyChanged(() => this.UserResponse); }
        }

        private ObservableCollection<UserDiscussionResponse> _Responses;
        public ObservableCollection<UserDiscussionResponse> Responses
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

        public DiscussionViewModel(string discussionId, DiscussionType dt)
        {
            CurrentDiscussionType = dt;

            if (CurrentDiscussionType == DiscussionType.TopicAndResponses)
            {
                this.TopicID = discussionId;
                this.UserTopicID = AppViewModel.CurrentUser.ID + "-" + this.TopicID;

                App.BuildService(new FetchMyDiscussionTopicByIdService(UserTopicID)).Execute(service =>
                {
                    this.UserTopic = service.Result;
                    this.DiscussionTitle = service.Result.Topic.Title;
                    this.DiscussionDescription = service.Result.Topic.Description;
                    this.DiscussionResponseCount = service.Result.ChildResponseCounts.TotalResponseCount;
                });
            }

            if (CurrentDiscussionType == DiscussionType.ResponseAndResponses)
            {
                this.ResponseID = discussionId;
                this.UserResponseID = AppViewModel.CurrentUser.ID + "-" + this.ResponseID;

                App.BuildService(new FetchMyDiscussionResponseByIdService(UserResponseID)).Execute(service =>
                {
                    this.UserResponse = service.Result;
                    this.DiscussionTitle = service.Result.Response.Title;
                    this.DiscussionDescription = service.Result.Response.Description;
                    this.DiscussionResponseCount = service.Result.ChildResponseCounts.TotalResponseCount;
                });
            }

            FetchResponses();
        }

        public void PostResponse(string responseTitle, string responseText)
        {
            if (CurrentDiscussionType == DiscussionType.TopicAndResponses)
            {
                App.BuildService(new PostMyResponseToTopicService(this.TopicID, responseTitle, responseText)).Execute(
                    service => FetchResponses());
            }
            else if (CurrentDiscussionType == DiscussionType.ResponseAndResponses)
            {
                App.BuildService(new PostMyResponseToResponseService(this.ResponseID, responseTitle, responseText)).Execute(
                    service => FetchResponses());
            }
        }

        protected void FetchResponses()
        {
            if (CurrentDiscussionType == DiscussionType.TopicAndResponses)
            {
                App.BuildService(new FetchMyDiscussionResponsesByTopicService(TopicID)).Execute(service =>
                {
                    var formattedResult = new ObservableCollection<UserDiscussionResponse>();
                    foreach (var r in service.Result) formattedResult.Add(r);
                    this.Responses = formattedResult;
                });
            }
            else if (CurrentDiscussionType == DiscussionType.ResponseAndResponses)
            {
                App.BuildService(new FetchMyDiscussionResponsesByResponseService(ResponseID)).Execute(service =>
                {
                    var formattedResult = new ObservableCollection<UserDiscussionResponse>();
                    foreach (var r in service.Result) formattedResult.Add(r);
                    this.Responses = formattedResult;
                });
            }

        }

    }
}
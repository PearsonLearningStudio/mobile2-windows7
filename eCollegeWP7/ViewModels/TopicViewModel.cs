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

namespace eCollegeWP7
{
    public class TopicViewModel : ViewModelBase
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

        private ObservableCollection<DiscussionResponseHeader> _Responses;
        public ObservableCollection<DiscussionResponseHeader> Responses
        {
            get { return _Responses; }
            set { _Responses = value; this.OnPropertyChanged(() => this.Responses); }
        }

        public TopicViewModel(string topicHeaderID, string topicID)
        {
            this.TopicHeaderID = topicHeaderID;
            this.TopicID = topicID;
            //App.ViewModel.API.FetchAnnouncements(courseId, (result) =>
            //{
            //    var formattedResult = new ObservableCollection<Announcement>();
            //    foreach (var ann in result)
            //    {
            //        formattedResult.Add(ann);
            //    }
            //    this.Announcements = formattedResult;
            //});

            App.ViewModel.API.FetchMyDiscussionTopicById(topicHeaderID, (result) =>
            {
                this.TopicHeader = result;
            });

            App.ViewModel.API.FetchMyDiscussionResponsesByTopic(topicID, (result) =>
            {
                var formattedResult = new ObservableCollection<DiscussionResponseHeader>();
                foreach (var r in result) formattedResult.Add(r);
                this.Responses = formattedResult;
            });
        }

    }
}
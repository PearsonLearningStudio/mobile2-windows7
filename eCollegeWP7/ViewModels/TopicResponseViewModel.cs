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
    public class TopicResponseViewModel : ViewModelBase
    {

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

        public TopicResponseViewModel(string responseHeaderID, string responseID)
        {
            this.ResponseHeaderID = responseHeaderID;
            this.ResponseID = responseID;

            App.ViewModel.API.FetchMyDiscussionResponseById(responseHeaderID, (result) =>
            {
                this.ResponseHeader = result;
            });
            App.ViewModel.API.FetchMyDiscussionResponsesByResponse(responseID, (result) =>
            {
                var formattedResult = new ObservableCollection<DiscussionResponseHeader>();
                foreach (var r in result) formattedResult.Add(r);
                this.Responses = formattedResult;
            });
        }

    }
}
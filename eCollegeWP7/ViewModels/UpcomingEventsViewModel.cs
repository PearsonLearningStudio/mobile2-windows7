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
using System.Linq;
using ECollegeAPI.Services.Discussions;
using ECollegeAPI.Services.Upcoming;

namespace eCollegeWP7.ViewModels
{
    public class UpcomingEventsViewModel : ViewModelBase
    {
       private bool _CanLoadMore = false;
        public bool CanLoadMore
        {
            get { return _CanLoadMore; }
            set { _CanLoadMore = value; this.OnPropertyChanged(() => this.CanLoadMore); }
        }

        public bool AllLoaded { get; set; }
        public bool LoadStarted { get; set; }

        protected BackgroundWorker _loadingWorker;
        protected string _typeFilter;

        private List<Group<UpcomingEventViewModel>> _UpcomingEventsByDate;
        public List<Group<UpcomingEventViewModel>> UpcomingEventsByDate
        {
            get { return _UpcomingEventsByDate; }
            set { _UpcomingEventsByDate = value; this.OnPropertyChanged(() => this.UpcomingEventsByDate); }
        }
        
        public UpcomingEventsViewModel()
        {
        }

        public void Load(bool all)
        {
            Load(all,null);
        }

        public void Load(bool all, Action<bool> callback)
        {
            LoadStarted = true;
            DateTime? until = null;

            if (all)
            {
                CanLoadMore = false;
                AllLoaded = true;
            }
            else
            {
                until = DateTime.Today.AddDays(14.0);
            }

            App.Model.BuildService(new FetchMyUpcomingEventsService(until)).Execute((service) =>
            {
                _loadingWorker = new BackgroundWorker();
                _loadingWorker.DoWork += (s, e) =>
                {
                    var res = (from t in service.Result where t.EventType != UpcomingEventType.Ignored
                               group new UpcomingEventViewModel(t) by UpcomingEventViewModel.ParseDateGroup(t)
                               into r
                               select new Group<UpcomingEventViewModel>(r.Key, r)).ToList();

                    e.Result = res;
                };
                _loadingWorker.RunWorkerCompleted += (s, e) =>
                {

                    this.UpcomingEventsByDate = (List<Group<UpcomingEventViewModel>>) e.Result;
                    this.CanLoadMore = all ? false : true;
                    _loadingWorker = null;
                    if (callback != null) callback(true);
                };
                _loadingWorker.RunWorkerAsync();
            });
        }
    }
}
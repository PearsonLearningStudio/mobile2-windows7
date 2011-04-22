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

        private ObservableCollection<UpcomingEventViewModel> _UpcomingEvents;
        public ObservableCollection<UpcomingEventViewModel> UpcomingEvents
        {
            get { return _UpcomingEvents; }
            set { _UpcomingEvents = value; this.OnPropertyChanged(() => this.UpcomingEvents); }
        }

        private ObservableCollection<GroupedObservableCollection<UpcomingEventViewModel>> _UpcomingEventsGroup;
        public ObservableCollection<GroupedObservableCollection<UpcomingEventViewModel>> UpcomingEventsGroup
        {
            get { return _UpcomingEventsGroup; }
            set { _UpcomingEventsGroup = value; this.OnPropertyChanged(() => this.UpcomingEventsGroup); }
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
                    ObservableCollection<UpcomingEventViewModel> data = new ObservableCollection<UpcomingEventViewModel>();
                    foreach (var item in service.Result)
                    {
                        data.Add(new UpcomingEventViewModel(item));
                    }
                    e.Result = data;
                };
                _loadingWorker.RunWorkerCompleted += (s, e) =>
                {
                    this.UpcomingEvents = e.Result as ObservableCollection<UpcomingEventViewModel>;
                    this.UpcomingEventsGroup = this.UpcomingEvents.ToSingleGroupedObservableCollection();
                    this.CanLoadMore = all ? false : true;
                    _loadingWorker = null;
                    if (callback != null) callback(true);
                };
                _loadingWorker.RunWorkerAsync();
            });
        }
    }
}
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

namespace eCollegeWP7.ViewModels
{
    public class ActivitiesViewModel : ViewModelBase
    {
        private ObservableCollection<ActivityViewModel> _Activities;
        public ObservableCollection<ActivityViewModel> Activities
        {
            get { return _Activities; }
            set { _Activities = value; this.OnPropertyChanged(() => this.Activities); }
        }

        public void Load()
        {
            Load(null);
        }

        public void Load(Action<bool> callback)
        {
            if (Activities != null)
            {
                if (callback != null) callback(true);
                return;
            }

            AppViewModel.Client.FetchMyWhatsHappeningFeed( (tresult) =>
            {
                ObservableCollection<ActivityViewModel> data = new ObservableCollection<ActivityViewModel>();

                foreach (var item in tresult)
                {
                    data.Add(new ActivityViewModel(item));
                }
                this.Activities = data;
                if (callback != null) callback(true);
            });
        }

    }
}
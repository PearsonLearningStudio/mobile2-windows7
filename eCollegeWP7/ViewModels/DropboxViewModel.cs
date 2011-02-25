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

namespace eCollegeWP7
{

    public class DropboxViewModel : ViewModelBase
    {
        private ObservableCollection<DropboxBasketMessage> _Messages;
        public ObservableCollection<DropboxBasketMessage> Messages
        {
            get { return _Messages; }
            set { _Messages = value; this.OnPropertyChanged(() => this.Messages); }
        }

        public DropboxViewModel(long courseId, long basketId)
        {
            AppViewModel.Session.Client().FetchDropboxBasketMessages(AppViewModel.Session.CurrentUser.ID,courseId, basketId, result =>
            {

                //this.Messages = (from m in result where m.SubmissionStudent.ID == AppViewModel.User.ID select m).ToList().ToObservableCollection();
                this.Messages = result.ToObservableCollection();
            });
        }

    }
}
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

    public class DropboxMessageViewModel : ViewModelBase
    {

        private DropboxMessage _Message;
        public DropboxMessage Message
        {
            get { return _Message; }
            set { _Message = value; this.OnPropertyChanged(() => this.Message); }
        }

        public DropboxMessageViewModel(long courseId, long basketId, long messageId)
        {
            AppViewModel.Client.FetchDropboxMessage(courseId, basketId, messageId, result =>
            {
                this.Message = result;
            });
        }

    }
}
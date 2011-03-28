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
    public class LinkViewModel : ViewModelBase
    {

        public LinkViewModel()
        {
            LinkForeground = "PhoneRegularBrush";
        }

        private string _IconTemplate;
        public string IconTemplate
        {
            get { return _IconTemplate; }
            set { _IconTemplate = value; this.OnPropertyChanged(() => this.IconTemplate); }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; this.OnPropertyChanged(() => this.Title); }
        }

        private string _LinkForeground;
        public string LinkForeground
        {
            get { return _LinkForeground; }
            set { _LinkForeground = value; this.OnPropertyChanged(() => this.LinkForeground); }
        }

        private Action _LinkAction;
        public Action LinkAction
        {
            get { return _LinkAction; }
            set { _LinkAction = value; this.OnPropertyChanged(() => this.LinkAction); }
        }

        private string _NavigationPath;
        public string NavigationPath
        {
            get { return _NavigationPath; }
            set { _NavigationPath = value; this.OnPropertyChanged(() => this.NavigationPath); }
        }

        private string _PanoramaItemName;
        public string PanoramaItemName
        {
            get { return _PanoramaItemName; }
            set { _PanoramaItemName = value; this.OnPropertyChanged(() => this.PanoramaItemName); }
        }
        
    }
}
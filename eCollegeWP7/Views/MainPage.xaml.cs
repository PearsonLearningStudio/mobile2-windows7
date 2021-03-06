﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Diagnostics;
using System.Collections.ObjectModel;
using ECollegeAPI.Model;
using eCollegeWP7.Util;
using eCollegeWP7.Exceptions;
using eCollegeWP7.ViewModels;

namespace eCollegeWP7.Views
{
    public partial class MainPage : BasePage
    {
        public MainViewModel Model { get { return this.DataContext as MainViewModel; } }
        protected bool _alreadyNavigatedTo = false;

        // Constructor
        public MainPage() : base()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = new MainViewModel();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (!_alreadyNavigatedTo)
            {
                IDictionary<string, string> parameters = this.NavigationContext.QueryString;

                string defaultPanoramaItem;

                if (!parameters.TryGetValue("defaultPanoramaItem", out defaultPanoramaItem))
                {
                    defaultPanoramaItem = "PanHome";
                }
                var defaultItem = PanMain.FindName(defaultPanoramaItem) as PanoramaItem;
                PanMain.DefaultItem = defaultItem;
                UpdateSelectedPanoramaItem(defaultItem);
                _alreadyNavigatedTo = true;
            } else
            {
                //quick hack until i can figure out why the list isn't loading on back button
                var oldActivitiesVM = PanActivity.DataContext as ActivitiesViewModel;
                var newActivitiesVM = new ActivitiesViewModel();
                if (oldActivitiesVM.LoadStarted) newActivitiesVM.Load(oldActivitiesVM.AllLoaded);
                PanActivity.DataContext = newActivitiesVM;

                var oldUpcomingEventsVM = PanUpcoming.DataContext as UpcomingEventsViewModel;
                var newUpcomingEventsVM = new UpcomingEventsViewModel();
                if (oldUpcomingEventsVM.LoadStarted) newUpcomingEventsVM.Load(oldUpcomingEventsVM.AllLoaded);
                PanUpcoming.DataContext = newUpcomingEventsVM;
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as HyperlinkButton;
            var link = btn.DataContext as LinkViewModel;

            if (link.NavigationPath != null)
            {
                this.NavigationService.Navigate(new Uri(link.NavigationPath, UriKind.Relative));
            }
            else if (link.LinkAction != null)
            {
                link.LinkAction();
            }
            else
            {
                var defaultItem = PanMain.FindName(link.PanoramaItemName);
                PanMain.DefaultItem = defaultItem;
            }
        }

        private void PanMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSelectedPanoramaItem(PanMain.SelectedItem as PanoramaItem);
        }

        protected void UpdateSelectedPanoramaItem(PanoramaItem selectedItem)
        {
            if (selectedItem != null)
            {
                if (selectedItem.Name == "PanActivity")
                {
                    var activitiesVM = (ActivitiesViewModel)selectedItem.DataContext;
                    if (!activitiesVM.LoadStarted)
                    {
                        activitiesVM.Load(false);
                        selectedItem.DataContext = activitiesVM;
                    }
                } else if (selectedItem.Name == "PanUpcoming")
                {
                    var upcomingEventsVM = (UpcomingEventsViewModel)selectedItem.DataContext;
                    if (!upcomingEventsVM.LoadStarted)
                    {
                        upcomingEventsVM.Load(false);
                        selectedItem.DataContext = upcomingEventsVM;
                    }
                }
            }
        }

        private void BtnActivity_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var vm = btn.DataContext as ActivityViewModel;

            if (vm.NavigationPath != null)
            {
                this.NavigationService.Navigate(new Uri(vm.NavigationPath, UriKind.Relative));
            }
        }

        private void BtnLoadMore_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var activitiesVM = (ActivitiesViewModel)LstActivity.DataContext;
            activitiesVM.Load(true);
        }

        private void BtnUpcomingEvent_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var vm = btn.DataContext as UpcomingEventViewModel;

            if (vm.NavigationPath != null)
            {
                this.NavigationService.Navigate(new Uri(vm.NavigationPath, UriKind.Relative));
            }
        }

        private void BtnLoadMoreUpcoming_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var upcomingEventsVM = (UpcomingEventsViewModel)LstUpcoming.DataContext;
            upcomingEventsVM.Load(true);
        }
    }
}
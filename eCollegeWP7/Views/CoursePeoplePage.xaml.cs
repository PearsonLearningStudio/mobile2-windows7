using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ECollegeAPI.Model;
using eCollegeWP7.Util;
using eCollegeWP7.ViewModels;

namespace eCollegeWP7.Views
{
    public partial class CoursePeoplePage : BasePage
    {
        public PeopleViewModel Model { get { return this.DataContext as PeopleViewModel; } }

        public CoursePeoplePage()
            : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            int courseId = Int32.Parse(parameters["courseId"]);
            this.DataContext = new PeopleViewModel(courseId);
        }

        private void LspFilterPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var LspFilterPeople = sender as ListPicker;

            if (LspFilterPeople.Tag == null)
            {
                LspFilterPeople.Tag = "NotFirstInit";
                var lspItems = Model.Roles;
                if (!Model.RoleFilter.Equals("all") && lspItems != null)
                {
                    for (int i = 0; i < lspItems.Count; i++)
                    {
                        if (lspItems[i].Equals(Model.RoleFilter))
                        {
                            LspFilterPeople.SelectedIndex = i;
                            LspFilterPeople.SelectedItem = lspItems[i];
                            return;
                        }
                    }
                }
            }
            Model.RoleFilter = LspFilterPeople.SelectedItem as string;
        }

        private void PnlListHeader_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as StackPanel).DataContext = Model;
        }

        private void BtnPerson_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var rosterUser = btn.DataContext as RosterUser;

            this.NavigationService.Navigate(new Uri("/Views/PersonPage.xaml?courseId=" + Model.CourseID + "&displayName=" + rosterUser.DisplayName + "&friendlyRole=" + rosterUser.FriendlyRole, UriKind.Relative));
        }

    }
}
using System;
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
using eCollegeWP7.ViewModels;

namespace eCollegeWP7.Views
{
    public partial class BasePage : PhoneApplicationPage
    {

        public bool RequiresLogin
        {
            get { return (bool)GetValue(RequiresLoginProperty); }
            set { SetValue(RequiresLoginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RequiresLogin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequiresLoginProperty =
            DependencyProperty.Register("RequiresLogin", typeof(bool), typeof(BasePage), new PropertyMetadata(true));

        public BasePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (RequiresLogin)
            {
                if (App.Model.Session == null || App.Model.Session.CurrentToken == null)
                {
                    throw new Exception("Tried to view a page that requires a login");
                }
                else
                {
                    OnReady(e);
                }
            }
            else
            {
                OnReady(e);
            }
        }

        protected virtual void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {

        }

    }
}
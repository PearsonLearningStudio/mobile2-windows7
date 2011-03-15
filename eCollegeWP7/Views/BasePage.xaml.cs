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
using eCollegeWP7.Exceptions;

namespace eCollegeWP7.Views
{
    public partial class BasePage : PhoneApplicationPage
    {
        public bool CloseOnBackButton
        {
            get { return (bool)GetValue(CloseOnBackButtonProperty); }
            set { SetValue(CloseOnBackButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseOnBackButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseOnBackButtonProperty =
            DependencyProperty.Register("CloseOnBackButton", typeof(bool), typeof(BasePage),  new PropertyMetadata(false));

        
        public BasePage()
        {
            this.BackKeyPress += new EventHandler<System.ComponentModel.CancelEventArgs>(BasePage_BackKeyPress);
        }

        void BasePage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CloseOnBackButton)
            {
                throw new AppExitException();
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            OnReady(e);
        }

        protected virtual void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {

        }

    }
}
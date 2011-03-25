using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using eCollegeWP7.Exceptions;
using Microsoft.Phone.Controls;

namespace eCollegeWP7.Views
{
    public class BasePage : PhoneApplicationPage
    {
        public bool CloseOnBackButton
        {
            get { return (bool)GetValue(CloseOnBackButtonProperty); }
            set { SetValue(CloseOnBackButtonProperty, value); }
        }

        public bool ShowAppName
        {
            get { return (bool)GetValue(ShowAppNameProperty); }
            set { SetValue(ShowAppNameProperty, value); }
        }

        public bool ShowUserName
        {
            get { return (bool)GetValue(ShowUserNameProperty); }
            set { SetValue(ShowUserNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowUserName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowUserNameProperty =
            DependencyProperty.Register("ShowUserName", typeof(bool), typeof(BasePage), new PropertyMetadata(true));


        // Using a DependencyProperty as the backing store for ShowAppName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowAppNameProperty =
            DependencyProperty.Register("ShowAppName", typeof(bool), typeof(BasePage), new PropertyMetadata(true));
        

        // Using a DependencyProperty as the backing store for CloseOnBackButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseOnBackButtonProperty =
            DependencyProperty.Register("CloseOnBackButton", typeof(bool), typeof(BasePage), new PropertyMetadata(false));

        public BasePage()
        {
            this.BackKeyPress += new EventHandler<System.ComponentModel.CancelEventArgs>(BasePageNew_BackKeyPress);
        }

        void BasePageNew_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
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

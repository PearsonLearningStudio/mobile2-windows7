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
using ECollegeAPI.Model;
using eCollegeWP7.Util;
using eCollegeWP7.ViewModels;

namespace eCollegeWP7.Views
{
    public partial class GradePage : BasePage
    {

        protected GradeViewModel Model { get { return this.DataContext as GradeViewModel; } }

        public GradePage()
            : base()
        {
            InitializeComponent();
        }

        protected override void OnReady(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            long courseId = Int64.Parse(parameters["courseId"]);
            string gradebookItemGuid = parameters["gradebookItemGuid"];
            this.DataContext = new GradeViewModel(courseId, gradebookItemGuid);

        }

        private void BtnViewAllCourseGrades_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/CourseGradebookPage.xaml?courseId=" + Model.CourseID, UriKind.Relative));
        }

    }
}
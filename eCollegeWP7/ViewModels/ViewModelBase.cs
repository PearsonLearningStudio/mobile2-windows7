using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using eCollegeWP7.Util;
using System.Linq.Expressions;

namespace eCollegeWP7
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        protected ViewModelBase()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(Expression<Func<object>> Property)
        {
            this.PropertyChanged.Notify(Property);
        }

        public void Dispose()
        {
            this.OnDispose();
        }

        protected virtual void OnDispose()
        {

        }
    }
}

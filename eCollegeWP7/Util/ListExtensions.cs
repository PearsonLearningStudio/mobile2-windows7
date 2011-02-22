using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.ObjectModel;

namespace eCollegeWP7.Util
{
    public static class ListExtensions
    {

        /// <summary>
        /// Notifies listeners about a change.
        /// </summary>
        /// <param name="EventHandler">The event to raise.</param>
        /// <param name="Property">The property that changed.</param>
        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> currentList)
        {
            var res = new ObservableCollection<T>();
            foreach (var i in currentList)
            {
                res.Add(i);
            }
            return res;
        }
    }
}

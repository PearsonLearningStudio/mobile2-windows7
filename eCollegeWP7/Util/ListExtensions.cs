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

        
        public static ObservableCollection<GroupedObservableCollection<T>> ToSingleGroupedObservableCollection<T>(this List<T> currentList)
        {
            //Initialise the Grouped OC to populate and return
            ObservableCollection<GroupedObservableCollection<T>> result = new ObservableCollection<GroupedObservableCollection<T>>();
            
            var innerOC = new GroupedObservableCollection<T>("singleItem");
            foreach (var i in currentList)
            {
                innerOC.Add(i);
            }

            result.Add(innerOC);
            return result;
        }

        public static ObservableCollection<GroupedObservableCollection<T>> ToSingleGroupedObservableCollection<T>(this ObservableCollection<T> currentList)
        {
            //Initialise the Grouped OC to populate and return
            ObservableCollection<GroupedObservableCollection<T>> result = new ObservableCollection<GroupedObservableCollection<T>>();
            
            var innerOC = new GroupedObservableCollection<T>("singleItem");
            foreach (var i in currentList)
            {
                innerOC.Add(i);
            }

            result.Add(innerOC);
            return result;
        }
    }
}

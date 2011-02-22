using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace eCollegeWP7.Util
{
    public static class TreeHelperExtensions
    {

//        public static childItem FindLogicalChild<childItem>(this DependencyObject obj)
//where childItem : class
//        {
//            return FindLogicalChildInternal<childItem>(obj);
//        }

//        private static childItem FindLogicalChildInternal<childItem>(object obj)
//where childItem : class
//        {
//            System.Collections.IEnumerable children = null;
//            if (obj is FrameworkElement)
//            {
//                children = LogicalTreeHelper.GetChildren(obj as FrameworkElement);
//            }
//            else if (obj is FrameworkContentElement)
//            {
//                children = LogicalTreeHelper.GetChildren(obj as FrameworkContentElement);
//            }
//            else if (obj is DependencyObject)
//            {
//                children = LogicalTreeHelper.GetChildren(obj as DependencyObject);
//            }

//            if (children != null)
//            {
//                foreach (var child in children)
//                {
//                    if (child != null && child is childItem)
//                        return child as childItem;
//                    else
//                    {
//                        childItem childOfChild = FindLogicalChildInternal<childItem>(child);
//                        if (childOfChild != null)
//                            return childOfChild;
//                    }
//                }
//            }
//            return null;
//        }

        public static childItem FindVisualChild<childItem>(this DependencyObject obj)
    where childItem : class
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return child as childItem;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }


        public static childItem FindVisualChild<childItem>(this DependencyObject obj, string name)
    where childItem : class
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem && child is FrameworkElement && (child as FrameworkElement).Name == name)
                    return child as childItem;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child,name);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public static parentItem FindVisualParent<parentItem>(this DependencyObject obj)
    where parentItem : class
        {
            DependencyObject target = obj;
            do
            {
                target = VisualTreeHelper.GetParent(target);
            } while (target != null && target is parentItem);
            return target as parentItem;
        }
    }

}

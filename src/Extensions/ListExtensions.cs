using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppBlocks.Models.Extensions
{
    public static class ListExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }

        //public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
        //{
        //    ObservableCollection<T> observableCollection = new ObservableCollection<T>();
        //    foreach (T obj in items)
        //        observableCollection.Add(obj);
        //    return observableCollection;
        //}

        public static List<T> Filter<T>(this List<T> Source, Func<T, string> selector, string CompValue)
        {
            return Source.Where(a => selector(a) == CompValue).ToList();
        }
    }
}
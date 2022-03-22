using System.Collections.Generic;
using System.Linq;

namespace AppBlocks.Models.Extensions
{
    public static class CollectionExtensions
    {
        public static string GetValue(this ICollection<Item> collection, string key, string defaultValue = null) => ((bool)collection?.Any(s => s.Id == key)) ? collection.FirstOrDefault(s => s.Id == key)?.Data : default;

        public static string GetValueOrDefault(this ICollection<Item> collection, string key, string defaultValue) //where TKey : ExtendedBindableObject// : notnull
            => collection?.GetValue(key, defaultValue);
    }
}
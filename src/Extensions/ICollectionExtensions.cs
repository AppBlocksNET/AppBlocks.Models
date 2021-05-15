using System.Collections.Generic;
using System.Linq;

namespace AppBlocks.Models.Extensions
{
    public static class CollectionExtensions
    {
        public static string GetValue(this ICollection<Setting> collection, string key, string defaultValue = null) => ((bool)collection?.Any(s => s.Key == key)) ? collection.FirstOrDefault(s => s.Key == key)?.Value : default;

        public static string GetValueOrDefault(this ICollection<Setting> collection, string key, string defaultValue) //where TKey : ExtendedBindableObject// : notnull
            => collection?.GetValue(key, defaultValue);
    }
}
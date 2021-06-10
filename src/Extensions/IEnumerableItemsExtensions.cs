using System.Collections.Generic;
using System.Linq;

namespace AppBlocks.Models.Extensions
{
    public static class IEnumerableItemsExtensions
    {
        public static Item GetChild(
           this IEnumerable<Item> source,
           string name, bool recursive = false)
        {
            return !recursive ? source.FirstOrDefault(i => i.Name == name) : source.FirstOrDefaultFromMany(c => c.Children, i => i.Name == name);
        }
    }
}

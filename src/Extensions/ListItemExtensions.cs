using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AppBlocks.Models.Extensions
{
    public static class ListItemExtensions
    {
        /// <summary>
        /// SetChildren
        /// </summary>
        /// <param name="items"></param>
        public static List<Item> SetChildren(this List<Item> items)
        {
            var results = new List<Item>();
            if (items == null) return results;
            Trace.WriteLine($"{typeof(Item).Name}.SetChildren({items.Count()}) started:{DateTime.Now.ToShortTimeString()}");
            results = items;
            foreach (var item in results)
            {
                var children = results.Where(i => i.ParentId == item.Id);
                item.Children = children.ToList();
                foreach (var childItem in item.Children)
                {
                    childItem.SetChildren(results);
                }
            }
            return results;
        }
    }
}
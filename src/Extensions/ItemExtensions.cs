using AppBlocks.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace AppBlocks.Models.Extensions
{
    public static class ItemExtensions
    {
        public static List<T> FindChild<T>(this Item item, Func<Item, bool> filter = null) where T : Item
        {
            var results = new List<T>();
            foreach(var child in item.Children)
            {
                if (child.Children.Any())
                {
                    results.AddRange(child.FindChild<T>(filter));
                }
                else
                {
                    var queue = new List<T> { (T)child };
                    if (queue.Count > 0) results.AddRange((IEnumerable<T>)queue.Where(filter ?? (s => true)));
                    queue.Clear();
                }
            }
            return results;
        }

        /// <summary>
        /// FromJson
        /// </summary>
        /// <param name="json"></param>
        public static T FromJsonList<T>(this Item item, string json) where T : List<Item>
        {
            var typeName = typeof(T).FullName;
            //_logger?.LogInformation($"{typeof(Item).Name}.FromJson<{typeName}>({json}) started:{DateTime.Now.ToShortTimeString()}");

            var jsonFile = Common.FromFile(json, typeName);
            if (!string.IsNullOrEmpty(jsonFile)) json = jsonFile;

            if (typeName == "AppBlocks.Models.User")
            {
                json = Context.AppBlocksServiceUrl + "/account/authenticate";// Models.Settings.GroupId
            }

            if (json.ToLower().StartsWith("http") || json == Context.GroupId || string.IsNullOrEmpty(json))
            {
                return Item.FromUriList<T>(!string.IsNullOrEmpty(json) && json != Context.GroupId ? new Uri(json) : default);
            }
            else
            {
                if (string.IsNullOrEmpty(json) || json == Context.GroupId) return default;

                var jsonTrimmed = json.Trim();
                if (!jsonTrimmed.StartsWith("[") && !jsonTrimmed.StartsWith("{")) return default;

                var array = json.StartsWith("[") && json.EndsWith("]");

                if (!array) return JsonSerializer.Deserialize<T>(json);

                var items = JsonSerializer.Deserialize<T>(json);

                if (items == null) return default;

                //var results = items.FirstOrDefault();
                items.FirstOrDefault().SetChildren(items);
                return items;
            }
        }
        public static async Task<T> FromJsonAsync<T>(this Item item, string json) where T : Item => Item.FromJson<T>(json);

        public static async Task<T> FromJsonListAsync<T>(this Item item, string json) where T : List<Item> => item.FromJsonList<T>(json);


        /// <summary>
        /// FromServiceAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<T> FromServiceAsync<T>(this Item item, string id = null) where T : Item => await item.FromJsonAsync<T>(!string.IsNullOrEmpty(id) ? id : Context.GroupId);

        /// <summary>
        /// FromServiceAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<T> FromServiceListAsync<T>(this Item item, string id = null) where T : List<Item> => await item.FromJsonListAsync<T>(!string.IsNullOrEmpty(id) ? id : Context.GroupId);



    }
}
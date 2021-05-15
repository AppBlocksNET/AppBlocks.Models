using AppBlocks.Extensions;
using AppBlocks.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppBlocks.Models
{
    /// <summary>
    /// Item
    /// </summary>
    public partial class Item : INotifyPropertyChanged //Item // INotifyPropertyChanged // : Item
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructors
        public Item()
        {
            Children = new List<Item>();
            if (Settings == null) Settings = new Dictionary<string, string>();
        }

        public Item(Uri sourceUri) => FromItem(FromUri<Item>(sourceUri));

        public Item(string json) => FromItem(FromJson<Item>(json));

        public Item(List<Item> items) => FromItem(FromList<Item>(items));
        ////(this IConfigurationRoot config) => config.GetSection("AppBlocks").AsEnumerable().ToImmutableDictionary(x => x.Key, x => x.Value);
        #endregion

        public T GetSetting<T>(string key, string defaultValue = null) => (T)Convert.ChangeType(Settings.GetValueOrDefault(key, defaultValue) ?? defaultValue, typeof(T));

        [JsonPropertyName("settings")]
        public Dictionary<string, string> Settings { get; set; }

        [JsonPropertyName("status")]
        [IgnoreDataMember]
        public string Status { get; set; }

        [JsonPropertyName("fullPath")]
        [NotMapped]
        public string FullPath { get; set; }

        /// <summary>
        /// Id - use this to create a new one: Guid.NewGuid().ToString()
        /// </summary>
        [DataMember(Name = "id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [DataMember(Name = "icon")]
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [DataMember(Name = "image")]
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [DataMember(Name = "name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public IEnumerable<Item> OwnedBy { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public Item Owner { get; set; }

        [DataMember(Name = "ownerId")]
        [JsonPropertyName("ownerid")]
        public string OwnerId { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        [JsonIgnore]
        public virtual Item Parent { get; set; }

        [DataMember(Name = "parentid")]
        [JsonPropertyName("parentid")]
        public string ParentId { get; set; }

        [DataMember(Name = "path")]
        [JsonPropertyName("path")]
        public string Path { get; set; }

        [DataMember(Name = "data")]
        [JsonPropertyName("")]
        public string Data { get; set; }

        [DataMember(Name = "description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [DataMember(Name = "link")]
        [JsonPropertyName("link")]
        public string Link { get; set; }

        [DataMember(Name = "source")]
        [JsonPropertyName("source")]
        public string Source { get; set; }

        [DataMember(Name = "title")]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        //[DataMember(Name = "type")]
        //[JsonPropertyName("type")]
        [JsonIgnore]
        public virtual Item Type { get; set; }

        [DataMember(Name = "typeId")]
        [JsonPropertyName("typeid")]
        public string TypeId { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public IEnumerable<Item> TypeOf { get; set; }


        [DataMember(Name = "created")]
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public List<Item> CreatedBy { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public Item Creator { get; set; }
        [DataMember(Name = "creatorId")]
        [JsonPropertyName("creatorid")]
        public string CreatorId { get; set; }


        [DataMember(Name = "edited")]
        [JsonPropertyName("edited")]
        public DateTime Edited { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public Item Editor { get; set; }

        [DataMember(Name = "editorId")]
        [JsonPropertyName("editorid")]
        public string EditorId { get; set; }


        [DataMember(Name = "children")]
        [JsonPropertyName("children")]
        public IEnumerable<Item> Children { get; set; }

        //public static explicit operator Item(JToken v)
        //{
        //    return JsonConvert.DeserializeObject<Item>(v.ToString());
        //}

        /// <summary>
        /// FromDataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IEnumerable<Item> FromDataReader(IDataReader reader) => reader.Select(r => new Item
        {
            Created = r["created"] is DBNull ? DateTime.MinValue : DateTime.Parse(r["created"].ToString()),
            CreatorId = r["creatorid"] is DBNull ? null : r["creatorid"].ToString(),
            Data = r["data"] is DBNull ? null : r["data"].ToString(),
            Description = r["description"] is DBNull ? null : r["description"].ToString(),
            Edited = r["updated"] is DBNull ? DateTime.MinValue : DateTime.Parse(r["updated"].ToString()),
            EditorId = r["EditorId"] is DBNull ? null : r["EditorId"].ToString(),
            FullPath = r["FullPath"] == null || r["FullPath"] is DBNull ? null : r["FullPath"].ToString(),
            Icon = r["icon"] is DBNull ? null : r["icon"].ToString(),
            Id = r["id"] is DBNull ? null : r["id"].ToString(),
            Image = r["image"] is DBNull ? null : r["image"].ToString(),
            Link = r["link"] is DBNull ? null : r["link"].ToString(),
            Name = r["name"] is DBNull ? null : r["name"].ToString(),
            OwnerId = r["ownerid"] is DBNull ? null : r["ownerid"].ToString(),
            ParentId = r["parentid"] is DBNull ? null : r["parentid"].ToString(),
            Path = r["path"] is DBNull ? null : r["path"].ToString(),
            Source = r["source"] is DBNull ? null : r["source"].ToString(),
            Status = r["status"] is DBNull ? null : r["status"].ToString(),
            Title = r["title"] is DBNull ? null : r["title"].ToString(),
            TypeId = r["typeid"] is DBNull ? null : r["typeid"].ToString()
        }).ToList();

        /// <summary>
        /// FromJson
        /// </summary>
        /// <param name="json"></param>
        public static T FromJson<T>(string json) where T : Item
        {
            Trace.WriteLine($"{typeof(Item).Name}.FromJson<{typeof(T).Name}>({json}) started:{DateTime.Now.ToShortTimeString()}");

            var jsonFile = FromFile(json, typeof(T).Name);
            if (!string.IsNullOrEmpty(jsonFile)) json = jsonFile;

            if (json.ToLower().StartsWith("http") || json == Models.Settings.GroupId || string.IsNullOrEmpty(json))
            {
                return FromUri<T>(json != Models.Settings.GroupId ? new Uri(json) : null);
            }
            else
            {
                if (string.IsNullOrEmpty(json) || json == Models.Settings.GroupId) return default;

                var jsonTrimmed = json.Trim();
                if (!jsonTrimmed.StartsWith("[") && !jsonTrimmed.StartsWith("{")) return default;

                var array = json.StartsWith("[") && json.EndsWith("]");

                if (!array) return JsonSerializer.Deserialize<T>(json);

                var items = JsonSerializer.Deserialize<List<T>>(json);

                if (items == null) return default;

                var item = items.FirstOrDefault();
                item.SetChildren(items);
                return item;
            }
        }

        /// <summary>
        /// FromEnumerable
        /// </summary>
        /// <param name="items"></param>
        public void FromEnumerable(IEnumerable<Item> items) => FromItem(FromList<Item>(items.ToList()));

        /// <summary>
        /// FromFile
        /// </summary>
        public static string FromFile(string filePathOrId, string typeName = "Item")
        {
            Trace.WriteLine($"{typeof(Item).Name}.Read({filePathOrId}) started:{DateTime.Now.ToShortTimeString()}");

            if (File.Exists(filePathOrId))
            {
                Trace.WriteLine($"{typeof(Item).Name}.Read({filePathOrId}) found:{filePathOrId}");
                return System.IO.File.ReadAllText(filePathOrId);
            }

            var filePath = GetFilepath(filePathOrId, typeName);
            if (File.Exists(filePath))
            {
                Trace.WriteLine($"{typeof(Item).Name}.Read({filePathOrId}) found:{filePath}");
                return File.ReadAllText(filePath);
            }
            Trace.WriteLine($"{typeof(Item).Name}.Read({filePathOrId}):no file found");
            return null;
        }

        /// <summary>
        /// FromItem
        /// </summary>
        /// <param name="item"></param>
        public void FromItem(Item item)
        {
            if (item == null) return;
            Trace.WriteLine($"{typeof(Item).Name}.FromItem({item.Id}) started:{DateTime.Now.ToShortTimeString()}");
            Children = item.Children;
            Created = item.Created;
            CreatorId = item.CreatorId;
            Data = item.Data;
            Description = item.Description;
            Edited = item.Edited;
            EditorId = item.EditorId;
            FullPath = item.FullPath;
            Icon = item.Icon;
            Id = item.Id;
            Image = item.Image;
            Link = item.Link;
            Name = item.Name;
            ParentId = item.ParentId;
            Path = item.Path;
            OwnerId = item.OwnerId;
            Title = item.Title;
            TypeId = item.TypeId;
            Source = item.Source;
            Status = item.Status;
        }

        /// <summary>
        /// FromList
        /// </summary>
        /// <param name="items"></param>
        public static T FromList<T>(List<Item> items) where T : Item => (items.SetChildren().SingleOrDefault(i => i.TypeId == Models.Settings.GroupTypeId) ?? items.SetChildren().FirstOrDefault()) as T;

        /// <summary>
        /// FromService
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T FromService<T>(string id = null) where T : Item => FromJson<T>(!string.IsNullOrEmpty(id) ? id : Models.Settings.GroupId);

        //if (string.IsNullOrEmpty(id)) id = Settings.GroupId;
        //Trace.WriteLine($"{typeof(T).Name}.FromService({id}) started:{DateTime.Now.ToShortTimeString()}");
        //var item = FromJson<T>(id);
        //if (item == null)
        //{
        //    item = FromUri<T>(new Uri($"{Settings.AppBlocksBlocksServiceUrl}{id}"));

        //    if (item != null)
        //    {
        //        item.ToFile<T>();
        //    }
        //}
        //return item;

        ///// <summary>
        ///// FromSettings
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="settings"></param>
        ///// <returns></returns>
        //public static T FromSettings<T>(ICollection<Item> settings) where T : Item
        //{
        //    Trace.WriteLine($"{typeof(T).Name}.FromSettings({settings}) started:{DateTime.Now.ToShortTimeString()}");
        //    T results = default;
        //    if (settings.Count() > 0)
        //    {
        //        var firstSetting = settings.FirstOrDefault();
        //        if (firstSetting.Id != "refresh")
        //        {
        //            if (settings?.Count() > 1)
        //            {
        //                Trace.WriteLine($"FromSettings: settings found - {settings?.Count()}");
        //            }
        //            return results;
        //        }
        //    }
        //    Trace.WriteLine($"FromSettings: {results?.Id}");
        //    return results;
        //}

        /// <summary>
        /// FromUri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static T FromUri<T>(Uri uri = null) where T : Item
        {
            Trace.WriteLine($"{typeof(Item).Name}.FromUri({uri}) started:{DateTime.Now.ToShortTimeString()}");
            var content = string.Empty;
            try
            {
                if (uri == null) uri = new Uri(Models.Settings.AppBlocksBlocksServiceUrl + "{Id}");
                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.ServerCertificateValidationCallback = (message, cert, chain, errors) => { return true; };
                // response.GetResponseStream();
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    Trace.WriteLine($"HttpStatusCode:{response.StatusCode}");
                    var responseValue = string.Empty;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = string.Format("{App.AppName ERROR: }Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }

                    content = responseValue;
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{nameof(Item)}.FromUri({uri}) ERROR:{exception.Message} {exception}");
            }
            Trace.WriteLine($"content:{content}");

            return FromJson<T>(content);
        }

        /// <summary>
        /// GetFilename
        /// </summary>
        /// <returns></returns>
        public virtual string GetFilename(string typeName = "Item") => GetFilepath(Id, typeName);

        /// <summary>
        /// GetFilepath
        /// </summary>
        /// <param name="filePathOrId"></param>
        /// <returns></returns>
        public static string GetFilepath(string filePathOrId, string typeName = "Item")
        {
            if (File.Exists(filePathOrId)) return filePathOrId;
            return $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\app_data\\blocks\\{typeName}.{filePathOrId}.json";
        }

        /// <summary>
        /// SetChildren
        /// </summary>
        /// <param name="items"></param>
        public void SetChildren<T>(List<T> items) where T : Item
        {
            Children = items.Where(i => i.ParentId == Id);

            foreach (var item in Children)
            {
                item.SetChildren(items);
            }
        }

        /// <summary>
        /// ToJson
        /// </summary>
        /// <returns></returns>
        public virtual string ToJson() => JsonSerializer.Serialize(this);//App.SerializerService.SerializeAsync(this).Result;

        /// <summary>
        /// ToFile
        /// </summary>
        /// <returns></returns>
        public bool ToFile<T>(string filePath = null) where T : Item
        {
            try
            {
                Trace.WriteLine($"{typeof(T).Name}.ToFile({filePath}): {DateTime.Now.ToShortTimeString()} started");

                if (string.IsNullOrEmpty(filePath)) filePath = GetFilename(typeof(T).Name);

                if (string.IsNullOrEmpty(filePath) || filePath.EndsWith($"{typeof(T).Name}..json")) return false;

                new FileInfo(filePath).Directory.Create();
                var content = ToJson();

                if (string.IsNullOrEmpty(content)) return false;

                File.WriteAllText(filePath, content);
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{this}.ToFile({filePath}) ERROR:{exception}");
                return false;
            }
            return true;
        }
    }
}
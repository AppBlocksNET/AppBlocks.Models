using AppBlocks.Extensions;
using AppBlocks.Models.Extensions;
using AppBlocks.Models.Services;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace AppBlocks.Models
{
    /// <summary>
    /// Item
    /// </summary>
    [Serializable, XmlRoot(Namespace = "", IsNullable = false),XmlType("item")]
    [BsonIgnoreExtraElements]
    [BsonKnownTypes(typeof(Item))]
    //[XmlObjectWrapper(TupleElementNamesAttribute = "items")]
    public class Item : Entity, INotifyPropertyChanged //Item // INotifyPropertyChanged // : Item
    {
        private readonly ILogger<Item> _logger;
        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructors
        public Item()
        {
            Children = new List<Item>();
            using (var serviceScope = ServiceActivator.GetScope())
            {
                if (serviceScope != null)
                {
                    _logger = (ILogger<Item>)serviceScope.ServiceProvider.GetService(typeof(ILogger<Item>));
                }
            }
        }

        public Item(Uri sourceUri) => FromItem(Item.FromUri<Item>(sourceUri));

        public Item(string json)
        {
            var item = FromJson<Item>(json);
            if (item != null)
            {
                Created = item.Created;
                CreatorId = item.CreatorId;
                Edited = item.Edited;
                EditorId = item.EditorId;
                Data = item.Data;
                Icon = item.Icon;
                Image = item.Image;
                Id = item.Id;
                Name = item.Name;
                OwnerId = item.OwnerId;
                ParentId = item.ParentId;
                Path = item.Path;
                Title = item.Title;
                TypeId = item.TypeId;
                Children = item.Children;
            }
        }
        public Item(ILogger<Item> logger = null)
        {
            _logger = logger;
        }
        public Item(List<Item> items) => this.FromItem(FromList<Item>(items));
        ////(this IConfigurationRoot config) => config.GetSection("AppBlocks").AsEnumerable().ToImmutableDictionary(x => x.Key, x => x.Value);
        #endregion

        [DataMember(Name = "children")]
        [JsonPropertyName("children")]
        [XmlArrayAttribute("children")]
        public List<Item> Children { get; set; }

        [DataMember(Name = "data")]
        [JsonPropertyName("data")]
        [XmlElement("data")]
        [BsonElement("data")]
        public string Data { get; set; }

        [DataMember(Name = "description")]
        [JsonPropertyName("description")]
        [XmlElement("description")]
        [BsonElement("description")]
        public string Description { get; set; }

        [NotMapped]
        [JsonPropertyName("fullpath")]
        [XmlElement("fullpath")]
        public string FullPath { get; set; }

        /// <summary>
        /// Id - use this to create a new one: Guid.NewGuid().ToString()
        /// </summary>
        [DataMember(Name = "id")]
        [JsonPropertyName("id")]
        [XmlElement("id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [DataMember(Name = "icon")]
        [JsonPropertyName("icon")]
        [XmlElement("icon")]
        [BsonElement("icon")]
        public string Icon { get; set; }

        [DataMember(Name = "image")]
        [JsonPropertyName("image")]
        [XmlElement("image")]
        [BsonElement("image")]
        public string Image { get; set; }

        [DataMember(Name = "name")]
        [JsonPropertyName("name")]
        [XmlElement("name")]
        [BsonElement("name")]
        public string Name { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        public IEnumerable<Item> OwnedBy { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        public Item Owner { get; set; }

        [DataMember(Name = "ownerid")]
        [JsonPropertyName("ownerid")]
        [XmlElement("ownerid")]
        [BsonElement("ownerid")]
        public string OwnerId { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        public virtual Item Parent { get; set; }

        [DataMember(Name = "parentid")]
        [JsonPropertyName("parentid")]
        [XmlElement("parentid")]
        [BsonElement("parentid")]
        public string ParentId { get; set; }

        [DataMember(Name = "path")]
        [JsonPropertyName("path")]
        [XmlElement("path")]
        [BsonElement("path")]
        public string Path { get; set; }

        [DataMember(Name = "link")]
        [JsonPropertyName("link")]
        [XmlElement("link")]
        [BsonElement("link")]
        public string Link { get; set; }

        [DataMember(Name = "source")]
        [JsonPropertyName("source")]
        [XmlElement("source")]
        [BsonElement("source")]
        public string Source { get; set; }

        [JsonPropertyName("status")]
        [IgnoreDataMember]
        [XmlElement("status")]
        [BsonElement("status")]
        public string Status { get; set; }

        [DataMember(Name = "title")]
        [JsonPropertyName("title")]
        [XmlElement("title")]
        [BsonElement("title")]
        public string Title { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        public virtual Item Type { get; set; }

        [DataMember(Name = "typeid")]
        [JsonPropertyName("typeid")]
        [XmlElement("typeid")]
        [BsonElement("typeid")]
        public string TypeId { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        public IEnumerable<Item> TypeOf { get; set; }

        [DataMember(Name = "created")]
        [JsonPropertyName("created")]
        [XmlElement("created")]
        [BsonElement("created")]
        public DateTime Created { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        public List<Item> CreatedBy { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        public Item Creator { get; set; }

        [DataMember(Name = "creatorid")]
        [JsonPropertyName("creatorid")]
        [XmlElement("creatorid")]
        [BsonElement("creatorid")]
        public string CreatorId { get; set; }

        [DataMember(Name = "edited")]
        [JsonPropertyName("edited")]
        [XmlElement("edited")]
        [BsonElement("edited")]
        public DateTime Edited { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        public Item Editor { get; set; }

        [DataMember(Name = "editorid")]
        [JsonPropertyName("editorid")]
        [XmlElement("editorid")]
        [BsonElement("editorid")]
        public string EditorId { get; set; }

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
            //for (int i = 0; i < r.FieldCount; i++) { //TODO: Do this dynamically?!
            Created = r["created"] == null || r["created"] is DBNull ? DateTime.MinValue : DateTime.Parse(r["created"].ToString()),
            CreatorId = r["creatorid"] == null || r["creatorid"] is DBNull ? null : r["creatorid"].ToString(),
            Data = r["data"] == null || r["data"] is DBNull ? null : r["data"].ToString(),
            Description = r["description"] == null || r["description"] is DBNull ? null : r["description"].ToString(),
            Edited = r["updated"] == null || r["updated"] is DBNull ? DateTime.MinValue : DateTime.Parse(r["updated"].ToString()),
            EditorId = r["EditorId"] == null || r["EditorId"] is DBNull ? null : r["EditorId"].ToString(),
            FullPath = r["FullPath"] == null || r["FullPath"] == null || r["FullPath"] is DBNull ? null : r["FullPath"].ToString(),
            Icon = r["icon"] == null || r["icon"] is DBNull ? null : r["icon"].ToString(),
            Id = r["id"] == null || r["id"] is DBNull ? null : r["id"].ToString(),
            Image = r["image"] == null || r["image"] is DBNull ? null : r["image"].ToString(),
            Link = r["link"] == null || r["link"] is DBNull ? null : r["link"].ToString(),
            Name = r["created"] == null || r["created"] is DBNull ? null : r["name"].ToString(),
            OwnerId = r["ownerid"] == null || r["ownerid"] is DBNull ? null : r["ownerid"].ToString(),
            ParentId = r["parentid"] == null || r["parentid"] is DBNull ? null : r["parentid"].ToString(),
            Path = r["path"] == null || r["path"] is DBNull ? null : r["path"].ToString(),
            Source = r["source"] == null || r["source"] is DBNull ? null : r["source"].ToString(),
            Status = r["status"] == null || r["status"] is DBNull ? null : r["status"].ToString(),
            Title = r["title"] == null || r["title"] is DBNull ? null : r["title"].ToString(),
            TypeId = r["typeid"] == null || r["typeid"] is DBNull ? null : r["typeid"].ToString()
        }).ToList();

        public static IEnumerable<Item> FromDictionary(Dictionary<string, string> s) => s.Select(r => new Item
        {
            //for (int i = 0; i < r.FieldCount; i++) { //TODO: Do this dynamically?!
            Created = s["created"] == null || s["created"] is null ? DateTime.MinValue : DateTime.Parse(s["created"].ToString()),
            CreatorId = s["creatorid"] == null || s["creatorid"] is null ? null : s["creatorid"].ToString(),
            Data = s["data"] == null || s["data"] is null ? null : s["data"].ToString(),
            Description = s["description"] == null || s["description"] is null ? null : s["description"].ToString(),
            Edited = s["updated"] == null || s["updated"] is null ? DateTime.MinValue : DateTime.Parse(s["updated"].ToString()),
            EditorId = s["EditorId"] == null || s["EditorId"] is null ? null : s["EditorId"].ToString(),
            FullPath = s["FullPath"] == null || s["FullPath"] == null || s["FullPath"] is null ? null : s["FullPath"].ToString(),
            Icon = s["icon"] == null || s["icon"] is null ? null : s["icon"].ToString(),
            Id = s["id"] == null || s["id"] is null ? null : s["id"].ToString(),
            Image = s["image"] == null || s["image"] is null ? null : s["image"].ToString(),
            Link = s["link"] == null || s["link"] is null ? null : s["link"].ToString(),
            Name = s["created"] == null || s["created"] is null ? null : s["name"].ToString(),
            OwnerId = s["ownerid"] == null || s["ownerid"] is null ? null : s["ownerid"].ToString(),
            ParentId = s["parentid"] == null || s["parentid"] is null ? null : s["parentid"].ToString(),
            Path = s["path"] == null || s["path"] is null ? null : s["path"].ToString(),
            Source = s["source"] == null || s["source"] is null ? null : s["source"].ToString(),
            Status = s["status"] == null || s["status"] is null ? null : s["status"].ToString(),
            Title = s["title"] == null || s["title"] is null ? null : s["title"].ToString(),
            TypeId = s["typeid"] == null || s["typeid"] is null ? null : s["typeid"].ToString()
        }).ToList();

        /// <summary>
        /// FromEnumerable
        /// </summary>
        /// <param name="items"></param>
        public void FromEnumerable(IEnumerable<Item> items) => FromItem(FromList<Item>(items.ToList()));

        /// <summary>
        /// FromItem
        /// </summary>
        /// <param name="item"></param>
        public void FromItem(Item item)
        {
            if (item == null) return;
            _logger?.LogInformation($"{typeof(Item).Name}.FromItem({item.Id}) started:{DateTime.Now.ToShortTimeString()}");
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
        public static T FromList<T>(List<Item> items) where T : Item => (items.SetChildren().SingleOrDefault(i => i.TypeId == Context.GroupTypeId) ?? items.SetChildren().FirstOrDefault()) as T;


        /// <summary>
        /// FromJson
        /// </summary>
        /// <param name="content"></param>
        public static T FromJson<T>(string content) where T : Item
        {
            var typeName = typeof(T).FullName;
            //_logger?.LogInformation($"{typeof(Item).Name}.FromJson<{typeName}>({json}) started:{DateTime.Now.ToShortTimeString()}");

            var jsonFile = Common.FromFile(content, typeName);
            if (!string.IsNullOrEmpty(jsonFile)) content = jsonFile;

            if (typeName == "AppBlocks.Models.User")
            {
                content = Context.AppBlocksServiceUrl + "/account/authenticate";// Models.Settings.GroupId
            }

            if (content.ToLower().StartsWith("http") || content == Context.GroupId || string.IsNullOrEmpty(content))
            {
                return FromUri<T>(!string.IsNullOrEmpty(content) && content != Context.GroupId ? new Uri(content) : default);
            }
            else
            {
                if (string.IsNullOrEmpty(content) || content == Context.GroupId) return default;

                var jsonTrimmed = content.Trim();



                //if (!jsonTrimmed.StartsWith("[") && !jsonTrimmed.StartsWith("{")) return default;

                //var array = content.StartsWith("[") && content.EndsWith("]");

                //if (!array) return JsonSerializer.Deserialize<T>(content);
                if (jsonTrimmed.StartsWith("[") && jsonTrimmed.EndsWith("]"))
                {
                    var items = JsonSerializer.Deserialize<List<T>>(content);
                    items.FirstOrDefault().SetChildren<T>(items);
                    return items.FirstOrDefault();
                }
                else if (jsonTrimmed.StartsWith("{") && jsonTrimmed.EndsWith("}"))
                {
                    return JsonSerializer.Deserialize<T>(content);
                }

                else if (jsonTrimmed.StartsWith("<") && jsonTrimmed.EndsWith(">"))
                {
                    return FromXml<T>(jsonTrimmed, null);
                }
                else if (!string.IsNullOrEmpty(jsonTrimmed))
                {
                    var item = new Item
                    {
                        Id = jsonTrimmed,
                        Name = jsonTrimmed,
                        Path = jsonTrimmed,
                        Title = jsonTrimmed,
                        Created = DateTime.Now,
                        Edited = DateTime.Now
                    };
                    var results = (T)item;
                    return results;
                }

                if (typeName == "AppBlocks.Models.Item")
                {
                    var items = JsonSerializer.Deserialize<List<T>>(content);

                    if (items == null) return default;

                    //var results = items.FirstOrDefault();
                    items.FirstOrDefault().SetChildren<T>(items);
                    return items.FirstOrDefault();
                }
                return default;
            }
        }


        /// <summary>
        /// FromJson
        /// </summary>
        /// <param name="json"></param>
        public static T FromJsonList<T>(string json) where T : List<Item>
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
                return FromUriList<T>(!string.IsNullOrEmpty(json) && json != Context.GroupId ? new Uri(json) : default);
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

        /// <summary>
        /// FromService
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T FromService<T>(string id = null) where T : Item => Item.FromJson<T>(!string.IsNullOrEmpty(id) ? id : Context.GroupId);

        /// <summary>
        /// FromServiceList
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T FromServiceList<T>(string id = null) where T : List<Item> => FromJsonList<T>(!string.IsNullOrEmpty(id) ? id : Context.GroupId);

        /// <summary>
        /// FromUri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static T FromUri<T>(Uri uri = null) where T : Item
        {
            if (uri == null) uri = new Uri(typeof(T).FullName != "AppBlocks.Models.User" ? Context.AppBlocksBlocksServiceUrl + Context.GroupId : Context.AppBlocksServiceUrl + "/account/authenticate");
            //_logger?.LogInformation($"{typeof(Item).Name}.FromUri({uri}) started:{DateTime.Now.ToShortTimeString()}");
            var content = string.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.ServerCertificateValidationCallback = (message, cert, chain, errors) => { return true; };
                // response.GetResponseStream();
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    //_logger?.LogInformation($"HttpStatusCode:{response.StatusCode}");
                    var responseValue = string.Empty;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = $"{typeof(Item).FullName} ERROR: Request failedd. Received HTTP {response.StatusCode}";
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
                //_logger?.LogInformation($"{nameof(Item)}.FromUri({uri}) ERROR:{exception.Message} {exception}");
                Trace.WriteLine($"{nameof(Item)}.FromUri({uri}) ERROR:{exception.Message} {exception}");
            }
            //_logger?.LogInformation($"content:{content}");

            if (content.StartsWith("<"))
            {
                return FromXml<T>(content);
            }
            return FromJson<T>(content);
        }


        /// <summary>
        /// FromUri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static T FromUriList<T>(Uri uri = null) where T : List<Item>
        {
            if (uri == null) uri = new Uri(typeof(T).FullName != "AppBlocks.Models.User" ? Context.AppBlocksBlocksServiceUrl + Context.GroupId : Context.AppBlocksServiceUrl + "/account/authenticate");
            //_logger?.LogInformation($"{typeof(Item).Name}.FromUri({uri}) started:{DateTime.Now.ToShortTimeString()}");
            var content = Common.GetUrl(uri.ToString());
            //_logger?.LogInformation($"content:{content}");

            if (content.StartsWith("<"))
            {
                return FromXmlList<T>(content);
            }
            return FromJsonList<T>(content);
        }


        /// <summary>
        /// FromUri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static T FromXml<T>(string content = null, string xslt = null) where T : Item
        {
            if (string.IsNullOrEmpty(content)) return default;
            if (content.StartsWith("http"))
            {
                content = Common.GetUrl(content);
            }
            else
            {
                var xmlFile = Common.FromFile(content);
                if (!string.IsNullOrEmpty(xmlFile)) content = xmlFile;
            }
            //if (uri == null) uri = new Uri(typeof(T).FullName != "AppBlocks.Models.User" ? Context.AppBlocksBlocksServiceUrl + Context.GroupId : Context.AppBlocksServiceUrl + "/account/authenticate");
            //_logger?.LogInformation($"{typeof(Item).Name}.FromUri({uri}) started:{DateTime.Now.ToShortTimeString()}");

            //var xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(content);
            //return (T)Item.FromJson<Item>(Newtonsoft.Json.JsonConvert.SerializeXmlNode(xmlDoc));
            //var serializer = new XmlSerializer(typeof(Item));
            //Item result;

            //using (TextReader reader = new StringReader(content))
            //{
            //    result = (Item)serializer.Deserialize(reader);
            //}
            //return (T)result;
            return Common.FromXml<T>(content, xslt);
        }

        /// <summary>
        /// FromUri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static T FromXmlList<T>(string content = null, string xslt = null) where T : List<Item>
        {
            if (string.IsNullOrEmpty(content)) return default;
            var xmlFile = Common.FromFile(content);
            if (!string.IsNullOrEmpty(xmlFile)) content = xmlFile;

            //if (uri == null) uri = new Uri(typeof(T).FullName != "AppBlocks.Models.User" ? Context.AppBlocksBlocksServiceUrl + Context.GroupId : Context.AppBlocksServiceUrl + "/account/authenticate");
            //_logger?.LogInformation($"{typeof(Item).Name}.FromUri({uri}) started:{DateTime.Now.ToShortTimeString()}");

            //var xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(content);
            //return (T)FromJsonList<List<Item>>(Newtonsoft.Json.JsonConvert.SerializeXmlNode(xmlDoc));
            
            //var serializer = new XmlSerializer(typeof(List<Item>));
            //List<Item> results;
            return Common.FromXmlList<T>(content, xslt);
        }

        //public static Item FromJson<Item>(string path) => FromJson<Item>(path);

        //public static List<Item> FromJsonList<Item>(string path) => FromJsonList<Item>(path);

        //public static Item FromService<T>(string id = null) where T : Item  => FromService<T>(id);
        //public static List<Item> FromServiceList<T>(string id = null) where T : List<Item> => FromServiceList<T>(id);

        //public static Item FromXml<T>(string content = null, string xslt = null) where T : Item => FromJson<Item>(content);

        //public static List<Item> FromXmlList<T>(string content = null, string xslt = null) where T : List<Item> => this.FromJsonList<Item>(content);

        //public T GetSetting<T>(string key, string defaultValue = null) => (T)Convert.ChangeType(Settings.GetValueOrDefault(key, defaultValue) ?? defaultValue, typeof(T));


        //if (string.IsNullOrEmpty(id)) id = Settings.GroupId;
        //_logger?.LogInformation($"{typeof(T).Name}.FromService({id}) started:{DateTime.Now.ToShortTimeString()}");
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
        //    _logger?.LogInformation($"{typeof(T).Name}.FromSettings({settings}) started:{DateTime.Now.ToShortTimeString()}");
        //    T results = default;
        //    if (settings.Count() > 0)
        //    {
        //        var firstSetting = settings.FirstOrDefault();
        //        if (firstSetting.Id != "refresh")
        //        {
        //            if (settings?.Count() > 1)
        //            {
        //                _logger?.LogInformation($"FromSettings: settings found - {settings?.Count()}");
        //            }
        //            return results;
        //        }
        //    }
        //    _logger?.LogInformation($"FromSettings: {results?.Id}");
        //    return results;
        //}



        /// <summary>
        /// GetFilename
        /// </summary>
        /// <returns></returns>
        public virtual string GetFilename(string typeName = "Item", string schema = "json") => Common.GetFilepath(Id, typeName, schema);

        /// <summary>
        /// SetChildren
        /// </summary>
        /// <param name="items"></param>
        public void SetChildren<T>(List<T> items) where T : Item
        {
            //foreach(var child in items.Where(i => !string.IsNullOrEmpty(i.ParentId)))
            //{
            //    var parent = items.FirstOrDefault(i => i.Id == child.ParentId);
            //    if (parent.Children.Contains(child)) parent.Children.ToList().Add(child);
            //}
            //if (Children.Any())
            //{
            foreach (var item in items)
            {
                //var parent = items.FirstOrDefault(i => i.Id == item.ParentId);
                //if (!parent.Children.Contains(item)) parent.Children.ToList().Add(item);
                Children = items?.Where(i => i.ParentId == Id).ToList<Item>();

                foreach (var child in Children)
                {
                    child.SetChildren(items);
                }
            }
        
            //}

            //Children = items?.Where(i => i.ParentId == Id).ToList<Item>();

            //foreach (var item in Children)
            //{
            //    item.SetChildren(items);
            //}
        }

        /// <summary>
        /// ToJson
        /// </summary>
        /// <returns></returns>
        public virtual string ToJson() => JsonSerializer.Serialize(this);//App.SerializerService.SerializeAsync(this).Result;
        public virtual string ToXml() {
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(GetType());
                serializer.Serialize(stringwriter, this);
                return stringwriter.ToString();
            }
        }

        /// <summary>
        /// ToFile
        /// </summary>
        /// <returns></returns>
        public bool ToFile<T>(string filePath = null, string schema = "json") where T : Item
        {
            try
            {
                _logger?.LogInformation($"{typeof(T).Name}.ToFile({filePath}): {DateTime.Now.ToShortTimeString()} started");

                if (string.IsNullOrEmpty(filePath)) filePath = GetFilename(typeof(T).Name, schema);

                if (string.IsNullOrEmpty(filePath) || filePath.EndsWith($"{typeof(T).Name}..json")) return false;

                var content = schema == "xml" ? ToXml() : ToJson();
                if (string.IsNullOrEmpty(content)) return false;

                if (schema == "json") filePath = filePath.Replace("json", schema);
                new FileInfo(filePath).Directory.Create();
                File.WriteAllText(filePath, content);
            }
            catch (Exception exception)
            {
                _logger?.LogInformation($"{this}.ToFile({filePath}) ERROR:{exception}");
                return false;
            }
            return true;
        }
    }
}
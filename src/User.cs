using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppBlocks.Models
{
    public class User : IdentityUser
    {
        [JsonPropertyName("item")]
        public Item Item { get; set; }

        //[JsonPropertyName("password")]
        //public string Password { get; set; }

        //[JsonIgnore]
        //public string Json => JsonConvert.SerializeObject(this);

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public User() { }

        public User(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public User(string json)
        {
            if (string.IsNullOrEmpty(json)) return;

            //var user = JsonConvert.DeserializeObject<User>(json);
            var user = FromJson<User>(json);
            if (user == null) return;
            //UserId = user.UserId;
            Email = user.Email;
            //Password = user.Password;
            //Username = user.Username;
            //Image = user.Image;

            //if (string.IsNullOrEmpty(Image)) Image = "http://radicaldave.com/images/Personal/DavidWalker_Hardcore_170_bigger.jpg";
            //if (string.IsNullOrEmpty(Image)) Image = App.DefaultProfileImageUrl; // "logo.png";

            //ShirtSize = user.ShirtSize;
            //FirstName = user.FirstName;
            //LastName = user.LastName;
            //Phone = user.Phone;
            //Zipcode = user.Zipcode;
            //HideContactInfo = user.HideContactInfo;
            //JeepImage = "https://grouplings.blob.core.windows.net/grouplings-groups/JeepersAnonymous/Members/vicepresident%40jeepersanonymous.org.jpg";
            //JeepModel = user.JeepModel;
            //JeepName = user.JeepName;
            //JeepYear = user.JeepYear;
        }

        //public User(ProfileViewModel profileViewModel) : base(profileViewModel)
        //{

        //}

        public static bool Authenticate(string username, string password)
        {
            //if (string.IsNullOrEmpty(password)) password = $"{username}Blocks!";

            //if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;
            //var user = _userManager.FindByNameAsync(username);
            //if (user == null) return new StatusCodeResult(401);
            //if (!(await _signInManager.CheckPasswordSignInAsync(user, password, false)).Succeeded) return new StatusCodeResult(401);
            //return Ok(user);

            using (var serviceScope = ServiceActivator.GetScope())
            {
                if (serviceScope != null)
                {
                    var signInService = serviceScope.ServiceProvider.GetService(typeof(SignInManager<IdentityUser>));
                    if (signInService != null)
                    {
                        SignInManager<IdentityUser> signInManager = (SignInManager<IdentityUser>)signInService;
                        return signInManager.CheckPasswordSignInAsync(new IdentityUser(username), password, false).Result.Succeeded;
                    }
                    //ILoggerFactory loggerFactory = serviceScope.ServiceProvider.GetService<ILoggerFactory>();
                    //IOptionsMonitor<JwtConfiguration> option = (IOptionsMonitor<JwtConfiguration>)serviceScope.ServiceProvider.GetService(typeof(IOptionsMonitor<JwtConfiguration>));

                    /*
                        use you services
                    */
                }
            }



            var userRequest = new User();// { UserName = username };
            var results = FromJson<User>(userRequest.ToJson());

            if (results != null)
            {
                if (CurrentUser == null) 
                { 
                    CurrentUser = results;
                }
                else
                {
                    CurrentUser.Email = username;
                    //CurrentUser.Password = password;
                }
                //return string.IsNullOrEmpty($"{CurrentUser.Email}{CurrentUser.Password}");
                return string.IsNullOrEmpty(CurrentUser.Email);
            }
            return false;
        }

        public static bool IsAuthenticated
        {
            get
            {
                if (CurrentUser == null) return false;

                var results = CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.Email) && !string.IsNullOrEmpty(CurrentUser.PasswordHash) && !string.IsNullOrEmpty(CurrentUser.UserName);
                Trace.WriteLine($"{CurrentUser.UserName}.IsAuthenticated:{results}");
                return results;
            }
        }

        private static User currentUser;
        public static User CurrentUser
        {
            get
            {
                if (currentUser != null) return currentUser;

                return null;

                //return new User(Preferences.Get(Keys.UserKey, string.Empty));

                //var user = Application.Current.Properties.ContainsKey(Keys.UserKey) ? Application.Current.Properties[Keys.UserKey].ToString() : null;
                //var user = Preferences.Get(Keys.UserKey, string.Empty) ? Preferences.Get(Keys.UserKey].ToString() : null;
                //return new User(currentUser);
                //currentUser = new User(user);
                //return currentUser;
            }

            set
            {
                currentUser = value;
            }
        }
        //public static User CurrentUser()
        //{
        //	if (currentUser != null) return currentUser;

        //	var user = Application.Current.Properties.ContainsKey(Keys.UserKey) ? Application.Current.Properties[Keys.UserKey].ToString() : null;
        //	//return new User(currentUser);
        //	currentUser = new User(user);
        //	return currentUser;
        //}

        /// <summary>
        /// ToJson
        /// </summary>
        /// <returns></returns>
        //public new string ToJson() =>
        //    //return //;App.SerializerService.SerializeAsync(this).Result;
        //    JsonSerializer.Serialize(this);


        //    /// <summary>
        //    /// GetFilepath
        //    /// </summary>
        //    /// <param name="filePathOrId"></param>
        //    /// <returns></returns>
        //    public new static string GetFilepath(string filePathOrId)
        //    {
        //        if (System.IO.File.Exists(filePathOrId)) return filePathOrId;
        //        return $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\app_data\\blocks\\item.{filePathOrId}.json";
        //    }

        /// <summary>
        /// GetFilename
        /// </summary>
        /// <returns></returns>
        public string GetFilename(string typeName = "Item") => Common.GetFilepath(Email, typeName);

        public string ToJson() => JsonSerializer.Serialize(this);


        /// <summary>
        /// FromJson
        /// </summary>
        /// <param name="json"></param>
        public static T FromJson<T>(string json) where T : User
        {
            var typeName = typeof(T).FullName;
            Trace.WriteLine($"{typeof(Item).Name}.FromJson<{typeName}>({json}) started:{DateTime.Now.ToShortTimeString()}");

            var jsonFile = Common.FromFile(json, typeName);
            if (!string.IsNullOrEmpty(jsonFile)) json = jsonFile;

            if (typeName == "AppBlocks.Models.User")
            {
                json = Models.Settings.AppBlocksServiceUrl + "/account/authenticate";// Models.Settings.GroupId
            }

            if (json.ToLower().StartsWith("http") || json == Models.Settings.GroupId || string.IsNullOrEmpty(json))
            {
                return FromUri<T>(!string.IsNullOrEmpty(json) && json != Models.Settings.GroupId ? new Uri(json) : null);
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
                //item.SetChildren(items);
                return item;
            }
        }


        /// <summary>
        /// FromUri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static T FromUri<T>(Uri uri = null) where T : User
        {
            Trace.WriteLine($"{typeof(Item).Name}.FromUri({uri}) started:{DateTime.Now.ToShortTimeString()}");
            var content = string.Empty;
            try
            {
                if (uri == null) uri = new Uri(Models.Settings.AppBlocksBlocksServiceUrl + Models.Settings.GroupId);
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
        /// ToFile
        /// </summary>
        /// <returns></returns>
        public bool ToFile<T>(string filePath = null) where T : User
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
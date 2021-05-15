using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppBlocks.Models
{
    public class User : Member
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }

        //[JsonIgnore]
        //public string Json => JsonConvert.SerializeObject(this);

        public User()
        {

        }
        public User(string json)
        {
            if (string.IsNullOrEmpty(json)) return;

            //var user = JsonConvert.DeserializeObject<User>(json);
            var user = FromJson<User>(json);
            if (user == null) return;

            Email = user.Email;
            Password = user.Password;
            Username = user.Username;
            Image = user.Image;

            //if (string.IsNullOrEmpty(Image)) Image = "http://radicaldave.com/images/Personal/DavidWalker_Hardcore_170_bigger.jpg";
            //if (string.IsNullOrEmpty(Image)) Image = App.DefaultProfileImageUrl; // "logo.png";

            ShirtSize = user.ShirtSize;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Phone = user.Phone;
            Zipcode = user.Zipcode;
            HideContactInfo = user.HideContactInfo;
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
            if (string.IsNullOrEmpty(password)) password = $"{username}!";
            var userRequest = new User() { Username = username, Password = password };
            var results = FromJson<User>(userRequest.ToJson());

            if (results != null)
            {
                if (CurrentUser != null)
                {
                    CurrentUser.Email = username;
                    CurrentUser.Password = password;
                    return true;
                }
            }
            return false;
        }

        public static bool IsAuthenticated
        {
            get
            {
                if (CurrentUser == null) return false;

                var results = CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.Email) && !string.IsNullOrEmpty(CurrentUser.Password) && !string.IsNullOrEmpty(CurrentUser.Username);
                Trace.WriteLine($"{CurrentUser.Username}.IsAuthenticated:{results}");
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
        public override string GetFilename(string typeName = "Item") => GetFilepath(Email, typeName);

        public override string ToJson() => JsonSerializer.Serialize(this);
    }
}
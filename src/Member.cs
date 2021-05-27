using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppBlocks.Models
{
    public class Member : Item
    {
        //[DataMember(Name="id")]
        //public string Id { get; set; }

        [StringLength(450)]
        public string UserId { get; set; }

        [DataMember(Name="username")]
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [DataMember(Name="email")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [DataMember(Name="firstname")]
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }

        [DataMember(Name="lastname")]
        [JsonPropertyName("lastname")]
        public string LastName { get; set; }

        //[DataMember(Name="image")]
        //public string Image { get; set; }

        //public virtual ICollection<Item> Items { get; set; }

        [DataMember(Name="zipcode")]
        [JsonPropertyName("zipcode")]
        public string Zipcode { get; set; }

        //[DataMember(Name="phone")]
        //[JsonPropertyName("phone")]
        //public string Phone { get; set; }

        //[DataMember(Name="shirtsize")]
        //[JsonPropertyName("shirtsize")]
        //public string ShirtSize { get; set; }

        //[DataMember(Name="status")]
        //public string Status { get; set; }

        //[DataMember(Name = "typeid")]
        //[JsonPropertyName("typeid")]
        //public string TypeId { get; set; }

        //[DataMember(Name="hidecontactinfo")]
        //[JsonPropertyName("hidecontactinfo")]
        //public string HideContactInfo { get; set; }

        //[DataMember(Name="jeepimage")]
        //public string JeepImage { get; set; }

        //[DataMember(Name="jeepname")]
        //public string JeepName { get; set; }

        //[DataMember(Name="jeepmodel")]
        //public string JeepModel { get; set; }

        //[DataMember(Name="jeepyear")]
        //public string JeepYear { get; set; }

        //[DataMember(Name="jeep")]
        //public Jeep Jeep { get; set; }

        [IgnoreDataMember]
        public string FullName => $"{FirstName} {LastName}";
        [IgnoreDataMember]
        public string Message { get; set; }

        public Member()
        {

        }

        public Member(string json)
        {
            if (string.IsNullOrEmpty(json)) return;

            var member = JsonSerializer.Deserialize<Member>(json);

            UserId = member.UserId;
            Email = member.Email;
            //Phone = member.Phone;
            //Password = user.Password;
            Username = member.Username;
            Image = member.Image;
            if (string.IsNullOrEmpty(Image)) Image = "";// App.DefaultProfileImageUrl;
            //ShirtSize = member.ShirtSize;
            FirstName = member.FirstName;
            LastName = member.LastName;            
            Zipcode = member.Zipcode;
            //HideContactInfo = member.HideContactInfo;
            //JeepModel = member.JeepModel;
            //JeepName = member.JeepName;
            //JeepYear = member.JeepYear;
        }


        //public Member(ProfileViewModel profileViewModel)
        //{
        //    if (profileViewModel == null) return;

        //    Username = profileViewModel.Username;
        //    //Password = profileViewModel.Password;
        //    Image = profileViewModel.Image;
        //    if (string.IsNullOrEmpty(Image)) Image = App.DefaultProfileImageUrl;
        //    Email = profileViewModel.Email;
        //    Phone = profileViewModel.Phone;
        //    JeepModel = profileViewModel.JeepModel;
        //    JeepName = profileViewModel.JeepName;
        //    JeepYear = profileViewModel.JeepYear;
        //    HideContactInfo = profileViewModel.HideContactInfo;
        //    ShirtSize = profileViewModel.ShirtSize;
        //    Zipcode = profileViewModel.Zipcode;
        //}
    }
}
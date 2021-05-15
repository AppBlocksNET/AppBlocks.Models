using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AppBlocks.Models
{
    public class Setting
    {
        [DataMember(Name = "key")]
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [DataMember(Name = "value")]
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
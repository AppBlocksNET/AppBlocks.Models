using System.Collections.Generic;
using System.Windows.Input;

namespace AppBlocks.Models.Extensions
{
    public static class DictionaryExtensions
    {
        public static ICommand GetValue(this Dictionary<string, ICommand> dictionary, string key) => ((bool)dictionary?.ContainsKey(key)) ? dictionary[key] : null;

        public static TValue GetValue<TValue>(this Dictionary<string, TValue> dictionary, string key, string defaultValue = null) => (bool)(dictionary?.ContainsKey(key)) ? dictionary[key] : default;

        public static string GetValueOrDefault(this Dictionary<string, string> dictionary, string key, string defaultValue) //where TKey : ExtendedBindableObject// : notnull
            => dictionary?.GetValue(key, defaultValue);
    }
}
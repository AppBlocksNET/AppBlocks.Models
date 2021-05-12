using System.Collections.Generic;
using System.Windows.Input;

namespace AppBlocks.Models.Extensions
{
    public static class DictionaryExtensions
    {
        public static ICommand GetValue(this Dictionary<string, ICommand> dict, string key)
        {
            return dict.ContainsKey(key) ? dict[key] : null;
        }
    }
}
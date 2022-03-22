using System;
using System.Diagnostics;
using System.IO;

namespace AppBlocks.Models
{
    public static class Common
    {

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
        /// GetFilepath
        /// </summary>
        /// <param name="filePathOrId"></param>
        /// <returns></returns>
        public static string GetFilepath(string filePathOrId, string typeName = "Item", string schema = "json")
        {
            if (File.Exists(filePathOrId)) return filePathOrId;
            return $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\app_data\\blocks\\{typeName}.{filePathOrId}.{schema}";
        }

    }
}

using AppBlocks.Config;
using AppBlocks.Models.Extensions;
using System;
using System.Collections.Generic;

namespace AppBlocks.Models
{
    /// <summary>
    /// Settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// ApiId
        /// </summary>
        public static string ApiId = Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.ApiId", GroupId);

        /// <summary>
        /// AppSettings
        /// </summary>
        public static Dictionary<string, string> AppSettings = Factory.GetConfig().AppSettings();

        /// <summary>
        /// Env
        /// </summary>
        public static string Env => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        /// <summary>
        /// GroupId //5E11C4A9-D602-EB11-A38D-BC9A78563412
        /// </summary>
        public static string GroupId = Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.GroupId", "");
        //public static string GroupId = System.Configuration.ConfigurationManager.AppSettings.Get("AppBlocks:GroupId") ?? "5E11C4A9-D602-EB11-A38D-BC9A78563412";
        //(id = !AssemblyName.ToLower().EndsWith(".uwp") ? AssemblyName : AssemblyName.Substring(0, AssemblyName.Length - 4));
        //public static string Id
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(id)) return id;
        //        //var assembly = typeof(LoginViewModel).Assembly;
        //        //var attributes = assembly.GetCustomAttributes(typeof(GuidAttribute), true);
        //        //var attribute = (GuidAttribute)attributes?[0];
        //        //var id  = attribute.Value;
        //        //return id;
        //        //return Assembly.GetExecutingAssembly().FullName;

        //        //TODO: customattribute on the assembly with ProjectGuid?
        //        //var id = Assembly.GetEntryAssembly().GetName().Name;
        //        //var id = Name;
        //        id = AssemblyName;
        //        if (id.ToLower().EndsWith(".uwp")) id = id.Substring(0, id.Length - 4);
        //        //Trace.WriteLine($"********** {Name}-REFLECTION - to get id:{Id}");
        //        return id;
        //    }
        //}
        /// <summary>
        /// GroupTypeId
        /// </summary>
        public static string GroupTypeId = Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.GroupTypeId", "78618B97-39FF-EA11-A38B-BC9A78563412");

        /// <summary>
        /// AppBlocksServiceBUrl
        /// </summary>
        public static string AppBlocksServiceUrl = Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.ServiceUrl", "https://appblocks.net/api/");

        /// <summary>
        /// AppBlocksBlocksServiceBUrl
        /// </summary>
        public static string AppBlocksBlocksServiceUrl = Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.BlocksServiceUrl", "https://appblocks.net/api/blocks/index/");
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.VSProjectProperty
{
    /// <summary>
    /// Contains functionality related to assembly info.
    /// </summary>
    [CakeAliasCategory("Visual Studio Project File Property Helper")]
    public static class VSProjectPropertyAliases
    {
        /// <summary>
        /// modify the properties of  .csproj file
        /// </summary>
        /// <param name="context">Cake context</param>
        /// <param name="projectFilePath">path of .csproj file</param>
        /// <param name="keyValues">properties key values</param>
        /// <param name="configure">build configure</param>
        /// <param name="platform">build configure</param>
        [CakeMethodAlias]
        public static void SetVSProjectProperties(this ICakeContext context, FilePath projectFilePath, IDictionary<string, string> keyValues, string configure = "Release", string platform = "AnyCPU")
        {
            if (context == null) throw new ArgumentNullException("context");
            if (projectFilePath == null) throw new ArgumentNullException("projectFilePath");
            if (projectFilePath.IsRelative) projectFilePath = projectFilePath.MakeAbsolute(context.Environment);

            var file = context.FileSystem.GetFile(projectFilePath);
            if (!file.Exists)
            {
                const string format = "Project file '{0}' does not exist.";
                var message = string.Format(CultureInfo.InvariantCulture, format, projectFilePath.FullPath);
                throw new CakeException(message);
            }

            configure = configure.ToLower();
            platform = platform.ToLower();

            ProjectFileHelper helper = new ProjectFileHelper(file.Path.FullPath);
            foreach (var pair in keyValues)
            {
                helper.SetProperty(pair.Key, pair.Value, configure, platform);
            }
            helper.Save();
        }


        /// <summary>
        /// modify the properties of  .csproj file
        /// </summary>
        /// <param name="context">Cake context</param>
        /// <param name="projectFilePath">path of .csproj file</param>
        /// <param name="key">properties key values</param>
        /// <param name="value">properties key values</param>
        /// <param name="configure">build configure</param>
        /// <param name="platform">build configure</param>
        [CakeMethodAlias]
        public static void SetVSProjectPropertie(this ICakeContext context, FilePath projectFilePath, string key, string value, string configure = "Release", string platform = "AnyCPU")
        {
            if (context == null) throw new ArgumentNullException("context");
            if (projectFilePath == null) throw new ArgumentNullException("projectFilePath");
            if (projectFilePath.IsRelative) projectFilePath = projectFilePath.MakeAbsolute(context.Environment);

            var file = context.FileSystem.GetFile(projectFilePath);
            if (!file.Exists)
            {
                const string format = "Project file '{0}' does not exist.";
                var message = string.Format(CultureInfo.InvariantCulture, format, projectFilePath.FullPath);
                throw new CakeException(message);
            }

            configure = configure.ToLower();
            platform = platform.ToLower();

            ProjectFileHelper helper = new ProjectFileHelper(file.Path.FullPath);
            helper.SetProperty(key, value, configure, platform);
            helper.Save();
        }



        /// <summary>
        /// get properties from  .csproj file
        /// </summary>
        /// <param name="context">Cake context</param>
        /// <param name="projectFilePath">path of .csproj file</param>
        /// <param name="keys">properties keys</param>
        /// <param name="configure">build configure</param>
        /// <param name="platform">build configure</param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static IDictionary<string, string> GetVSProjectProperties(this ICakeContext context, FilePath projectFilePath, IEnumerable<string> keys, string configure = "Release", string platform = "AnyCPU")
        {
            if (context == null) throw new ArgumentNullException("context");
            if (projectFilePath == null) throw new ArgumentNullException("projectFilePath");
            if (projectFilePath.IsRelative) projectFilePath = projectFilePath.MakeAbsolute(context.Environment);

            var file = context.FileSystem.GetFile(projectFilePath);
            if (!file.Exists)
            {
                const string format = "Project file '{0}' does not exist.";
                var message = string.Format(CultureInfo.InvariantCulture, format, projectFilePath.FullPath);
                throw new CakeException(message);
            }

            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            configure = configure.ToLower();
            platform = platform.ToLower();

            ProjectFileHelper helper = new ProjectFileHelper(file.Path.FullPath);
            foreach (var key in keys)
            {
                string value = helper.GetProperty(key, configure, platform);
                if (!string.IsNullOrEmpty(value))
                {
                    keyValues.Add(key, value);
                }
            }
            return keyValues;
        }


        /// <summary>
        /// get properties from  .csproj file
        /// </summary>
        /// <param name="context">Cake context</param>
        /// <param name="projectFilePath">path of .csproj file</param>
        /// <param name="key">propertie key</param>
        /// <param name="configure">build configure</param>
        /// <param name="platform">build configure</param>
        /// <returns>value</returns>
        [CakeMethodAlias]
        public static string GetVSProjectPropertie(this ICakeContext context, FilePath projectFilePath, string key, string configure = "Release", string platform = "AnyCPU")
        {
            if (context == null) throw new ArgumentNullException("context");
            if (projectFilePath == null) throw new ArgumentNullException("projectFilePath");
            if (projectFilePath.IsRelative) projectFilePath = projectFilePath.MakeAbsolute(context.Environment);

            var file = context.FileSystem.GetFile(projectFilePath);
            if (!file.Exists)
            {
                const string format = "Project file '{0}' does not exist.";
                var message = string.Format(CultureInfo.InvariantCulture, format, projectFilePath.FullPath);
                throw new CakeException(message);
            }

            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            configure = configure.ToLower();
            platform = platform.ToLower();

            ProjectFileHelper helper = new ProjectFileHelper(file.Path.FullPath);
            return helper.GetProperty(key, configure, platform); ;
        }

        /// <summary>
        /// a helper method for myself. it will throw an exception if the input string is null or empty.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static string ValidateString_(this ICakeContext context, string str)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new CakeException("string is null or empty.");
            return str;
        }


    }
}

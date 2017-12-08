using Cake.Core;
using System.Xml;

namespace Cake.VSProjectProperty
{
    /// <summary>
    /// The Project File info helper.
    /// </summary>
    public sealed class VSProjectPropertyHelper
    {
        private string _path = null;
        private XmlDocument _doc = null;
        private string _configuration = null;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="projectFilePath"></param>
        /// <param name="configure"></param>
        public VSProjectPropertyHelper(string projectFilePath, string configure)
        {
            if (!System.IO.File.Exists(projectFilePath)) throw new CakeException("Project file not exist.");
            _path = projectFilePath;

            _doc = new XmlDocument();
            _doc.Load(_path);

            if (string.IsNullOrEmpty(configure)) throw new CakeException("no configure found.");
            _configuration = configure;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void SetProperty(string key, string value, string config = null)
        {
            XmlNode root = _doc.DocumentElement;
            if (root == null || root.Name != "Project") throw new CakeException("Project file is not a valid .csproj file.");

            string configuration = string.IsNullOrEmpty(config) ? _configuration : config;

            foreach (XmlNode group in root.ChildNodes)
            {
                if (group.Name != "PropertyGroup") continue;

                bool isConfig = true;
                foreach (XmlAttribute attri in group.Attributes)
                {
                    if (attri.Name != "Condition") continue;
                    if (!attri.InnerText.Contains(configuration)) isConfig = false;
                    break;
                }
                if (!isConfig) continue;

                foreach (XmlNode item in group.ChildNodes)
                {
                    if (item.Name != key) continue;
                    item.InnerText = value;
                    break;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public string GetProperty(string key, string config = null)
        {
            XmlNode root = _doc.DocumentElement;
            if (root == null || root.Name != "Project") throw new CakeException("Project file is not a valid .csproj file.");

            string configuration = string.IsNullOrEmpty(config) ? _configuration : config;

            foreach (XmlNode group in root.ChildNodes)
            {
                if (group.Name != "PropertyGroup") continue;

                bool isConfig = true;
                foreach (XmlAttribute attri in group.Attributes)
                {
                    if (attri.Name != "Condition") continue;
                    if (!attri.InnerText.Contains(configuration)) isConfig = false;
                    break;
                }
                if (!isConfig) continue;

                foreach (XmlNode item in group.ChildNodes)
                {
                    if (item.Name != key) continue;
                    return item.InnerText;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            _doc.Save(_path);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reload()
        {
            _doc = null;
            _doc = new XmlDocument();
            _doc.Load(_path);
        }
    }


}

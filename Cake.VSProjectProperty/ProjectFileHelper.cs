using System.Xml;

using Cake.Core;

namespace Cake.VSProjectProperty
{
	/// <summary>
	/// The Project File info helper.
	/// </summary>
	public sealed class ProjectFileHelper
	{
		private string _path = null;
		private XmlDocument _doc = null;

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="projectFilePath"></param>
		public ProjectFileHelper(string projectFilePath)
		{
			if (!System.IO.File.Exists(projectFilePath)) throw new CakeException("Project file not exist.");
			_path = projectFilePath;

			_doc = new XmlDocument();
			_doc.Load(_path);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void SetProperty(string key, string value)
		{
			XmlNode root = _doc.DocumentElement;
			if (root == null || root.Name != "Project") throw new CakeException("not a valid Project file.");

			foreach (XmlNode group in root.ChildNodes)
			{
				if (group.Name != "PropertyGroup") continue;

				foreach (XmlNode item in group.ChildNodes)
				{
					if (item.ChildNodes.Count == 1)
					{
						if (item.Name != key) continue;
						item.InnerText = value;
						return;
					}
				}

				var elm = _doc.CreateElement(key);
				elm.InnerText = value;
				group.AppendChild(elm);
				break;
			}
		}




		/// <summary>
		///  GetProperty
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetProperty(string key)
		{
			XmlNode root = _doc.DocumentElement;
			if (root == null || root.Name != "Project") throw new CakeException("Project file is not a valid .csproj file.");

			foreach (XmlNode group in root.ChildNodes)
			{
				if (group.Name != "PropertyGroup") continue;

				foreach (XmlNode item in group.ChildNodes)
				{
					if (item.ChildNodes.Count == 1)
					{
						if (item.Name != key) continue;
						return item.InnerText;
					}
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

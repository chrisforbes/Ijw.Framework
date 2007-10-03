using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;

namespace Ijw.Updates
{
	public class XmlUpdateProvider : IUpdateProvider
	{
		XmlDocument doc = new XmlDocument();

		XmlUpdateProvider(XmlDocument doc)
		{
			this.doc = doc;
		}

		public UpdateInfo GetLatestVersion(string productName)
		{
			XmlElement product = doc.SelectSingleNode("/products/product[@name=\""+ productName +"\"]") as XmlElement;
			Version updateVersion = new Version(product.SelectSingleNode("./version").InnerText);

			string url = product.SelectSingleNode("./url").InnerText;
			string msi = product.SelectSingleNode("./msi").InnerText;
			string lastUpdate = product.SelectSingleNode("./lastupdate").InnerText;

			return new UpdateInfo(productName, updateVersion, url, msi, DateTime.Parse(lastUpdate));
		}

		public static IUpdateProvider FromFile(string filename)
		{
			try
			{
				XmlDocument document = new XmlDocument();
				document.Load(filename);
				return new XmlUpdateProvider(document);
			}
			catch (XmlException)
			{
				return null;
			}
		}

		public static IUpdateProvider FromInternet(Uri uri)
		{
			try
			{
				WebClient wc = new WebClient();
				wc.Headers.Add(HttpRequestHeader.Referer, "http://update.ijw.co.nz/");
				string content = wc.DownloadString(uri);

				XmlDocument document = new XmlDocument();
				document.LoadXml(content);
				return new XmlUpdateProvider(document);
			}
			catch (XmlException)
			{
				return null;
			}
		}
	}
}

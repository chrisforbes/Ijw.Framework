using System;
using System.Collections.Generic;
using System.Text;

namespace IjwFramework.Updates
{
	public class UpdateInfo
	{
		readonly string productName;
		readonly Version version;
		readonly Uri url, msiUrl;
		readonly DateTime lastUpdate;

		public string ProductName { get { return productName; } }
		public Version Version { get { return version; } }
		public Uri Url { get { return url; } }
		public Uri MsiUrl { get { return msiUrl; } }
		public DateTime LastUpdate { get { return lastUpdate; } }

		public UpdateInfo(string productName, Version version, string url, string msiUrl, DateTime lastUpdate)
		{
			this.productName = productName;
			this.version = version;
			this.url = new Uri(url);
			this.msiUrl = new Uri(msiUrl);
			this.lastUpdate = lastUpdate;
		}
	}
}

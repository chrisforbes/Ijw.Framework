using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace IjwFramework.Updates
{
	public class UpdateManager
	{
		readonly string productName;
		readonly IUpdateProvider updateProvider;
		readonly Version version;

		string Version { get { return version.ToString(); } }

		public UpdateManager(string productName, string version, IUpdateProvider updateProvider)
		{
			this.productName = productName;
			this.version = new Version(version);
			this.updateProvider = updateProvider;
		}

		public UpdateInfo Update()
		{
			UpdateInfo info = updateProvider.GetLatestVersion(productName);
			return (info == null || info.Version <= version) ? null : info;
		}

		public static void CheckForUpdates(string productName, string version)
		{
			const string productListUrl = "http://www.ijw.co.nz/products.xml";

			try
			{
				IUpdateProvider provider = XmlUpdateProvider.FromInternet(new Uri(productListUrl));
				UpdateManager manager = new UpdateManager(productName, version, provider);
				UpdateInfo info = manager.Update();

				if (info == null)
				{
					MessageBox.Show("You already have the latest version of " + productName,
						"Check for Updates", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				string text = "A new version of {0} is available.\n" +
					"Your version: {1}\nLatest version: {2} (released {3})\n" +
					"Do you want to download it?";

				string hax = string.Format(text, productName, manager.Version, info.Version, info.LastUpdate.ToLongDateString());

				if (DialogResult.Yes == MessageBox.Show(hax, "Check for Updates", MessageBoxButtons.YesNo,
					MessageBoxIcon.Information))
				{
					Process.Start(info.Url.ToString());
				}
			}
			catch (Exception e)
			{
				MessageBox.Show("An error occurred while checking for updates.\n" + e.Message, "Check for Updates",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}

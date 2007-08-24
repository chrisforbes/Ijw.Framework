using System;
using System.Collections.Generic;
using System.Text;

namespace IjwFramework.Updates
{
	interface IUpdateProvider
	{
		UpdateInfo GetLatestVersion(string productName);
	}
}

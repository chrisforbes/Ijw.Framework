using System;
using System.Collections.Generic;
using System.Text;

namespace IjwFramework.Updates
{
	public interface IUpdateProvider
	{
		UpdateInfo GetLatestVersion(string productName);
	}
}

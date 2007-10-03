using System;
using System.Collections.Generic;
using System.Text;

namespace Ijw.Updates
{
	public interface IUpdateProvider
	{
		UpdateInfo GetLatestVersion(string productName);
	}
}

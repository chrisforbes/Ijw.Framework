using System;
using System.Collections.Generic;
using System.Text;
using IjwFramework.Updates;

namespace IjwFrameworkTests
{
	class MockUpdateProvider : IUpdateProvider
	{
		Dictionary<string, UpdateInfo> products = new Dictionary<string, UpdateInfo>();

		public void AddProduct(UpdateInfo info)
		{
			products.Add(info.ProductName, info);
		}

		public UpdateInfo GetLatestVersion(string productName)
		{
			UpdateInfo info;
			return products.TryGetValue(productName, out info) ? info : null;
		}
	}
}

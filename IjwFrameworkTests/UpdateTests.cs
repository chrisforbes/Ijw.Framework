using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using IjwFramework.Updates;

namespace IjwFrameworkTests
{
	[TestFixture]
	public class Tests
	{
		const string tempuri = "http://tempuri.org/";

		[Test]
		public void TestUnavailableProduct()
		{
			IUpdateProvider provider = new MockUpdateProvider();
			UpdateManager manager = new UpdateManager("Fake Product", "1.0", provider);

			UpdateInfo info = manager.Update();
			Assert.IsNull(info);
		}

		[Test]
		public void TestHasOldVersion()
		{
			MockUpdateProvider provider = new MockUpdateProvider();

			provider.AddProduct(new UpdateInfo("Fake Product", new Version("2.0"),
				tempuri, tempuri, DateTime.Now));

			UpdateManager manager = new UpdateManager("Fake Product", "1.0", provider);
			UpdateInfo update = manager.Update();

			Assert.IsNotNull(update);
		}

		[Test]
		public void TestHasSameVersion()
		{
			MockUpdateProvider provider = new MockUpdateProvider();

			provider.AddProduct(new UpdateInfo("Fake Product", new Version("1.0"),
				tempuri, tempuri, DateTime.Now));

			UpdateManager manager = new UpdateManager("Fake Product", "1.0", provider);
			UpdateInfo update = manager.Update();

			Assert.IsNull(update);
		}

		[Test]
		public void TestHasNewerVersion()
		{
			MockUpdateProvider provider = new MockUpdateProvider();

			provider.AddProduct(new UpdateInfo("Fake Product", new Version("1.0"),
				tempuri, tempuri, DateTime.Now));

			UpdateManager manager = new UpdateManager("Fake Product", "2.0", provider);
			UpdateInfo update = manager.Update();

			Assert.IsNull(update);
		}
	}
}

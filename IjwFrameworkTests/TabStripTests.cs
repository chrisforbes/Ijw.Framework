using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using IjwFramework.Ui;

namespace IjwFrameworkTests
{
	[TestFixture]
	public class TabStripTests
	{
		TabStrip<string> tabControl = new TabStrip<string>();
		static string value1 = "Foo", value2 = "Bar", value3 = "schwartz";
		
		[Test]
		public void TabStripTest1()
		{
			tabControl.Add(value1);
			tabControl.Add(value2);
			Assert.AreEqual(2, tabControl.Count);
			tabControl.Close(value2);
			Assert.AreEqual(1, tabControl.Count);
			tabControl.Add(value3);
			tabControl.CloseAll();
			Assert.AreEqual(0, tabControl.Count);
		}

		[Test]
		public void TabStripTest2()
		{
			tabControl.Add(value1);
			tabControl.Add(value2);
			Assert.AreEqual(value2, tabControl.Current);
			tabControl.CloseCurrent();
			Assert.AreEqual(value1, tabControl.Current);
		}

		[Test]
		public void TabStripTest3()
		{
			List<string> tabs = new List<string>();
			tabs.Add(value1);
			tabs.Add(value2);
			tabControl.Add(value1);
			tabControl.Add(value2);
			List<string> values = new List<string>();
			values.AddRange(tabControl.Items);
			CollectionAssert.AreEqual(tabs, values);
		}
	}
}

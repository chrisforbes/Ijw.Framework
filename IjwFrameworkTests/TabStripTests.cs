using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using IjwFramework.TabStrip;

namespace IjwFrameworkTests
{
	[TestFixture]
	public class TabStripTests
	{
		TabStripControl<string> tabControl = new TabStripControl<string>();
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

		[Test]
		public void TabStripTest4()
		{
			tabControl.Add(value1);
			tabControl.Add(value2);
			TabIterator<string> iterator = tabControl.Iterator;
			Assert.AreEqual(value2, iterator.Current.Content);
			iterator.MovePrevious();
			Assert.AreEqual(value1, iterator.Current.Content);
			iterator.MoveNext();
			Assert.AreEqual(value2, iterator.Current.Content);
		}
	}
}

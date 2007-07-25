using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using IjwFramework.Types;

namespace IjwFrameworkTests
{
	[TestFixture]
	public class TypeTests
	{
		[Test]
		public void PairTest1()
		{
			Pair<int, int> p = new Pair<int, int>(0, 1);
			Assert.AreEqual(0, p.First);
			Assert.AreEqual(1, p.Second);
		}

		[Test]
		public void PairTest2()
		{
			Pair<int, int> p = new Pair<int, int>(0, 1);
			Pair<int, int> q = new Pair<int, int>(0, 1);
			Assert.IsFalse(p != q);
			Assert.IsTrue(p == q);
			q.First = 1;
			Assert.IsTrue(p != q);
			Assert.IsFalse(p == q);
		}
	}
}

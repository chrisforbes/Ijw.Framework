using System;
using System.Collections.Generic;
using System.Text;

namespace IjwFramework.Types
{
	public static class PairComparison
	{
		public static int AscendingFirst<T, U>(Pair<T, U> a, Pair<T, U> b)
			where T : IComparable<T>
		{
			return a.First.CompareTo(b.First);
		}

		public static int AscendingSecond<T, U>(Pair<T, U> a, Pair<T, U> b)
			where U : IComparable<U>
		{
			return a.Second.CompareTo(b.Second);
		}

		public static int DescendingFirst<T, U>(Pair<T, U> a, Pair<T, U> b)
			where T : IComparable<T>
		{
			return -a.First.CompareTo(b.First);
		}

		public static int DescendingSecond<T, U>(Pair<T, U> a, Pair<T, U> b)
			where U : IComparable<U>
		{
			return -a.Second.CompareTo(b.Second);
		}
	}
}

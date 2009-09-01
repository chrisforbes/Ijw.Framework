using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ijw.Framework.v35
{
	public static class Extensions
	{
		/// <summary>
		/// Shorthand for string.Format()
		/// </summary>
		public static string F(this string s, params object[] args)
		{ return string.Format(s, args); }

		/// <summary>
		/// Repeats the specified sequence. For example, [1,2,3] becomes [1,2,3, 1,2,3, 1,...]
		/// </summary>
		public static IEnumerable<T> Cycle<T>(this IEnumerable<T> seq)
		{
			for (; ; )
				foreach (var t in seq) yield return t;
		}
	}
}

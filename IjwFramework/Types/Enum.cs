using System;
using System.Collections.Generic;
using System.Text;

namespace IjwFramework.Types
{
	public static class Enum<T>
	{
		public static bool TryParse(string s, out T value)
		{
			try
			{
				value = Parse(s);
				return true;
			}
			catch
			{
				value = default(T);
				return false;
			}
		}

		public static T Parse(string s) { return (T)Enum.Parse(typeof(T), s, true); }
		public static T Parse(string s, T defaultValue) { T t; return TryParse(s, out t) ? t : defaultValue; }

		public static T[] Values { get { return (T[])Enum.GetValues(typeof(T)); } }
		public static string[] Names { get { return Enum.GetNames(typeof(T)); } }
	}
}

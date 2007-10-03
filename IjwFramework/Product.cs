using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace IjwFramework
{
	public static class Product
	{
		static T GetAttribute<T>()
			where T : Attribute
		{
			T[] attributes = (T[])Assembly.GetEntryAssembly().GetCustomAttributes(typeof(T), false);
			return attributes.Length == 0 ? null : attributes[0];
		}

		public static string Name { get { return GetAttribute<AssemblyProductAttribute>().Product; } }
		public static string Description { get { return GetAttribute<AssemblyDescriptionAttribute>().Description; } }
		public static string Copyright { get { return GetAttribute<AssemblyCopyrightAttribute>().Copyright; } }
		public static string Company { get { return GetAttribute<AssemblyCompanyAttribute>().Company; } }
		public static string Version { get { return Assembly.GetEntryAssembly().GetName().Version.ToString(); } }

		public static string ShortVersion
		{
			get
			{
				string s = Version;
				return s.Substring(0, 1 + s.LastIndexOfAny("123456789".ToCharArray()));
			}
		}
	}
}

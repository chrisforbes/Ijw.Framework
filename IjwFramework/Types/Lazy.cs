using System;
using System.Collections.Generic;
using System.Text;
using IjwFramework.Delegates;

namespace IjwFramework.Types
{
	public class Lazy<T>
	{
		Provider<T> p;
		T value;

		public Lazy(Provider<T> p)
		{
			if (p == null)
				throw new ArgumentNullException();

			this.p = p;
		}

		public T Value
		{
			get
			{
				if (p == null)
					return value;

				value = p();
				p = null;
				return value;
			}
		}
	}

	public static class Lazy
	{
		public static Lazy<T> New<T>(Provider<T> p) { return new Lazy<T>(p); }
	}
}

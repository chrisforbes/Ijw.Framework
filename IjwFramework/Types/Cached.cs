using System;
using System.Collections.Generic;
using System.Text;
using IjwFramework.Delegates;

namespace IjwFramework.Types
{
	public class Cached<T>
	{
		Lazy<T> inner;
		Provider<T> p;

		public Cached(Provider<T> p)
		{
			if (p == null)
				throw new ArgumentNullException();

			this.p = p;
		}

		public void Invalidate()
		{
			inner = null;
		}

		public T Value
		{
			get
			{
				inner = inner ?? Lazy.New(p);
				return inner.Value;
			}
		}
	}

	public static class Cached
	{
		public static Cached<T> New<T>(Provider<T> p) { return new Cached<T>(p); }
	}
}

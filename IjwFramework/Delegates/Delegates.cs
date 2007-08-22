using System;
using System.Collections.Generic;

namespace IjwFramework.Delegates
{
	public delegate T Provider<T>();
	public delegate T Provider<T, U>(U u);
	public delegate T Provider<T, U, V>(U u, V v);

	public delegate void Action();
	public delegate void Action<T,U>(T t, U u);
	public delegate void Action<T,U,V>(T t, U u, V v);

	// predicates, and simple combinators over predicates

	public static class Predicates
	{
		public static bool Always<T>(T ignored) { return true; }
		public static bool Never<T>(T ignored) { return false; }

		public static Predicate<T> Or<T>(params Predicate<T>[] a)
		{
			return delegate(T t)
			{
				foreach (Predicate<T> p in a)
					if (p(t))
						return true;
				return false;
			};
		}

		public static Predicate<T> And<T>(params Predicate<T>[] a)
		{
			return delegate(T t)
			{
				foreach (Predicate<T> p in a)
					if (!p(t))
						return false;
				return false;
			};
		}

		public static Predicate<T> Not<T>(Predicate<T> p)
		{
			return delegate(T t) { return !p(t); };
		}
	}

	// helper iterators

	public static class Iterators
	{
		public static IEnumerable<T> Yield<T>(params T[] t) { return t; }
	}
}
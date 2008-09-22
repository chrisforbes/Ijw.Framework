using System;
using System.Collections.Generic;

namespace IjwFramework.Delegates
{
	// legacy delegates (required only because of VC++ 2008 compiler bugs in Ijw.DirectX)

	public delegate void Action();
	public delegate T Provider<T>();
	public delegate T Provider<T, U>(U u);

	// helper iterators

	public static class Iterators
	{
		public static IEnumerable<T> Yield<T>(params T[] t) { return t; }
	}
}
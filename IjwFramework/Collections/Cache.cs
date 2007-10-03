using System;
using System.Collections.Generic;
using IjwFramework.Delegates;
using System.Collections;

namespace IjwFramework.Collections
{
	public class Cache<T, U> : IEnumerable<KeyValuePair<T, U>>
	{
		Dictionary<T, U> hax = new Dictionary<T, U>();
		Provider<U, T> loader;

		public Cache(Provider<U, T> loader)
		{
			if (loader == null)
				throw new ArgumentNullException();

			this.loader = loader;
		}

		public U this[T key]
		{
			get
			{
				U result;
				if (!hax.TryGetValue(key, out result))
					hax.Add(key, result = loader(key));

				return result;
			}
		}

		public IEnumerator<KeyValuePair<T, U>> GetEnumerator() { return hax.GetEnumerator(); }

		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

		public IEnumerable<T> Keys { get { return hax.Keys; } }
		public IEnumerable<U> Values { get { return hax.Values; } }
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace IjwFramework.Collections
{
	public class Set<T> : ICollection<T>
	{
		Dictionary<T, bool> inner = new Dictionary<T, bool>();

		public void Add(T item) { inner.Add(item, false); }
		public void Clear() { inner.Clear(); }
		public bool Contains(T item) { return inner.ContainsKey(item); }
		public void CopyTo(T[] array, int arrayIndex) { throw new NotImplementedException(); }
		public int Count { get { return inner.Count; } }
		public bool IsReadOnly { get { return false; } }
		public bool Remove(T item) { return inner.Remove(item); }
		public IEnumerator<T> GetEnumerator() { return inner.Keys.GetEnumerator(); }
		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

		public Set() { }

		public Set(IEnumerable<T> src)
		{
			foreach (T t in src)
				Add(t);
		}
	}
}

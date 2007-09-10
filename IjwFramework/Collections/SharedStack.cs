using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace IjwFramework.Collections
{
	class StackNode<T>
	{
		public StackNode<T> next;
		public T item;

		public StackNode(T item, StackNode<T> next)
		{
			this.item = item;
			this.next = next;
		}
	}

	public class SharedStack<T> : IEnumerable<T>
	{
		StackNode<T> top;

		public void Push(T item)
		{
			top = new StackNode<T>(item, top);
		}

		public T Pop()
		{
			if (top == null)
				throw new InvalidOperationException("The stack is empty");

			StackNode<T> oldTop = top;
			top = top.next;
			return oldTop.item;
		}

		public T Peek()
		{
			if (top == null)
				throw new InvalidOperationException("The stack is empty");

			return top.item;
		}

		public bool Empty { get { return top == null; } }

		public SharedStack() { }
		public SharedStack(SharedStack<T> other)
		{
			if (other != null)
				top = other.top;
		}

		public IEnumerator<T> GetEnumerator() { return Iterate().GetEnumerator(); }

		IEnumerable<T> Iterate()
		{
			StackNode<T> x = top;
			while (x != null)
			{
				yield return x.item;
				x = x.next;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using IjwFramework.Delegates;

namespace IjwFramework.Ui
{
	class TabIterator<T>
		where T : class
	{
		int index;
		TabStrip<T> outer;
		Tab<T> current;

		public TabIterator(TabStrip<T> outer)
		{
			this.outer = outer;
			outer.Changed += delegate { Update(); };
		}

		void Update()
		{
			int newIndex = outer.IndexOf(Current);
			if (newIndex >= 0)
				index = newIndex;
			else
			{
				Current = outer.GetTab(Math.Min(index, outer.Count - 1));
				index = outer.IndexOf(Current);
			}
		}

		public void MoveNext()
		{
			index = (index + 1 < outer.Count) ? index + 1 : 0;
			Current = outer.GetTab(index);
		}

		public void MovePrevious()
		{
			index = (index > 0) ? index - 1 : outer.Count - 1;
			Current = outer.GetTab(index);
		}

		public Tab<T> Current
		{
			get { return current; }
			set
			{
				index = outer.IndexOf(value);
				current = value;
				outer.Invalidate();
				Changed();
			}
		}

		public event Action Changed = delegate { };
	}
}

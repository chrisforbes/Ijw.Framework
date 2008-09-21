using System;
using System.Collections.Generic;
using System.Text;

namespace IjwFramework.Ui
{
	public class ColumnCollection
	{
		List<IColumn> columns = new List<IColumn>();

		internal int GetLeft(IColumn c)
		{
			int left = 0;
			foreach (IColumn i in columns)
				if (i == c)
					return left;
				else
					left += i.Width;

			throw new InvalidOperationException("The column is not in the collection");
		}

		public IColumn CreateFixedWidth(string name, int width, Action<IColumn, Painter, Node> a )
		{
			IColumn c = new FixedWidthColumn(name, width, this, a);
			columns.Add(c);
			return c;
		}

		public IColumn CreateAutoWidth(string name, Action<IColumn, Painter, Node> a)
		{
			IColumn c = new AutoWidthColumn(name, this, a);
			columns.Add(c);
			return c;
		}

		public Action<int> WidthUpdatedHandler { get { return Update; } }

		int autoWidth = 0;

		void Update(int width)
		{
			int autoCount = 0;

			foreach (IColumn c in columns)
				if (c is AutoWidthColumn)
					autoCount++;
				else
					width -= c.Width;

			autoWidth = width / autoCount;
		}

		public void Render(Painter p, Node n)
		{
			foreach (IColumn c in columns)
				c.Render(p, n);
		}

		public void RenderCustom(Painter p, Node n, Action<IColumn, Painter, Node> a)
		{
			foreach (IColumn c in columns)
				a(c, p, n);
		}

		public int AutoWidth { get { return autoWidth; } }
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using IjwFramework.Delegates;

namespace IjwFramework.Ui.Tree
{
	class FixedWidthColumn : IColumn
	{
		readonly string name;
		readonly int width;
		readonly ColumnCollection owner;
		readonly Action<IColumn, Painter, Node> a;

		public FixedWidthColumn(string name, int width, ColumnCollection owner, Action<IColumn, Painter, Node> a)
		{
			this.name = name;
			this.width = width;
			this.owner = owner;
			this.a = a;
		}

		public string Name { get { return name; } }
		public int Width { get { return width; } }
		public int Left { get { return owner.GetLeft(this); } }

		public void Render(Painter p, Node n)
		{
			a(this, p, n);
		}
	}
}

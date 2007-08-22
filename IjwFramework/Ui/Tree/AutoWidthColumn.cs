using System;
using System.Collections.Generic;
using System.Text;
using IjwFramework.Delegates;

namespace IjwFramework.Ui.Tree
{
	class AutoWidthColumn : IColumn
	{
		readonly string name;
		readonly ColumnCollection owner;
		readonly Action<IColumn, Painter, Node> a;

		public AutoWidthColumn(string name, ColumnCollection owner, Action<IColumn, Painter, Node> a)
		{
			this.name = name;
			this.owner = owner;
			this.a = a;
		}

		public string Name { get { return name; } }
		public int Width { get { return owner.AutoWidth; } }
		public int Left { get { return owner.GetLeft(this); } }

		public void Render(Painter p, Node n)
		{
			a(this, p, n);
		}
	}
}

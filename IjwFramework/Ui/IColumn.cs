using System;
using System.Collections.Generic;
using System.Text;

namespace IjwFramework.Ui
{
	public interface IColumn
	{
		string Name { get; }
		int Width { get; }
		int Left { get; }

		void Render(Painter p, Node n);
	}
}

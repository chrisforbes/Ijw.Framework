using System;
using System.Collections.Generic;
using System.Text;

namespace IjwFramework.Ui.Tree
{
	public delegate void RowOperation( int first, int count );

	public class Presenter
	{
		public RootNode root;
		int rows = 0;

		public Presenter(RootNode root)
		{
			if (root == null)
				throw new ArgumentNullException("The tree must not be null");
			this.root = root;

			root.NodeExpanded += OnNodeExpanded;
			root.NodeCollapsed += OnNodeCollapsed;
			root.NodeAdded += OnNodeAdded;
			root.NodeRemoved += OnNodeRemoved;

			rows = root.VisibleSubtreeSize;
		}

		public int VisibleRows { get { return rows; } }

		public Node GetRow(int i)
		{
			if (i < 0 || i >= rows)
				return null;

			foreach (Node n in VisibleNodes)
				if (i-- == 0)
					return n;

			throw new InvalidOperationException("invariant failed");
		}

		public int IndexOf(Node n)
		{
			int i = 0;
			foreach (Node a in VisibleNodes)
				if (a == n)
					return i;
				else
					++i;

			return -1;
		}

		public event RowOperation RowsInserted = delegate { };
		public event RowOperation RowsRemoved = delegate { };

		void OnNodeExpanded(Node n)
		{
			int size = n.VisibleSubtreeSize;

			RowsInserted(IndexOf(n) + 1, size);
			rows += size;
		}

		void OnNodeCollapsed(Node n)
		{
			int size = n.VisibleSubtreeSize;
			rows -= size;
			RowsRemoved(IndexOf(n) + 1, size);
		}

		void OnNodeRemoved(Node n)
		{
			rows -= 1 + n.VisibleSubtreeSize;
		}

		void OnNodeAdded(Node n)
		{
			rows += 1 + n.VisibleSubtreeSize;
		}

		IEnumerable<Node> VisibleNodes
		{
			get
			{
				Stack<Node> stack = new Stack<Node>();

				foreach (Node child in root.ReverseChildren)
					stack.Push(child);

				while (stack.Count > 0)
				{
					Node node = stack.Pop();
					yield return node;

					if (node.Expanded && !node.IsLeaf)
						foreach (Node child in node.ReverseChildren)
							stack.Push(child);
				}
			}
		}
	}
}

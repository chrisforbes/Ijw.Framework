using System;
using System.Collections.Generic;
using System.Text;

namespace IjwFramework.Ui
{
	public class Node
	{
		public Node parent;
		readonly List<Node> children = new List<Node>();

		bool expanded = true;

		public bool Expanded { get { return expanded; } }

		public int Count { get { return children.Count; } }

		public void Expand()
		{
			expanded = true;
			if (IsLeaf)
				return;

			RootNode root = Root as RootNode;
			if (root != null)
				root.NotifyNodeExpanded(this);
		}

		public void ToggleExpanded()
		{
			if (expanded)
				Collapse();
			else
				Expand();
		}

		public void Collapse()
		{
			if (!IsLeaf)
			{
				RootNode root = Root as RootNode;
				if (root != null)
					root.NotifyNodeCollapsed(this);
			}

			expanded = false;
		}
		
		public IEnumerable<Node> Children { get { return children; } }

		public IEnumerable<Node> ReverseChildren
		{
			get
			{
				for (int i = children.Count - 1; i >= 0; i--)
					yield return children[i];
			}
		}

		public Node Root
		{
			get
			{
				Node t = this;
				while (t.parent != null)
					t = t.parent;
				return t;
			}
		}

		public bool IsLeaf { get { return children.Count == 0; } }

		public int Depth
		{
			get
			{
				int i = 0;
				Node t = this;
				while ((t = t.parent) != null)
					++i;

				return i;
			}
		}

		bool IsDescendentOf(Node n)
		{
			Node t = this;
			while (t != null)
				if (t == n)
					return true;
				else
					t = t.parent;

			return false;
		}

		public void Add(Node child)
		{
			if (child == null)
				throw new ArgumentNullException("Cannot add null child");

			if (children.Contains(child)) 
				throw new ArgumentException("The node already contains this child");

			if (child.parent != null)
				throw new InvalidOperationException("The child already has a parent");

			if (this.IsDescendentOf(child))
				throw new InvalidOperationException("Cannot create cycle");

			children.Add(child);
			child.parent = this;

			RootNode root = Root as RootNode;
			if (root != null)
				root.NotifyNodeAdded(child);
		}

		public bool Contains(Node node)
		{
			return children.Contains(node);
		}

		public void Remove(Node node)
		{
			if (node == null) 
				throw new ArgumentNullException("Cannot remove null child");

			if (!children.Remove(node))
				throw new ArgumentException("The node does not contain this child");

			node.parent = null;

			RootNode root = Root as RootNode;
			if (root != null)
				root.NotifyNodeRemoved(node);
		}

		public int VisibleSubtreeSize
		{
			get
			{
				if (!expanded || IsLeaf)
					return 0;

				int count = 0;
				foreach (Node n in children)
					count += 1 + n.VisibleSubtreeSize;

				return count;
			}
		}
	}

	public class Node<T> : Node
	{
		readonly T value;

		public T Value { get { return value; } }

		public Node(T value)
			: base()
		{
			this.value = value;
		}
	}
}

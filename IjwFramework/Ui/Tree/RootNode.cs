using System;
using System.Collections.Generic;
using System.Text;

namespace IjwFramework.Ui.Tree
{
	public class RootNode : Node
	{
		public event Action<Node> NodeExpanded = delegate { };
		public event Action<Node> NodeCollapsed = delegate { };
		public event Action<Node> NodeAdded = delegate { };
		public event Action<Node> NodeRemoved = delegate { };

		internal void NotifyNodeExpanded(Node n) { NodeExpanded(n); }
		internal void NotifyNodeCollapsed(Node n) { NodeCollapsed(n); }
		internal void NotifyNodeAdded(Node n) { NodeAdded(n); }
		internal void NotifyNodeRemoved(Node n) { NodeRemoved(n); }
	}
}

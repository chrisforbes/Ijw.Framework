using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using IjwFramework.Ui;

namespace IjwFramework.Ui.Tree
{
	public class TreeControl : ScrollableView
	{
		public RootNode Root { get { return root; }}
		readonly ItemPainter painter;
		readonly RootNode root = new RootNode();
		Presenter presenter;
		Scroller scroller;
		readonly ColumnCollection columns;
		const int rowHeight = 16;

		int selected = 0;
		int scroll = 0;

		public Node SelectedNode
		{
			get { return presenter.GetRow(selected); }
			set { SelectedIndex = presenter.IndexOf(value); }
		}

		public int SelectedIndex
		{
			get { return selected; }
			set
			{
				if (value == selected)
					return;

				EnsureVisible(value);
				InvalidateRows(selected, value);
				selected = value;
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Invalidate();
		}

		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			Invalidate();
		}

		public TreeControl( ImageProvider imageProvider, ColumnCollection columns )
			: base(false, true)
		{
			presenter = new Presenter(root);
			scroller = new Scroller(this);
			BackColor = SystemColors.Window;

			this.painter = new ItemPainter( imageProvider );
			this.columns = columns;

			presenter.RowsInserted += OnRowsInserted;
			presenter.RowsRemoved += OnRowsRemoved;

			SetStyle(ControlStyles.Selectable, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			
			UpdateStyles();

			VerticalScroll.Scroll += OnScroll;
		}

		int GetFirstItem(Rectangle clipRectangle)
		{
			return clipRectangle.Top / rowHeight;	//round down
		}

		int GetLastItem(Rectangle clipRectangle)
		{
			return (clipRectangle.Bottom + rowHeight - 1) / rowHeight;	//round up
		}

		void PaintRow(Graphics g, Node n, int row)
		{
			painter.g = g;
			Rectangle bounds = GetRowRectangle(row);
			painter.PaintItem(n, bounds, row == selected);

			Painter p = new Painter(g, bounds);
			p.SetPosition(n.Depth * bounds.Height);
			columns.Render(p, n);
		}

		Rectangle GetRowRectangle(int i)
		{
			i -= scroll;
			return new Rectangle(0, i * rowHeight, ClientSize.Width, rowHeight);
		}

		Rectangle GetRectangleStartingAt(int i)
		{
			i -= scroll;
			return new Rectangle(0, i * rowHeight, ClientSize.Width, ClientSize.Height - i * rowHeight);
		}

		void InvalidateRows(params int[] rows)
		{
			foreach (int i in rows)
				Invalidate(GetRowRectangle(i));
		}

		protected override bool IsInputKey(Keys keyData) { return true; }

		protected override void OnPaint(PaintEventArgs e)
		{
			AdjustScrollbar();
			int first = Math.Max(0, GetFirstItem(e.ClipRectangle)) + scroll;
			int last = Math.Min(presenter.VisibleRows, GetLastItem(e.ClipRectangle)) + scroll;

			for (int row = first; row < last; row++)
			{
				Node n = presenter.GetRow(row);
				if (n != null)
					PaintRow(e.Graphics, n, row);
			}

			if (last * rowHeight < e.ClipRectangle.Bottom)
			{
				e.Graphics.FillRectangle(SystemBrushes.Window,
					new Rectangle( 0, (last - scroll) * rowHeight, ClientSize.Width, e.ClipRectangle.Bottom - last * rowHeight));
			}

			base.OnPaint(e);
		}

		//todo: fix render glitch so this works again
		//protected override void OnPaintBackground(PaintEventArgs pevent) {}

		bool ExpanderClicked(Point location, Node n)
		{
			if (location.X < (n.Depth - 1) * rowHeight)
				return false;
			if (location.X > (n.Depth) * rowHeight)
				return false;

			n.ToggleExpanded();
			return true;
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			Select();

			int i = e.Y / rowHeight + scroll;

			Node n = presenter.GetRow(i);
			if (n == null)
				return;

			SelectedIndex = i;

			if (e.Button == MouseButtons.Left)
				ExpanderClicked(e.Location, n);
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);

			Node n = presenter.GetRow(e.Y / rowHeight + scroll);
			if (n == null)
				return;

			if (e.Button == MouseButtons.Left)
				n.ToggleExpanded();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (e.KeyCode == Keys.Up)
				SelectedIndex = SelectedIndex > 0 ? SelectedIndex - 1 : 0;

			if (e.KeyCode == Keys.Down)
			{
				int rows = presenter.VisibleRows;
				if (SelectedIndex < presenter.VisibleRows - 1)
					++SelectedIndex;
			}

			if (e.KeyCode == Keys.Left)
			{
				Node n = SelectedNode;
				if (!n.IsLeaf && n.Expanded)
					n.Collapse();
				else
				{
					int k = presenter.IndexOf(n.parent);
					SelectedIndex = k < 0 ? 0 : k;
				}
			}

			if (e.KeyCode == Keys.Right)
			{
				Node n = SelectedNode;
				if (n.IsLeaf)
					return;

				if (!n.Expanded)
					n.Expand();
				else
					++SelectedIndex;
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == (Keys.Control | Keys.Down))
			{
				if (scroll < VerticalScroll.Maximum - VerticalScroll.PageSize)
					ScrollBy(1);
				return true;
			}

			if (keyData == (Keys.Control | Keys.Up))
			{
				if (scroll > 0)
					ScrollBy(-1);
				return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		void OnRowsInserted(int start, int count)
		{
			InvalidateRows(start - 1);
			Rectangle r = GetRectangleStartingAt(start);
			scroller.ScrollRectangle(r, 0, count * rowHeight);

			AdjustScrollbar();
		}

		void OnRowsRemoved(int start, int count)
		{
			InvalidateRows(start - 1);
			OnRowsInserted(start + count, -count);

			Rectangle k = GetRectangleStartingAt(start);
			Invalidate(k);
		}

		int GetScrollPosition(int row)
		{
			if (row < scroll)
				return row;

			int c = (ClientSize.Height - rowHeight + 1) / rowHeight;
			if (row > scroll + c)
				return row - c;

			return scroll;
		}

		void EnsureVisible(int row)
		{
			int newScroll = GetScrollPosition(row);
			ScrollBy(newScroll - scroll);
		}

		void OnScroll(object sender, ScrollEventArgs e)
		{
			ScrollBy(e.NewValue - e.OldValue);
		}

		void ScrollBy(int dy)
		{
			if (dy == 0)
				return;

			scroll += dy;
			ScrollContent(0, -rowHeight * dy);
			AdjustScrollbar();
		}

		void AdjustScrollbar()
		{
			VerticalScroll.PageSize = ClientSize.Height / rowHeight;
			VerticalScroll.TrackPosition = VerticalScroll.Position = scroll;
			VerticalScroll.Maximum = presenter.VisibleRows;
			VerticalScroll.Min = 0;
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);

			int newScroll = Constrain(scroll - Math.Sign(e.Delta), 0, 
				VerticalScroll.Maximum - VerticalScroll.PageSize);

			ScrollBy(newScroll - scroll);
		}

		static T Constrain<T>(T value, T lower, T upper)
		{
			Comparer<T> c = Comparer<T>.Default;
			if (c == null)
				throw new InvalidOperationException("Cannot compare values of type " + typeof(T));

			if (c.Compare(value, lower) < 0)
				return lower;
			if (c.Compare(value, upper) > 0)
				return upper;
			return value;
		}
	}
}

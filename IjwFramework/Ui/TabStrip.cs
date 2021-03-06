using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using IjwFramework.Ui;

namespace IjwFramework.Ui
{
	public class TabStrip<T> : Control
		where T : class
	{
		readonly List<Tab<T>> tabs = new List<Tab<T>>();
		readonly TabIterator<T> iterator;

		public int Count { get { return tabs.Count; } }

		CloseBox closeBox;

		public event Action Changed = delegate { };
		public event Action CurrentTabChanged = delegate { };

		public TabStrip()
		{
			BackColor = SystemColors.ButtonFace;
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			UpdateStyles();

			closeBox = new CloseBox(this);
			closeBox.Clicked += delegate { CloseCurrent(); };
			iterator = new TabIterator<T>(this);
			iterator.Changed += delegate { CurrentTabChanged(); };

			Changed += delegate { closeBox.Visible = (Current != null); };
		}

		public IEnumerable<T> Items
		{
			get
			{
				foreach (Tab<T> tab in tabs)
					yield return tab.Content;
			}
		}

		Tab<T> GetTab(T item)
		{
			if (item == null)
				throw new ArgumentNullException("document");

			foreach (Tab<T> tab in tabs)
				if (tab.Content == item)
					return tab;

			return null;
		}

		internal Tab<T> GetTab(int index)
		{
			return index < 0 ? 
				null : tabs[index];
		}

		public T Current
		{
			get
			{
				return iterator.Current == null ? 
					null : iterator.Current.Content;
			}
		}

		public void Add(T item)
		{
			Tab<T> tab = GetTab(item);
			if (tab == null)
				tabs.Add(tab = new Tab<T>(item, this));

			Changed();
			Select( item );
		}

		public void Select( T item )
		{
			Tab<T> tab = GetTab( item );
			if( tab != null )
				iterator.Current = tab;
		}

		public void SelectNext() { iterator.MoveNext(); }
		public void SelectPrevious() { iterator.MovePrevious(); }

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Graphics g = e.Graphics;

			ControlPaint.DrawBorder(g,
				ClientRectangle,
				SystemColors.ButtonShadow,
				ButtonBorderStyle.Solid);

			Region oldClip = e.Graphics.Clip;
			e.Graphics.IntersectClip(new Rectangle(0, 0, Width - Height, Height));

			int x = 1;
			foreach (Tab<T> d in tabs)
				d.Paint(g, ref x, iterator.Current == d, ClientRectangle);

			e.Graphics.Clip = oldClip;

			closeBox.Paint(g);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			Tab<T> tab = GetTab(e.Location);
			if (tab == null)
				return;

			switch (e.Button)
			{
				case MouseButtons.Left:
					iterator.Current = tab;
					break;
				case MouseButtons.Middle:
					Close(tab.Content);
					break;

				default:
					break;
			}
		}

		public bool CloseAll()
		{
			tabs.Clear();
			iterator.Current = null;
			Changed();
			return true;
		}

		public void Close(T item)
		{
			if (item != null)
			{
				Tab<T> tab = GetTab(item);
				tabs.Remove(tab);
				Changed();
				Invalidate();
			}
		}

		Tab<T> GetTab(Point p)
		{
			foreach (Tab<T> t in tabs)
				if (t.Bounds.Contains(p))
					return t;

			return null;
		}

		internal int IndexOf(Tab<T> t) { return tabs.IndexOf(t); }
		public void CloseCurrent() { Close(Current); }
	}
}

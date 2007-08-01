using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IjwFramework.Ui
{
	public class MenuBuilder
	{
		MenuStrip menu = new MenuStrip();
		Dictionary<string, Image> images;

		public MenuBuilder(ToolStripContainer container, Dictionary<string, Image> images)
		{
			container.TopToolStripPanel.Controls.Add(menu);
			this.images = images;
		}

		ToolStripMenuItem GetNamedChild(ToolStripItemCollection container, string name)
		{
			foreach (ToolStripMenuItem item in container)
				if (name == item.Text)
					return item as ToolStripMenuItem;

			ToolStripMenuItem newItem = new ToolStripMenuItem(name);
			container.Add(newItem);

			return newItem;
		}

		public ToolStripItem CreateMenu(string path, EventHandler click, string image, Keys shortcutKeys)
		{
			string[] pathFrags = path.Split('/', '\\');
			string[] actualPathFrags = new string[pathFrags.Length - 1];

			if (actualPathFrags.Length > 0)
				Array.Copy(pathFrags, actualPathFrags, pathFrags.Length - 1);

			string name = pathFrags[pathFrags.Length - 1];

			return CreateMenu(image, click, shortcutKeys, name, actualPathFrags);
		}

		ToolStripItem CreateMenu(string image, EventHandler click, Keys shortcutKeys, string name, string[] textPath)
		{
			ToolStripMenuItem temp = null;
			ToolStripItemCollection c = menu.Items;

			foreach (string node in textPath)
				if (null != (temp = GetNamedChild(c, node)))
					c = temp.DropDownItems;

			Image itemImage = string.IsNullOrEmpty(image) ? null : images[image];

			ToolStripItem item;

			if (name == "-")
				item = new ToolStripSeparator();
			else
				item = new ToolStripMenuItem(name, itemImage, click, shortcutKeys);

			c.Add(item);
			return item;
		}
	}
}

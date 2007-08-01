using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IjwFramework.Ui
{
	public class ToolbarBuilder
	{
		ToolStrip toolstrip = new ToolStrip();
		Dictionary<string, Image> images;

		public ToolbarBuilder(ToolStripContainer container, Dictionary<string, Image> images)
		{
			container.LeftToolStripPanel.Controls.Add(toolstrip);
			this.images = images;
		}

		public ToolStripItem CreateButton(string image, string text, EventHandler click)
		{
			if (string.IsNullOrEmpty(image))
			{
				ToolStripSeparator s = new ToolStripSeparator();
				toolstrip.Items.Add(s);
				return s;
			}

			ToolStripButton button = new ToolStripButton(text,
				string.IsNullOrEmpty(image) ? null : images[image], click);

			button.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolstrip.Items.Add(button);

			return button;
		}
	}
}

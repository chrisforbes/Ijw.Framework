using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using IjwFramework.Types;

namespace IjwFramework.Ui
{
	public class ToolbarBuilder
	{
		Lazy<ToolStrip> toolstrip;
		Dictionary<string, Image> images;

		public ToolbarBuilder(ToolStripContainer container, Dictionary<string, Image> images)
		{
			toolstrip = Lazy.New(() =>
			{
				var ts = new ToolStrip();
				container.LeftToolStripPanel.Controls.Add(ts);
				return ts;
			});

			this.images = images;
		}

		public ToolStripItem CreateButton(string image, string text, EventHandler click)
		{
			if (string.IsNullOrEmpty(image))
			{
				ToolStripSeparator s = new ToolStripSeparator();
				toolstrip.Value.Items.Add(s);
				return s;
			}

			ToolStripButton button = new ToolStripButton(text,
				string.IsNullOrEmpty(image) ? null : images[image], click);

			button.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolstrip.Value.Items.Add(button);

			return button;
		}
	}
}

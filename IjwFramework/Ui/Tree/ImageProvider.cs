using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IjwFramework.Ui.Tree
{
	public class ImageProvider
	{
		ImageList imageList = new ImageList();
		string srcPath;

		public ImageProvider( string srcPath )
		{
			this.srcPath = srcPath;

			imageList.TransparentColor = Color.Fuchsia;
			imageList.ColorDepth = ColorDepth.Depth32Bit;
		}

		public Image GetImage(string key)
		{
			return imageList.Images[key] ?? LoadImage(key);
		}

		Image LoadImage(string key)
		{
			imageList.Images.Add(key, Image.FromFile(srcPath + key + ".bmp"));
			return imageList.Images[key];
		}
	}
}

/// <summary>
/// This dialog is for simply viewing a stored character image. That is all.
/// Author: Justin Pearce <whitefox@guardianfox.net>
/// </summary>

using System;
using System.Drawing;
using System.Windows.Forms;

namespace CharSheetV2
{
	/// <summary>
	/// Class for defining the methods and UI components for the window used for displaying character photos.
	/// </summary>
	public partial class ImageViewer : Form
	{
		public ImageViewer()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
		}
		
		/// <summary>
		/// Populates the internal picture box control with the provided image object.
		/// </summary>
		/// <param name="characterPicture">Image object to put into the picture box.</param>
		public void SetImage(Image characterPicture) {
			this.viewLargerPictureBox.Image = characterPicture;
		}
	}
}

// Simple about dialog that gice information about the program. 
// Author: Justin Pearce <whitefox@guardianfox.net>

using System;
using System.Windows.Forms;

namespace CharSheetV2
{
	/// <summary>
	/// Class containing methods and UI components for the 'About' dialog.
	/// </summary>
	public partial class AboutDialog : Form
	{
		public AboutDialog()
		{
			InitializeComponent();
		}
		
		/// <summary>
		/// Close the dialog, obviously.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event's arguments.</param>
		public void AboutOkClick(object sender, EventArgs e)
		{
			Close();
		}
	}
}

// This represent the dialog to collect the number of minutes to set Fate's Hand to
// Author: Justin Pearce <whitefox@guardianfox.net>

using System;
using System.Drawing;
using System.Windows.Forms;

namespace CharSheetV2
{
	/// <summary>
	/// Description of FatesHandDialog.
	/// </summary>
	public partial class FatesHandDialog : Form
	{
		public int fatesHandTimerMillis = 900000;
		public FatesHandDialog(bool enabled, int millis)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.fatesHandTimerMillis = millis;
			this.fatesHandMinutes.Value = (millis / 1000) / 60;
		}
		
		public void Button1Click(object sender, EventArgs e)
		{
			fatesHandTimerMillis = (int)((fatesHandMinutes.Value * 60 ) * 1000);
			Close();
		}
		
		public void Button2Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}

/*
 * Created by SharpDevelop.
 * User: Shirogitsune
 * Date: 6/10/2017
 * Time: 9:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
			this.fatesHandMinutes.Value = (millis / 1000) / 60;
			this.fatesHandTimerMillis = millis;
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

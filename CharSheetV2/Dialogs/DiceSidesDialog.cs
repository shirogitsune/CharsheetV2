// This represent the dialog to set the number of dice to default to in the application.
// Author: Justin Pearce <whitefox@guardianfox.net>

using System;
using System.Drawing;
using System.Windows.Forms;

namespace CharSheetV2
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class DiceSidesDialog : Form
	{
		public int numberOfDiceSides;
		public DiceSidesDialog(int diceSides)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.numericUpDown1.Value = diceSides;	
			this.numberOfDiceSides = diceSides;			
		}

		/// <summary>
		/// Returns the desired dice sides.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Button1Click(object sender, EventArgs e)
		{
			numberOfDiceSides = Int32.Parse(numericUpDown1.Value.ToString());
			Close();
		}
		
		/// <summary>
		/// Close the dialog!
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		public void Button2Click(object sender, EventArgs e)
		{
			Close();
		}
		
		/// <summary>
		/// Get the value of the number of dice sides.
		/// </summary>
		/// <returns>Dice sides as an integer</returns>
		public int getDiceSides() 
		{
			return numberOfDiceSides;	
		}
		
	}
}

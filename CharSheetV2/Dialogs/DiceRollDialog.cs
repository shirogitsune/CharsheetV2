// This represent the dialog to roll an arbitrary number of dice
// Author: Justin Pearce <whitefox@guardianfox.net>

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace CharSheetV2
{
	/// <summary>
	/// Description of DiceRollDialog.
	/// </summary>
	public partial class DiceRollDialog : Form
	{
	    public int diceCount {get; set;}
	    public int sideCount {get; set;}
	    /// <summary>
	    /// Constructor
	    /// </summary>
	    /// <param name="dice">Number of dice to roll</param>
	    /// <param name="sides">Number of dice sides</param>
		public DiceRollDialog(int dice, int sides)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			diceCount = dice;
			sideCount = sides;
			
		    diceNumber.Value = diceCount;
		    diceSides.Value = sideCount;
		}
		/// <summary>
		/// Update internal dice count on update
		/// </summary>
		/// <param name="sender">The Sender</param>
		/// <param name="e">The event</param>
        public void DiceNumberValueChanged(object sender, EventArgs e)
        {
            diceCount = (int)diceNumber.Value;
        }
        /// <summary>
        /// Update internal dice side on update
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The event</param>
        public void DiceSidesValueChanged(object sender, EventArgs e)
        {
            sideCount = (int)diceSides.Value;
        }
        /// <summary>
        /// Complete to data collection and return to the main form.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The event</param>
        public void RollButtonClick(object sender, EventArgs e)
        {
            diceCount = (int)diceNumber.Value;
            sideCount = (int)diceSides.Value;
            this.DialogResult = DialogResult.OK;            
            Close();
        }
	}
}

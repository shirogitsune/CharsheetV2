/*
 * Created by SharpDevelop.
 * User: Shirogitsune
 * Date: 1/17/2019
 * Time: 9:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace CharSheetV2
{
	partial class DiceRollDialog
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.NumericUpDown diceNumber;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown diceSides;
		private System.Windows.Forms.Button rollButton;
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(DiceRollDialog));
			this.diceNumber = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.diceSides = new System.Windows.Forms.NumericUpDown();
			this.rollButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.diceNumber)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.diceSides)).BeginInit();
			this.SuspendLayout();
			// 
			// diceNumber
			// 
			this.diceNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.diceNumber.Location = new System.Drawing.Point(73, 22);
			this.diceNumber.Name = "diceNumber";
			this.diceNumber.Size = new System.Drawing.Size(54, 26);
			this.diceNumber.TabIndex = 0;
			this.diceNumber.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.diceNumber.ValueChanged += new System.EventHandler(this.DiceNumberValueChanged);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(27, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Roll";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(158, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(20, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "D";
			// 
			// diceSides
			// 
			this.diceSides.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.diceSides.Location = new System.Drawing.Point(178, 22);
			this.diceSides.Name = "diceSides";
			this.diceSides.Size = new System.Drawing.Size(51, 26);
			this.diceSides.TabIndex = 3;
			this.diceSides.ValueChanged += new System.EventHandler(this.DiceSidesValueChanged);
			// 
			// rollButton
			// 
			this.rollButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rollButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rollButton.Location = new System.Drawing.Point(61, 70);
			this.rollButton.Name = "rollButton";
			this.rollButton.Size = new System.Drawing.Size(143, 31);
			this.rollButton.TabIndex = 4;
			this.rollButton.Text = "Roll!";
			this.rollButton.UseVisualStyleBackColor = true;
			this.rollButton.Click += new System.EventHandler(this.RollButtonClick);
			// 
			// DiceRollDialog
			// 
			this.AcceptButton = this.rollButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(265, 123);
			this.Controls.Add(this.rollButton);
			this.Controls.Add(this.diceSides);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.diceNumber);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DiceRollDialog";
			this.Text = "Roll some dice...";
			((System.ComponentModel.ISupportInitialize)(this.diceNumber)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.diceSides)).EndInit();
			this.ResumeLayout(false);

		}
	}
}

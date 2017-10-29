/*
 * Created by SharpDevelop.
 * User: Shirogitsune
 * Date: 6/10/2017
 * Time: 9:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace CharSheetV2
{
	partial class FatesHandDialog
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FatesHandDialog));
			this.fatesHandMinutes = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.fatesHandMinutes)).BeginInit();
			this.SuspendLayout();
			// 
			// fatesHandMinutes
			// 
			this.fatesHandMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.fatesHandMinutes.Location = new System.Drawing.Point(182, 49);
			this.fatesHandMinutes.Maximum = new decimal(new int[] {
									1440,
									0,
									0,
									0});
			this.fatesHandMinutes.Name = "fatesHandMinutes";
			this.fatesHandMinutes.Size = new System.Drawing.Size(82, 26);
			this.fatesHandMinutes.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(164, 47);
			this.label1.TabIndex = 2;
			this.label1.Text = "Minutes Until Fate\'s Hand Chooses";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(46, 103);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 3;
			this.saveButton.Text = "Save";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.Button1Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(165, 103);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.Button2Click);
			// 
			// FatesHandDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(286, 155);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.fatesHandMinutes);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FatesHandDialog";
			this.Text = "Fate\'s Hand";
			((System.ComponentModel.ISupportInitialize)(this.fatesHandMinutes)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown fatesHandMinutes;
	}
}

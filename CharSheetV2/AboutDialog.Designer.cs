/*
 * Created by SharpDevelop.
 * User: Shirogitsune
 * Date: 4/21/2013
 * Time: 8:11 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace CharSheetV2
{
	partial class AboutDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.aboutOk = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(26, 28);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(60, 66);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(115, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(179, 114);
			this.label1.TabIndex = 1;
			this.label1.Text = "Written By: \r\nJustin Pearce (aka Shirogitsune)\r\nwhitefox@guardianfox.net\r\nhttp://" +
			"www.guardianfox.net\r\n\r\nWritten as a replacement to CharSheet by TerrinFox terrin_fox@hotmail.com !";
			// 
			// aboutOk
			// 
			this.aboutOk.Location = new System.Drawing.Point(116, 126);
			this.aboutOk.Name = "aboutOk";
			this.aboutOk.Size = new System.Drawing.Size(75, 23);
			this.aboutOk.TabIndex = 2;
			this.aboutOk.Text = "OK";
			this.aboutOk.UseVisualStyleBackColor = true;
			this.aboutOk.Click += new System.EventHandler(this.AboutOkClick);
			// 
			// AboutDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(306, 157);
			this.Controls.Add(this.aboutOk);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AboutDialog";
			this.Text = "About CharSheet V2";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button aboutOk;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}

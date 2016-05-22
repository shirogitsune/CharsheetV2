/*
 * Created by SharpDevelop.
 * User: Shirogitsune
 * Date: 10/4/2015
 * Time: 11:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace CharSheetV2
{
	partial class ImageViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageViewer));
			this.imageViewPanel = new System.Windows.Forms.Panel();
			this.viewLargerPictureBox = new System.Windows.Forms.PictureBox();
			this.imageViewPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.viewLargerPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// imageViewPanel
			// 
			this.imageViewPanel.AutoScroll = true;
			this.imageViewPanel.AutoSize = true;
			this.imageViewPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.imageViewPanel.Controls.Add(this.viewLargerPictureBox);
			this.imageViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageViewPanel.Location = new System.Drawing.Point(0, 0);
			this.imageViewPanel.Name = "imageViewPanel";
			this.imageViewPanel.Size = new System.Drawing.Size(284, 262);
			this.imageViewPanel.TabIndex = 1;
			// 
			// viewLargerPictureBox
			// 
			this.viewLargerPictureBox.Location = new System.Drawing.Point(0, 0);
			this.viewLargerPictureBox.Name = "viewLargerPictureBox";
			this.viewLargerPictureBox.Size = new System.Drawing.Size(284, 262);
			this.viewLargerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.viewLargerPictureBox.TabIndex = 0;
			this.viewLargerPictureBox.TabStop = false;
			// 
			// ImageViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.imageViewPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ImageViewer";
			this.Text = "ImageViewer";
			this.imageViewPanel.ResumeLayout(false);
			this.imageViewPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.viewLargerPictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Panel imageViewPanel;
		private System.Windows.Forms.PictureBox viewLargerPictureBox;
	}
}

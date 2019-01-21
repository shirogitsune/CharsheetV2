/*
 * Created by SharpDevelop.
 * User: Justin Pearce <whitefox@guardianfox.net>
 * Date: 1/5/2013
 * Time: 7:09 PM
 * 
 */

namespace CharSheetV2
{
	/// <summary>
	/// Class designed to contain the code and definitions for the UI component items displayed by the application.
	/// </summary>
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ClearCheckedButton = new System.Windows.Forms.Button();
			this.charList = new System.Windows.Forms.CheckedListBox();
			this.skillDiceButton = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.skillPoints = new System.Windows.Forms.Label();
			this.skillDataGridView = new System.Windows.Forms.DataGridView();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.charAdvantagesDisadvantages = new System.Windows.Forms.RichTextBox();
			this.charNPC = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.charWeight = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.charHeight = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.charRank = new System.Windows.Forms.TextBox();
			this.experiencePoints = new System.Windows.Forms.NumericUpDown();
			this.label9 = new System.Windows.Forms.Label();
			this.karmaPoints = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.attrPoints = new System.Windows.Forms.Label();
			this.attributesDataGridView = new System.Windows.Forms.DataGridView();
			this.charGender = new System.Windows.Forms.ComboBox();
			this.charAge = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.charAffiliation = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.charSpecies = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.charCallsign = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.charName = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox10 = new System.Windows.Forms.GroupBox();
			this.charInventory = new System.Windows.Forms.RichTextBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.ClearPicture = new System.Windows.Forms.Button();
			this.charPictureEnlarge = new System.Windows.Forms.Button();
			this.pictureFramePanel = new System.Windows.Forms.Panel();
			this.charPictureBox = new System.Windows.Forms.PictureBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.charBackground = new System.Windows.Forms.RichTextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.charNotes = new System.Windows.Forms.RichTextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveChangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.importDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.characterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportCharsheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.asDatabseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.asCharsheetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gMModesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fateTimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.configureFatesHandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setDefaultDiceSidesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.notificationLabel = new System.Windows.Forms.Label();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this.rollBtn = new System.Windows.Forms.Button();
			this.diceCount = new System.Windows.Forms.NumericUpDown();
			this.d20Modifier = new System.Windows.Forms.ComboBox();
			this.d20Button = new System.Windows.Forms.Button();
			this.fatesHand = new System.Windows.Forms.Timer(this.components);
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.skillDataGridView)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.experiencePoints)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.karmaPoints)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.attributesDataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.charAge)).BeginInit();
			this.tabPage2.SuspendLayout();
			this.groupBox10.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.pictureFramePanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.charPictureBox)).BeginInit();
			this.groupBox4.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.groupBox9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.diceCount)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ClearCheckedButton);
			this.groupBox1.Controls.Add(this.charList);
			this.groupBox1.Location = new System.Drawing.Point(12, 31);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 354);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Characters";
			// 
			// ClearCheckedButton
			// 
			this.ClearCheckedButton.Location = new System.Drawing.Point(8, 315);
			this.ClearCheckedButton.Name = "ClearCheckedButton";
			this.ClearCheckedButton.Size = new System.Drawing.Size(185, 23);
			this.ClearCheckedButton.TabIndex = 1;
			this.ClearCheckedButton.Text = "Clear Checked";
			this.ClearCheckedButton.UseVisualStyleBackColor = true;
			this.ClearCheckedButton.Click += new System.EventHandler(this.ClearCheckedButtonClick);
			// 
			// charList
			// 
			this.charList.FormattingEnabled = true;
			this.charList.Location = new System.Drawing.Point(7, 20);
			this.charList.Name = "charList";
			this.charList.Size = new System.Drawing.Size(187, 289);
			this.charList.TabIndex = 0;
			this.charList.SelectedIndexChanged += new System.EventHandler(this.CharListSelectedIndexChanged);
			// 
			// skillDiceButton
			// 
			this.skillDiceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.skillDiceButton.Location = new System.Drawing.Point(7, 47);
			this.skillDiceButton.Name = "skillDiceButton";
			this.skillDiceButton.Size = new System.Drawing.Size(111, 23);
			this.skillDiceButton.TabIndex = 0;
			this.skillDiceButton.Text = "Roll Skill/Attribute";
			this.skillDiceButton.UseVisualStyleBackColor = true;
			this.skillDiceButton.Click += new System.EventHandler(this.SkillDiceButtonClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.skillPoints);
			this.groupBox2.Controls.Add(this.skillDataGridView);
			this.groupBox2.Location = new System.Drawing.Point(300, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(345, 274);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Skills";
			// 
			// skillPoints
			// 
			this.skillPoints.Location = new System.Drawing.Point(7, 244);
			this.skillPoints.Name = "skillPoints";
			this.skillPoints.Size = new System.Drawing.Size(201, 16);
			this.skillPoints.TabIndex = 2;
			this.skillPoints.Text = "Total Skill Points: ";
			// 
			// skillDataGridView
			// 
			this.skillDataGridView.AllowUserToResizeColumns = false;
			this.skillDataGridView.AllowUserToResizeRows = false;
			this.skillDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.skillDataGridView.Location = new System.Drawing.Point(7, 19);
			this.skillDataGridView.MultiSelect = false;
			this.skillDataGridView.Name = "skillDataGridView";
			this.skillDataGridView.Size = new System.Drawing.Size(334, 222);
			this.skillDataGridView.TabIndex = 0;
			this.skillDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.SkillDataGridViewCellEndEdit);
			this.skillDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.SkillDataGridViewRowEnter);
			this.skillDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.SkillDataGridViewUserDeletedRow);
			this.skillDataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.SkillDataGridViewUserDeletingRow);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(219, 31);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(656, 545);
			this.tabControl1.TabIndex = 3;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox6);
			this.tabPage1.Controls.Add(this.charNPC);
			this.tabPage1.Controls.Add(this.label11);
			this.tabPage1.Controls.Add(this.charWeight);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.charHeight);
			this.tabPage1.Controls.Add(this.label10);
			this.tabPage1.Controls.Add(this.charRank);
			this.tabPage1.Controls.Add(this.experiencePoints);
			this.tabPage1.Controls.Add(this.label9);
			this.tabPage1.Controls.Add(this.karmaPoints);
			this.tabPage1.Controls.Add(this.label8);
			this.tabPage1.Controls.Add(this.groupBox3);
			this.tabPage1.Controls.Add(this.charGender);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.charAge);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.charAffiliation);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.charSpecies);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.charCallsign);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.charName);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(648, 519);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "General Information";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.charAdvantagesDisadvantages);
			this.groupBox6.Location = new System.Drawing.Point(7, 256);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(287, 254);
			this.groupBox6.TabIndex = 23;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Advantages / Disadvantages";
			// 
			// charAdvantagesDisadvantages
			// 
			this.charAdvantagesDisadvantages.AcceptsTab = true;
			this.charAdvantagesDisadvantages.EnableAutoDragDrop = true;
			this.charAdvantagesDisadvantages.Location = new System.Drawing.Point(6, 24);
			this.charAdvantagesDisadvantages.Name = "charAdvantagesDisadvantages";
			this.charAdvantagesDisadvantages.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.charAdvantagesDisadvantages.ShowSelectionMargin = true;
			this.charAdvantagesDisadvantages.Size = new System.Drawing.Size(275, 224);
			this.charAdvantagesDisadvantages.TabIndex = 0;
			this.charAdvantagesDisadvantages.Text = "";
			this.charAdvantagesDisadvantages.TextChanged += new System.EventHandler(this.CharAdvantagesDisadvantagesTextChanged);
			// 
			// charNPC
			// 
			this.charNPC.Location = new System.Drawing.Point(63, 220);
			this.charNPC.Name = "charNPC";
			this.charNPC.Size = new System.Drawing.Size(168, 24);
			this.charNPC.TabIndex = 12;
			this.charNPC.Text = "This character is an NPC";
			this.charNPC.UseVisualStyleBackColor = true;
			this.charNPC.CheckedChanged += new System.EventHandler(this.CharNPCCheckedChanged);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(127, 107);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(48, 22);
			this.label11.TabIndex = 22;
			this.label11.Text = "Weight";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// charWeight
			// 
			this.charWeight.Location = new System.Drawing.Point(181, 109);
			this.charWeight.Name = "charWeight";
			this.charWeight.Size = new System.Drawing.Size(58, 20);
			this.charWeight.TabIndex = 6;
			this.charWeight.TextChanged += new System.EventHandler(this.CharWeightTextChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(7, 107);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(67, 22);
			this.label7.TabIndex = 20;
			this.label7.Text = "Height";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// charHeight
			// 
			this.charHeight.Location = new System.Drawing.Point(80, 109);
			this.charHeight.Name = "charHeight";
			this.charHeight.Size = new System.Drawing.Size(45, 20);
			this.charHeight.TabIndex = 5;
			this.charHeight.TextChanged += new System.EventHandler(this.CharHeightTextChanged);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(7, 159);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(67, 22);
			this.label10.TabIndex = 18;
			this.label10.Text = "Rank";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// charRank
			// 
			this.charRank.Location = new System.Drawing.Point(80, 161);
			this.charRank.Name = "charRank";
			this.charRank.Size = new System.Drawing.Size(122, 20);
			this.charRank.TabIndex = 8;
			this.charRank.TextChanged += new System.EventHandler(this.CharRankTextChanged);
			// 
			// experiencePoints
			// 
			this.experiencePoints.Location = new System.Drawing.Point(181, 187);
			this.experiencePoints.Maximum = new decimal(new int[] {
			2000000,
			0,
			0,
			0});
			this.experiencePoints.Name = "experiencePoints";
			this.experiencePoints.Size = new System.Drawing.Size(52, 20);
			this.experiencePoints.TabIndex = 10;
			this.experiencePoints.ValueChanged += new System.EventHandler(this.ExperiencePointsValueChanged);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(127, 184);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(48, 23);
			this.label9.TabIndex = 15;
			this.label9.Text = "Exp. Pts";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// karmaPoints
			// 
			this.karmaPoints.Location = new System.Drawing.Point(63, 187);
			this.karmaPoints.Maximum = new decimal(new int[] {
			2000000,
			0,
			0,
			0});
			this.karmaPoints.Minimum = new decimal(new int[] {
			2000000,
			0,
			0,
			0});
			this.karmaPoints.Name = "karmaPoints";
			this.karmaPoints.Size = new System.Drawing.Size(46, 20);
			this.karmaPoints.TabIndex = 9;
			this.karmaPoints.Value = new decimal(new int[] {
			2000000,
			0,
			0,
			0});
			this.karmaPoints.ValueChanged += new System.EventHandler(this.KarmaPointsValueChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(7, 184);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(50, 23);
			this.label8.TabIndex = 13;
			this.label8.Text = "Karma";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.attrPoints);
			this.groupBox3.Controls.Add(this.attributesDataGridView);
			this.groupBox3.Location = new System.Drawing.Point(300, 283);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(340, 230);
			this.groupBox3.TabIndex = 12;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Attributes";
			// 
			// attrPoints
			// 
			this.attrPoints.Location = new System.Drawing.Point(6, 211);
			this.attrPoints.Name = "attrPoints";
			this.attrPoints.Size = new System.Drawing.Size(219, 16);
			this.attrPoints.TabIndex = 1;
			this.attrPoints.Text = "Total Attribute Points: ";
			// 
			// attributesDataGridView
			// 
			this.attributesDataGridView.AllowUserToResizeColumns = false;
			this.attributesDataGridView.AllowUserToResizeRows = false;
			this.attributesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.attributesDataGridView.Location = new System.Drawing.Point(7, 20);
			this.attributesDataGridView.MultiSelect = false;
			this.attributesDataGridView.Name = "attributesDataGridView";
			this.attributesDataGridView.Size = new System.Drawing.Size(327, 188);
			this.attributesDataGridView.TabIndex = 0;
			this.attributesDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.AttributesDataGridViewCellEndEdit);
			this.attributesDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.AttributesDataGridViewRowEnter);
			this.attributesDataGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.AttributesDataGridViewUserDeletedRow);
			this.attributesDataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.AttributesDataGridViewUserDeletingRow);
			// 
			// charGender
			// 
			this.charGender.FormattingEnabled = true;
			this.charGender.Items.AddRange(new object[] {
			"Male",
			"Female",
			"Herm",
			"Trans",
			"Other"});
			this.charGender.Location = new System.Drawing.Point(80, 82);
			this.charGender.Name = "charGender";
			this.charGender.Size = new System.Drawing.Size(101, 21);
			this.charGender.TabIndex = 3;
			this.charGender.SelectionChangeCommitted += new System.EventHandler(this.CharGenderSelectionChangeCommitted);
			// 
			// charAge
			// 
			this.charAge.Location = new System.Drawing.Point(230, 81);
			this.charAge.Name = "charAge";
			this.charAge.Size = new System.Drawing.Size(58, 20);
			this.charAge.TabIndex = 4;
			this.charAge.Value = new decimal(new int[] {
			18,
			0,
			0,
			0});
			this.charAge.ValueChanged += new System.EventHandler(this.CharAgeValueChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(7, 133);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(67, 23);
			this.label6.TabIndex = 5;
			this.label6.Text = "Affiliation";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(187, 78);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(37, 23);
			this.label5.TabIndex = 9;
			this.label5.Text = "Age";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// charAffiliation
			// 
			this.charAffiliation.Location = new System.Drawing.Point(80, 135);
			this.charAffiliation.Name = "charAffiliation";
			this.charAffiliation.Size = new System.Drawing.Size(159, 20);
			this.charAffiliation.TabIndex = 7;
			this.charAffiliation.TextChanged += new System.EventHandler(this.CharAffiliationTextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 81);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(67, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Gender";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 22);
			this.label3.TabIndex = 5;
			this.label3.Text = "Species";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// charSpecies
			// 
			this.charSpecies.Location = new System.Drawing.Point(79, 57);
			this.charSpecies.Name = "charSpecies";
			this.charSpecies.Size = new System.Drawing.Size(209, 20);
			this.charSpecies.TabIndex = 2;
			this.charSpecies.TextChanged += new System.EventHandler(this.CharSpeciesTextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Callsign";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// charCallsign
			// 
			this.charCallsign.Location = new System.Drawing.Point(79, 31);
			this.charCallsign.Name = "charCallsign";
			this.charCallsign.Size = new System.Drawing.Size(209, 20);
			this.charCallsign.TabIndex = 1;
			this.charCallsign.TextChanged += new System.EventHandler(this.CharCallsignTextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(35, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// charName
			// 
			this.charName.Location = new System.Drawing.Point(79, 5);
			this.charName.Name = "charName";
			this.charName.Size = new System.Drawing.Size(209, 20);
			this.charName.TabIndex = 0;
			this.charName.TextChanged += new System.EventHandler(this.CharNameTextChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox10);
			this.tabPage2.Controls.Add(this.groupBox5);
			this.tabPage2.Controls.Add(this.groupBox4);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(648, 519);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Background / Inventory";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// groupBox10
			// 
			this.groupBox10.Controls.Add(this.charInventory);
			this.groupBox10.Location = new System.Drawing.Point(6, 313);
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.Size = new System.Drawing.Size(468, 200);
			this.groupBox10.TabIndex = 11;
			this.groupBox10.TabStop = false;
			this.groupBox10.Text = "Inventory";
			// 
			// charInventory
			// 
			this.charInventory.AcceptsTab = true;
			this.charInventory.EnableAutoDragDrop = true;
			this.charInventory.Location = new System.Drawing.Point(7, 20);
			this.charInventory.Name = "charInventory";
			this.charInventory.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.charInventory.ShowSelectionMargin = true;
			this.charInventory.Size = new System.Drawing.Size(455, 172);
			this.charInventory.TabIndex = 0;
			this.charInventory.Text = "";
			this.charInventory.TextChanged += new System.EventHandler(this.CharInventoryTextChanged);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.ClearPicture);
			this.groupBox5.Controls.Add(this.charPictureEnlarge);
			this.groupBox5.Controls.Add(this.pictureFramePanel);
			this.groupBox5.Location = new System.Drawing.Point(480, 6);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(160, 224);
			this.groupBox5.TabIndex = 6;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Picture";
			// 
			// ClearPicture
			// 
			this.ClearPicture.Location = new System.Drawing.Point(87, 183);
			this.ClearPicture.Name = "ClearPicture";
			this.ClearPicture.Size = new System.Drawing.Size(67, 23);
			this.ClearPicture.TabIndex = 7;
			this.ClearPicture.Text = "Delete";
			this.ClearPicture.UseVisualStyleBackColor = true;
			this.ClearPicture.Click += new System.EventHandler(this.ClearPictureClick);
			// 
			// charPictureEnlarge
			// 
			this.charPictureEnlarge.Location = new System.Drawing.Point(6, 183);
			this.charPictureEnlarge.Name = "charPictureEnlarge";
			this.charPictureEnlarge.Size = new System.Drawing.Size(75, 23);
			this.charPictureEnlarge.TabIndex = 6;
			this.charPictureEnlarge.Text = "View Larger";
			this.charPictureEnlarge.UseVisualStyleBackColor = true;
			this.charPictureEnlarge.Click += new System.EventHandler(this.CharPictureEnlargeClick);
			// 
			// pictureFramePanel
			// 
			this.pictureFramePanel.AutoScroll = true;
			this.pictureFramePanel.Controls.Add(this.charPictureBox);
			this.pictureFramePanel.Location = new System.Drawing.Point(6, 19);
			this.pictureFramePanel.Name = "pictureFramePanel";
			this.pictureFramePanel.Size = new System.Drawing.Size(148, 148);
			this.pictureFramePanel.TabIndex = 5;
			this.pictureFramePanel.DoubleClick += new System.EventHandler(this.PictureFramePanelDoubleClick);
			// 
			// charPictureBox
			// 
			this.charPictureBox.AccessibleDescription = "Double click to add or update.";
			this.charPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.charPictureBox.Location = new System.Drawing.Point(0, 0);
			this.charPictureBox.Name = "charPictureBox";
			this.charPictureBox.Size = new System.Drawing.Size(148, 148);
			this.charPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.charPictureBox.TabIndex = 0;
			this.charPictureBox.TabStop = false;
			this.charPictureBox.DoubleClick += new System.EventHandler(this.PictureFramePanelDoubleClick);
			this.charPictureBox.MouseHover += new System.EventHandler(this.CharPictureBoxMouseHover);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.charBackground);
			this.groupBox4.Location = new System.Drawing.Point(6, 6);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(468, 301);
			this.groupBox4.TabIndex = 0;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Background Information / Description";
			// 
			// charBackground
			// 
			this.charBackground.AcceptsTab = true;
			this.charBackground.EnableAutoDragDrop = true;
			this.charBackground.Location = new System.Drawing.Point(7, 19);
			this.charBackground.Name = "charBackground";
			this.charBackground.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.charBackground.ShowSelectionMargin = true;
			this.charBackground.Size = new System.Drawing.Size(455, 276);
			this.charBackground.TabIndex = 0;
			this.charBackground.Text = "";
			this.charBackground.TextChanged += new System.EventHandler(this.CharBackgroundTextChanged);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.groupBox7);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(648, 519);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Notes";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.charNotes);
			this.groupBox7.Location = new System.Drawing.Point(6, 6);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(452, 296);
			this.groupBox7.TabIndex = 9;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Notes";
			// 
			// charNotes
			// 
			this.charNotes.AcceptsTab = true;
			this.charNotes.EnableAutoDragDrop = true;
			this.charNotes.Location = new System.Drawing.Point(7, 20);
			this.charNotes.Name = "charNotes";
			this.charNotes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.charNotes.ShowSelectionMargin = true;
			this.charNotes.Size = new System.Drawing.Size(439, 270);
			this.charNotes.TabIndex = 0;
			this.charNotes.Text = "";
			this.charNotes.TextChanged += new System.EventHandler(this.CharNotesTextChanged);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.fileToolStripMenuItem,
			this.characterToolStripMenuItem,
			this.gMModesToolStripMenuItem,
			this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(875, 24);
			this.menuStrip1.TabIndex = 4;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.saveChangesToolStripMenuItem,
			this.importToolStripMenuItem1,
			this.importDatabaseToolStripMenuItem,
			this.quitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// saveChangesToolStripMenuItem
			// 
			this.saveChangesToolStripMenuItem.Name = "saveChangesToolStripMenuItem";
			this.saveChangesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveChangesToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			this.saveChangesToolStripMenuItem.Text = "Save Changes";
			this.saveChangesToolStripMenuItem.Click += new System.EventHandler(this.SaveChangesToDatabase);
			// 
			// importToolStripMenuItem1
			// 
			this.importToolStripMenuItem1.Name = "importToolStripMenuItem1";
			this.importToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
			this.importToolStripMenuItem1.Size = new System.Drawing.Size(239, 22);
			this.importToolStripMenuItem1.Text = "Import Data File...";
			this.importToolStripMenuItem1.Click += new System.EventHandler(this.ImportCharSheetFile);
			// 
			// importDatabaseToolStripMenuItem
			// 
			this.importDatabaseToolStripMenuItem.Name = "importDatabaseToolStripMenuItem";
			this.importDatabaseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.I)));
			this.importDatabaseToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			this.importDatabaseToolStripMenuItem.Text = "Import Database...";
			this.importDatabaseToolStripMenuItem.Click += new System.EventHandler(this.ImportDatabaseClick);
			// 
			// quitToolStripMenuItem
			// 
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			this.quitToolStripMenuItem.Text = "Quit";
			this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitProgram);
			// 
			// characterToolStripMenuItem
			// 
			this.characterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.newToolStripMenuItem,
			this.deleteSelectedToolStripMenuItem,
			this.exportCharsheetToolStripMenuItem});
			this.characterToolStripMenuItem.Name = "characterToolStripMenuItem";
			this.characterToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
			this.characterToolStripMenuItem.Text = "Character";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.newToolStripMenuItem.Text = "New...";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.CreateNewCharacter);
			// 
			// deleteSelectedToolStripMenuItem
			// 
			this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
			this.deleteSelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
			| System.Windows.Forms.Keys.D)));
			this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.deleteSelectedToolStripMenuItem.Text = "Delete Selected...";
			this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectedCharacter);
			// 
			// exportCharsheetToolStripMenuItem
			// 
			this.exportCharsheetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.asDatabseToolStripMenuItem,
			this.asCharsheetsToolStripMenuItem});
			this.exportCharsheetToolStripMenuItem.Name = "exportCharsheetToolStripMenuItem";
			this.exportCharsheetToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.exportCharsheetToolStripMenuItem.Text = "Export Selected";
			// 
			// asDatabseToolStripMenuItem
			// 
			this.asDatabseToolStripMenuItem.Name = "asDatabseToolStripMenuItem";
			this.asDatabseToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.asDatabseToolStripMenuItem.Text = "As Database";
			this.asDatabseToolStripMenuItem.Click += new System.EventHandler(this.ExportAsDatabaseClick);
			// 
			// asCharsheetsToolStripMenuItem
			// 
			this.asCharsheetsToolStripMenuItem.Name = "asCharsheetsToolStripMenuItem";
			this.asCharsheetsToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.asCharsheetsToolStripMenuItem.Text = "As Charsheets";
			this.asCharsheetsToolStripMenuItem.Click += new System.EventHandler(this.ExportAsCharsheetsClick);
			// 
			// gMModesToolStripMenuItem
			// 
			this.gMModesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.fateTimerToolStripMenuItem,
			this.configureFatesHandToolStripMenuItem,
			this.setDefaultDiceSidesToolStripMenuItem});
			this.gMModesToolStripMenuItem.Name = "gMModesToolStripMenuItem";
			this.gMModesToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
			this.gMModesToolStripMenuItem.Text = "GM Tools";
			// 
			// fateTimerToolStripMenuItem
			// 
			this.fateTimerToolStripMenuItem.CheckOnClick = true;
			this.fateTimerToolStripMenuItem.Name = "fateTimerToolStripMenuItem";
			this.fateTimerToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.fateTimerToolStripMenuItem.Text = "Fate\'s Hand";
			this.fateTimerToolStripMenuItem.ToolTipText = "Selects a character at random on an interval and notifies you of the selection";
			this.fateTimerToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.FatesHandCheckedStateChanged);
			// 
			// configureFatesHandToolStripMenuItem
			// 
			this.configureFatesHandToolStripMenuItem.Name = "configureFatesHandToolStripMenuItem";
			this.configureFatesHandToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.configureFatesHandToolStripMenuItem.Text = "Configure Fate\'s Hand";
			this.configureFatesHandToolStripMenuItem.Click += new System.EventHandler(this.ConfigureFatesHandToolStripMenuItemClick);
			// 
			// setDefaultDiceSidesToolStripMenuItem
			// 
			this.setDefaultDiceSidesToolStripMenuItem.Name = "setDefaultDiceSidesToolStripMenuItem";
			this.setDefaultDiceSidesToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.setDefaultDiceSidesToolStripMenuItem.Text = "Default Dice Sides...";
			this.setDefaultDiceSidesToolStripMenuItem.Click += new System.EventHandler(this.SetDefaultDiceSidesToolStripMenuItemClick);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.aboutToolStripMenuItem.Text = "About...";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutMenuItemClicked);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.statusBarLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 579);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(875, 22);
			this.statusStrip1.TabIndex = 6;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// statusBarLabel
			// 
			this.statusBarLabel.Name = "statusBarLabel";
			this.statusBarLabel.Size = new System.Drawing.Size(64, 17);
			this.statusBarLabel.Text = "Char Sheet";
			// 
			// groupBox8
			// 
			this.groupBox8.Controls.Add(this.notificationLabel);
			this.groupBox8.Location = new System.Drawing.Point(12, 474);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(200, 102);
			this.groupBox8.TabIndex = 7;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Notification";
			// 
			// notificationLabel
			// 
			this.notificationLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.notificationLabel.ForeColor = System.Drawing.Color.Red;
			this.notificationLabel.Location = new System.Drawing.Point(8, 16);
			this.notificationLabel.Name = "notificationLabel";
			this.notificationLabel.Padding = new System.Windows.Forms.Padding(5);
			this.notificationLabel.Size = new System.Drawing.Size(186, 81);
			this.notificationLabel.TabIndex = 0;
			this.notificationLabel.Text = "Initializing...";
			// 
			// groupBox9
			// 
			this.groupBox9.Controls.Add(this.rollBtn);
			this.groupBox9.Controls.Add(this.diceCount);
			this.groupBox9.Controls.Add(this.d20Modifier);
			this.groupBox9.Controls.Add(this.skillDiceButton);
			this.groupBox9.Controls.Add(this.d20Button);
			this.groupBox9.Location = new System.Drawing.Point(12, 391);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(200, 77);
			this.groupBox9.TabIndex = 8;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Dice";
			// 
			// rollBtn
			// 
			this.rollBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rollBtn.Location = new System.Drawing.Point(125, 47);
			this.rollBtn.Name = "rollBtn";
			this.rollBtn.Size = new System.Drawing.Size(69, 23);
			this.rollBtn.TabIndex = 4;
			this.rollBtn.Text = "Roll...";
			this.rollBtn.UseVisualStyleBackColor = true;
			this.rollBtn.Click += new System.EventHandler(this.RollBtnClick);
			// 
			// diceCount
			// 
			this.diceCount.Location = new System.Drawing.Point(8, 18);
			this.diceCount.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.diceCount.Name = "diceCount";
			this.diceCount.Size = new System.Drawing.Size(42, 20);
			this.diceCount.TabIndex = 3;
			this.diceCount.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// d20Modifier
			// 
			this.d20Modifier.AllowDrop = true;
			this.d20Modifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.d20Modifier.FormattingEnabled = true;
			this.d20Modifier.Items.AddRange(new object[] {
			"-5",
			"-4",
			"-3",
			"-2",
			"-1",
			"0",
			"1",
			"2",
			"3",
			"4",
			"5"});
			this.d20Modifier.Location = new System.Drawing.Point(144, 18);
			this.d20Modifier.Name = "d20Modifier";
			this.d20Modifier.Size = new System.Drawing.Size(49, 21);
			this.d20Modifier.TabIndex = 2;
			this.d20Modifier.Text = "Mod";
			// 
			// d20Button
			// 
			this.d20Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.d20Button.Location = new System.Drawing.Point(56, 18);
			this.d20Button.Name = "d20Button";
			this.d20Button.Size = new System.Drawing.Size(82, 23);
			this.d20Button.TabIndex = 1;
			this.d20Button.Text = "Roll D20";
			this.d20Button.UseVisualStyleBackColor = true;
			this.d20Button.Click += new System.EventHandler(this.DiceButtonClick);
			// 
			// fatesHand
			// 
			this.fatesHand.Interval = 900000;
			this.fatesHand.Tick += new System.EventHandler(this.FatesHandTick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(875, 601);
			this.Controls.Add(this.groupBox9);
			this.Controls.Add(this.groupBox8);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "CharSheet v7";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.skillDataGridView)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.experiencePoints)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.karmaPoints)).EndInit();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.attributesDataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.charAge)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.groupBox10.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.pictureFramePanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.charPictureBox)).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.groupBox8.ResumeLayout(false);
			this.groupBox9.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.diceCount)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.NumericUpDown diceCount;
		private System.Windows.Forms.ToolStripMenuItem configureFatesHandToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setDefaultDiceSidesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importDatabaseToolStripMenuItem;
		private System.Windows.Forms.Button ClearCheckedButton;
		private System.Windows.Forms.Button ClearPicture;
		private System.Windows.Forms.ToolStripMenuItem saveChangesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
		private System.Windows.Forms.Label attrPoints;
		private System.Windows.Forms.Label skillPoints;
		private System.Windows.Forms.CheckBox charNPC;
		private System.Windows.Forms.RichTextBox charInventory;
		private System.Windows.Forms.GroupBox groupBox10;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TextBox charHeight;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox charWeight;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem asCharsheetsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem asDatabseToolStripMenuItem;
		private System.Windows.Forms.TextBox charRank;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Timer fatesHand;
		private System.Windows.Forms.DataGridView attributesDataGridView;
		private System.Windows.Forms.ToolStripMenuItem fateTimerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem gMModesToolStripMenuItem;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.NumericUpDown experiencePoints;
		private System.Windows.Forms.Button skillDiceButton;
		private System.Windows.Forms.Button d20Button;
		private System.Windows.Forms.ComboBox d20Modifier;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown karmaPoints;
		private System.Windows.Forms.CheckedListBox charList;
		private System.Windows.Forms.Label notificationLabel;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.RichTextBox charAdvantagesDisadvantages;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.RichTextBox charNotes;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.ToolStripStatusLabel statusBarLabel;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.RichTextBox charBackground;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Button charPictureEnlarge;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.PictureBox charPictureBox;
		private System.Windows.Forms.Panel pictureFramePanel;
		private System.Windows.Forms.ToolStripMenuItem exportCharsheetToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem characterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.TextBox charAffiliation;
		private System.Windows.Forms.Button rollBtn;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown charAge;
		private System.Windows.Forms.ComboBox charGender;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox charSpecies;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox charCallsign;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox charName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView skillDataGridView;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}

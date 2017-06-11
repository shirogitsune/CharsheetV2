/// <summary>
/// The main UI form for the application. this has much of the UI logic. Also holds the dice rolling logic for now.
/// Author: Justin Pearce <whitefox@guardianfox.net>
/// </summary>

using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using CharSheetV2.DataLayer;
using CharSheetV2.Models;

namespace CharSheetV2
{
	/// <summary>
	/// This class contains the program logic for the main UI components of the application.
	/// </summary>
	public partial class MainForm : Form
	{
		private int diceSides = 20;
		private Boolean skillCheck;
		private Boolean switchingChars;
		private Boolean changesMade;
		private Boolean deletingCharacters; 
		private CharacterDataInterface database;
		private CharacterSheetModel currentCharacter;
		private ConfigurationModel config;
		private List<CharacterSheetModel> castOfCharacters;
		private List<Combatant> brawlers;
		public enum SkillCheckType {SKILL, ATTRIBUTE};
		
		/// <summary>
		/// Start the application form.
		/// </summary>
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.skillCheck = false;
			this.changesMade = false;
			this.deletingCharacters = false;
			this.loadConfig();
			this.InitCharacterList();
		}
		
		/// <summary>
		/// This method handles giving the user an opportunity to save any changes that 
		/// were made prior to quitting or closing the program.
		/// </summary>
		private void DoApplicationClose() {
			this.notificationLabel.Text = "Shutting down...";
			if(this.changesMade){
				//If changes are flagged, ask the user for direction.
				DialogResult saveChanges = MessageBox.Show("There were changes made that have not been saved.\nSave changes now?", 
				                                           "Apply Changes", 
				                                           MessageBoxButtons.YesNo, 
				                                           MessageBoxIcon.Exclamation);
				if(saveChanges == DialogResult.Yes){
					
					//Enumerate over the characters in the list
					IEnumerator<CharacterSheetModel> savable = castOfCharacters.GetEnumerator();
					while (savable.MoveNext()) {
						//If the character is 'volatile', then we need to save it's state.
						if (savable.Current.GetVolatility()) {
							this.notificationLabel.Text = "Saving " + savable.Current.characterName + "...";
						    savable.Current.Save();
						}
					}
					//Stop all operations and close.
					this.changesMade = false;
					this.database.VacuumDatabase();
					this.fatesHand.Stop();
					this.fatesHand.Dispose();
					Application.Exit();
				}else if(saveChanges == DialogResult.No){
					//Don't save the data and exit!
					this.changesMade = false;
					this.fatesHand.Stop();
					this.fatesHand.Dispose();
					Application.Exit();
				}
			}else{
				//Nothing to save, kill the timer and exit.
				this.changesMade = false;
				this.fatesHand.Stop();
				this.fatesHand.Dispose();
				Application.Exit();
			}
		}
		
		/// <summary>
		/// Initialize and populate the list of characters. This populates the UI control that
		/// allows for selecting a character as well as populating a data set containing all of the
		/// character data for reference.
		/// </summary>
		public void InitCharacterList() {
			//Create a list for the characters to 'live'
			this.castOfCharacters = new List<CharacterSheetModel>();
			//Open the database and perform an integrity check
			this.database = new CharacterDataInterface();
			if(this.database.IntegrityCheck()){
				//Get all the characters
				DataTable initialCharacterList = this.database.GetTable("characters");
				if(initialCharacterList != null && initialCharacterList.Rows.Count > 0){
					//If we have characters...
					this.charList.Items.Clear();
					foreach(DataRow row in initialCharacterList.Rows){
						//Load the character object from the database.
						CharacterSheetModel thisCharacter = new CharacterSheetModel();
						thisCharacter.LoadCharacterById(int.Parse(row.ItemArray.GetValue(0).ToString()));
						//Add the character to the related lists.
						castOfCharacters.Add(thisCharacter);
						this.charList.Items.Add(thisCharacter.characterName);
					}
					this.notificationLabel.Text = initialCharacterList.Rows.Count + " characters loaded.";
				} else {
					//No characters. Make some!
					this.notificationLabel.Text = "No characters found. Make some characters!";
				}
			}else{
				// "I sense a great distubance in the Database...as if thousands of records cried out in 
				// terror and were suddenly silenced. I fear soemthing terrible has happened." ~ Odbc Kenobi
				MessageBox.Show("The database failed it's integrity check! Please use another program to recover the data from the database file. "
				               +"(Hint: Search for 'how to repair a sqlite database')", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.notificationLabel.Text = "Error!";
			}
		}

		/// <summary>
		/// This method populates the configuration of the application from the table stored in the data files. If non exists, it 
		/// populates some defaults.
		/// </summary>
		private void loadConfig() {
			//TODO: Load Config
			this.config = new ConfigurationModel();
			this.diceSides = this.config.GetDiceSides();
			this.fatesHand.Interval = this.config.GetFatesHandTimer();
			this.configureFatesHandToolStripMenuItem.Checked = this.config.GetFatesHand();
		}
		
		/// <summary>
		/// This method populates the form fields with the data of the character selected from the
		/// character list. It also initailizes the DataGridViews that represent the 
		/// attributes and skils table entries associated with the character.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The arguments for the event.</param>
		public void CharListSelectedIndexChanged(object sender, EventArgs e){
			//If the number of checked items is more than one, don't update the UI.
			if (this.charList.CheckedItems.Count > 1) {
				this.switchingChars = true;				
			} else {
				if (this.skillCheck) {
					this.notificationLabel.Text = "Skill Roll Canceled...";
				}
				this.skillCheck = false;
			}
			//Don't default select a cell in the skill/attribute tables.
			this.skillDataGridView.CurrentCell = null;
			this.attributesDataGridView.CurrentCell = null;
			//Make sure we are not deleting and also within the rang of the elements in he list.
			if (!deletingCharacters && (this.charList.SelectedIndex < this.charList.Items.Count && this.charList.SelectedIndex >= 0)) {
				//Set the current character object.
				this.currentCharacter = this.castOfCharacters.ToArray()[this.charList.SelectedIndex];
				//Set alot of fields.
				this.charName.Text = this.currentCharacter.characterName;
				this.charCallsign.Text = this.currentCharacter.characterCallsign;
				this.charSpecies.Text = this.currentCharacter.characterSpecies;
				
				//Set the gender field.
				if(this.charGender.FindString(this.currentCharacter.characterGender) != -1) {
					this.charGender.SelectedIndex = this.charGender.FindString(this.currentCharacter.characterGender);
				} else {
					this.charGender.SelectedIndex = this.charGender.FindString("Other");
				}
				
				this.charAffiliation.Text = this.currentCharacter.characterAffiliation;
				
				//Ensure the age field is a non-0 value.
				try {
					this.charAge.Value = this.currentCharacter.characterAge;
				} catch (ArgumentOutOfRangeException ea) {
					ea.ToString();
					if(this.currentCharacter.characterAge > 0) {
						this.charAge.Value = this.charAge.Maximum;
					} else {
						this.charAge.Value = this.charAge.Minimum;
					}
				}
				
				this.charHeight.Text = this.currentCharacter.characterHeight;
				this.charWeight.Text = this.currentCharacter.characterWeight;
				this.charRank.Text = this.currentCharacter.characterRank;
				
				//Ensure the karma field is within the range for the field.
				try { 
					this.karmaPoints.Value = this.currentCharacter.characterKarma;
				} catch (ArgumentOutOfRangeException ek) {
					ek.ToString();
					if(this.currentCharacter.characterKarma > 0) {
						this.karmaPoints.Value = this.karmaPoints.Maximum;
					} else {
						this.karmaPoints.Value = this.karmaPoints.Minimum;
					}
				}
				
				//Ensure that the experience points value is within the acceptable range.
				try {
					this.experiencePoints.Value = Math.Abs(this.currentCharacter.characterExperience);
				} catch (ArgumentOutOfRangeException ee) {
					ee.ToString();
					if(this.currentCharacter.characterExperience > 0) {
						this.experiencePoints.Value = this.experiencePoints.Maximum;
					}
				}
				
				this.charBackground.Text = this.currentCharacter.characterBackground;
				this.charAdvantagesDisadvantages.Text = this.currentCharacter.characterAdvantages;
				this.charNotes.Text = this.currentCharacter.characterNotes;
				this.charInventory.Text = this.currentCharacter.characterInventory;
				
				//..and a checkbox.
				if (this.currentCharacter.isNPC == 1) {
					this.charNPC.Checked = true;				
				} else {
					this.charNPC.Checked = false;
				}
				
				//Wire up the Attributes data table to the grid view
				this.attributesDataGridView.DataSource = this.currentCharacter.characterAttributes;
				this.attributesDataGridView.CurrentCell = null;
				if (this.attributesDataGridView.Columns.Count > 0) {
					this.attributesDataGridView.AutoResizeColumn(3);
					this.attributesDataGridView.AutoResizeColumn(4);
					this.attributesDataGridView.AutoResizeColumn(5);
					
					//Hide the aid and cid columns
					this.attributesDataGridView.Columns[0].Visible = false;
					this.attributesDataGridView.Columns[1].Visible = false;
				}
							
				//Set total attribute points label
				int attrSum = 0;
				for (int x = 0; x < this.attributesDataGridView.Rows.Count; x++) {
					attrSum += Convert.ToInt32(this.attributesDataGridView.Rows[x].Cells[3].Value);
				}
				this.attrPoints.Text = this.attrPoints.Text.Substring(0, this.attrPoints.Text.LastIndexOf(':') + 1) + attrSum;
	
				//Wire up the Skills data tabe to the grid view.
				this.skillDataGridView.DataSource = this.currentCharacter.characterSkills;
				this.skillDataGridView.CurrentCell = null;
				if (this.skillDataGridView.Columns.Count > 0) {
					this.skillDataGridView.AutoResizeColumn(3);
					this.skillDataGridView.AutoResizeColumn(4);
					this.skillDataGridView.AutoResizeColumn(5);
				
					//Hide the sid and cid columns
					this.skillDataGridView.Columns[0].Visible = false;
					this.skillDataGridView.Columns[1].Visible = false;
				}
				
				//Set total skill points label;
				int skillSum = 0;
				for (int x = 0; x < this.skillDataGridView.Rows.Count; x++) {
					skillSum += Convert.ToInt32(this.skillDataGridView.Rows[x].Cells[3].Value);
				}
				this.skillPoints.Text = this.skillPoints.Text.Substring(0, this.skillPoints.Text.LastIndexOf(':') + 1) + skillSum;
				
				this.charPictureBox.Image = this.currentCharacter.GetCharacterImage();
			}
			//No longer switshing characters.
			this.switchingChars = false;
			
		}
		
		/// <summary>
		/// Starts and stops the 'Fates Hand' timer.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments.</param>
		public void FatesHandCheckedStateChanged(object sender, EventArgs e){
			if(this.fateTimerToolStripMenuItem.Checked){
				this.fatesHand.Start();
			}else{
				this.fatesHand.Stop();
			}
		}
		
		/// <summary>
		/// Opens a dialog allowing for the configuration of the number of minutes
		/// Fate's Hand will wait between ticks.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void ConfigureFatesHandToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			FatesHandDialog fhDialog = new FatesHandDialog(this.fatesHand.Enabled, this.fatesHand.Interval);
			fhDialog.ShowDialog();
			this.fatesHand.Interval = fhDialog.fatesHandTimerMillis;
			if(this.fateTimerToolStripMenuItem.Checked){
				this.fatesHand.Stop();
				this.fatesHand.Start();
			}
		}
		
		/// <summary>
		/// Opens a dialog allowing for the configuration of the number of dice
		/// sides to default to.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments.</param>
		public void SetDefaultDiceSidesToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			DiceSidesDialog dsDialog = new DiceSidesDialog(this.diceSides);
			dsDialog.ShowDialog();
			this.diceSides = dsDialog.numberOfDiceSides;
			this.d20Button.Text = "D"+this.diceSides.ToString();
		}
		
		/// <summary>
		/// This method fires on the timer end and selects one random character from the character list
		/// to call to the GM's attention.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments.</param>
		public void FatesHandTick(object sender, EventArgs e){
			//If we have one or more charcters.
			if(this.charList.Items.Count > 0){
				//Initialize a psudo-random and pick oneof the characters in the list.
				Random fate = new Random();
				int fated = fate.Next(0, this.charList.Items.Count);
				this.notificationLabel.Text = "Fate's Hand has selected "+castOfCharacters[fated].characterName.ToString()+"!";
				MessageBox.Show("Fate's Hand has selected "+castOfCharacters[fated].characterName.ToString()+"!", "Fate's Hand", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			//If the timer is still a go, restart the timer.
			if(this.fateTimerToolStripMenuItem.Checked){
				this.fatesHand.Start();
			}
		}
		
		/// <summary>
		/// Handler called when the application is closed, either through the
		/// 'X' button, or other means.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			this.DoApplicationClose();
		}
		
		/// <summary>
		/// Handler called when the 'Quit' option is selected from the 'File' menu
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void QuitProgram(object sender, EventArgs e)
		{
			this.Close();
		}
		
		/// <summary>
		/// Handler for clicking the D20 button. This calls the D20 dice roll method and applies
		/// any modifiers before alerting the user.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void D20ButtonClick(object sender, EventArgs e)
		{
			//Get the die value  (1D20)
			int diceCount = Int32.Parse(this.diceCount.Value.ToString());
			int dieValue = this.RollDie(this.diceSides, diceCount);
			
			//If there mod value is set, add it. Otherwise, don't.
			int modValue = 0;
			if( d20Modifier.SelectedIndex != -1 ){
				Int32.TryParse(d20Modifier.SelectedItem.ToString(), out modValue);
				this.notificationLabel.Text = "Rolled "+diceCount+" D"+this.diceSides.ToString()+" and got "+(dieValue+modValue)+" ("+modValue+")...";
			}else{
				this.notificationLabel.Text = "Rolled "+diceCount+" D"+this.diceSides.ToString()+" and got "+dieValue+"...";
			}
			this.diceCount.Value=1;
		}
		
		/// <summary>
		/// Performs a dice roll for a die of 'n' number of sides.
		/// </summary>
		/// <param name="sides">The number of sides to the 'die'</param>
		/// <returns>The result of the dice 'roll'</returns>
		public int RollDie(int sides, int count){
			//Initialize a psudo-random and pick a number between 1 and n.
			Random dice = new Random();
			int diceTotal = 0, diceToRoll = count;
			for(int i = diceToRoll; i > 0; i--) {
				diceTotal += dice.Next(1, sides+1);
			}
			return diceTotal;
		}
		
		/// <summary>
		/// Handle the Skill Check operation
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The event arguments.</param>
		public void SkillDiceButtonClick(object sender, EventArgs e)
		{
			//Do we have a character selected?
			if (this.charList.SelectedIndex >= 0) {	
				//Do we have more that one character?
				if (this.charList.CheckedItems.Count > 1) {
					//If not yet in a skill check...
					if (this.skillCheck != true) {
						//Setup for a multiplayer skill check
						this.notificationLabel.Text = "Click on skill or attribute to roll against for the selected characters...";
						brawlers = new List<Combatant>();
					} else {
						//Otherwise, terminate a skill check.
						this.skillCheck = false;
						brawlers.Clear();
						this.notificationLabel.Text = "Canceled VS mode...";
					}
				} else {
					//Single-player skill check.
					this.notificationLabel.Text = "Click on skill or attribute to roll against...";
				}
				this.skillCheck = true;
			} else {
				this.notificationLabel.Text = "Select one or more characters from the list.";
			}
			
		}
		
		/// <summary>
		/// Handler for processing the save menu item click
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event arguments.</param>
		public void SaveChangesToDatabase(object sender, EventArgs e)
		{
			if (this.changesMade) {
				//Enumerate over the characters in the list
				IEnumerator<CharacterSheetModel> savable = castOfCharacters.GetEnumerator();
				while (savable.MoveNext()) {
					//If the character is 'volatile', then we need to save it's state.
					if (savable.Current.GetVolatility()) {
						this.notificationLabel.Text = "Saving " + savable.Current.characterName + "...";
					    savable.Current.Save();
					}
				}
				//Clean up database.
				this.database.VacuumDatabase();
				this.notificationLabel.Text = "All Changes Saved!";
				this.changesMade = false;
			} else {
				this.notificationLabel.Text = "No changes detected.";
			}
		}
		
		/// <summary>
		/// Event handler for New Character operations.
		/// </summary>
		/// <param name="sender">The sender of the event object.</param>
		/// <param name="e">The event arguments.</param>
		public void CreateNewCharacter(object sender, EventArgs e)
		{
			//Initialize a new character model object.
			CharacterSheetModel newCharacter = new CharacterSheetModel();
			//Set it's name and id
			newCharacter.characterName = "New Character";
			newCharacter.characterId = -1;
			//Add the character to the database and the interface.
			this.castOfCharacters.Add(newCharacter);
			this.charList.Items.Add(newCharacter.characterName);
			newCharacter.Save();
			newCharacter.LoadCharacterById(newCharacter.characterId);
		}
		
		/// <summary>
		/// Event listener for deleting character data.
		/// </summary>
		/// <param name="sender">The object sending the event.</param>
		/// <param name="e">The event arguments.</param>
		public void DeleteSelectedCharacter(object sender, EventArgs e)
		{
			//Ensure that we have at least one character marked.
			if (this.charList.CheckedItems.Count == 0) {
				MessageBox.Show("There are no characters marked for deletion.\nPlease check the box next to the characters you wish to delete and try again.", 
				                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			
			//Populate a list of names of selected characters for confirmation.
			String names = "";
			for (int i = 0; i < this.charList.CheckedItems.Count; i++) {
				names += this.charList.CheckedItems[i].ToString() + ", ";
				if (i % 3 == 0 && i != 0) {
					names += "\n";
				}
			}
			if (names.LastIndexOf("\n") == names.Length) {
				names = names.Substring(0, names.Length - 3);
			} else {
				names = names.Substring(0, names.Length - 2);
			}
			//Show he confirmation dialog.
			DialogResult result = MessageBox.Show("The following characters will be deleted permanently:\n\n" + 
			                                       names + "\n\nAre you sure? This cannot be undone!", 
			                                       "Delete Characters", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			//If we want to delete the character(s)
			if (result == DialogResult.Yes) {
				//We're deleting, so empty th UI elements.
				this.deletingCharacters = true;
				this.EmptyUIFields();
				//Look through the list of characters, and remove the characters from the lists an database.
				for(int i = this.castOfCharacters.Count - 1; i >= 0; i--) {
					if (this.charList.CheckedIndices.Contains(i)) {
						this.castOfCharacters[i].Delete();
						this.castOfCharacters.RemoveAt(i);
						this.charList.Items.RemoveAt(i);
					}
				}
				//Update the list and clean up the database.
				this.charList.Update();
				this.database.VacuumDatabase();
				this.deletingCharacters = false;
			}
		}
		
		/// <summary>
		/// Handles clicking on the export selected as charsheets action.
		/// </summary>
		/// <param name="sender">The sending obejct.</param>
		/// <param name="e">The event arguments.</param>
		public void ExportAsCharsheetsClick(object sender, EventArgs e)
		{
			//Get a file browser dialog setup
			FolderBrowserDialog saveCharSheets = new FolderBrowserDialog();
			saveCharSheets.ShowNewFolderButton = true;
			saveCharSheets.RootFolder = System.Environment.SpecialFolder.Desktop;
			DialogResult result = saveCharSheets.ShowDialog();
			
			if (result == DialogResult.OK) {
				//On 'Ok'
				int exported = 0;
				for(int x = 0; x < this.charList.Items.Count; x++) {
					//Get the checked characters
					if ( this.charList.GetItemChecked(this.charList.Items.IndexOf(this.charList.Items[x]))) {
						//Make a new file from their name
						String filePath = saveCharSheets.SelectedPath + System.IO.Path.DirectorySeparatorChar 
									    + castOfCharacters[this.charList.Items.IndexOf(this.charList.Items[x])].characterName.Replace(" ", "_") + ".txt";
						//Trigger the export method on the model.
						if (castOfCharacters[this.charList.Items.IndexOf(this.charList.Items[x])].ExportAsCharSheet(filePath)) {
							exported ++;
						}
					}
				}
				MessageBox.Show("" + exported + " character sheets exported to\n" + saveCharSheets.SelectedPath, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			} else {
				this.notificationLabel.Text = "Export canceled...";
			}
		}
		
		/// <summary>
		/// Handles clicking on the export selected as database action.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event arguments.</param>
		public void ExportAsDatabaseClick(object sender, EventArgs e)
		{
			//Open a save dialog.
			SaveFileDialog exportDb = new SaveFileDialog();
			exportDb.AddExtension = true;
			exportDb.InitialDirectory = System.Environment.SpecialFolder.MyDocuments.ToString();
			exportDb.Filter = "CharSheet Database (.db)|*.db|All Files (*.*)|*.*";
			DialogResult result = exportDb.ShowDialog();
			
			if (result == DialogResult.OK) { //On 'Ok'
				int exported = 0;
				int exporting = 0;
				this.notificationLabel.Text = "Exporting...";
				CharacterDataInterface newDB = new CharacterDataInterface(exportDb.FileName);
				
				//Initialize a background thread.
				BackgroundWorker parser = new BackgroundWorker();
				parser.WorkerReportsProgress = true;
				
				for(int i = 0; i < this.charList.Items.Count; i++) {
					if ( this.charList.GetItemChecked(this.charList.Items.IndexOf(this.charList.Items[i]))) {
						exporting++;
					}
				}
				
				//Setup the delegate/handler to actually do the work.
				parser.DoWork += delegate(object workSender, DoWorkEventArgs ex) {
					for(int x = 0; x < this.charList.Items.Count; x++) {
						//Get the checked characters
						if ( this.charList.GetItemChecked(this.charList.Items.IndexOf(this.charList.Items[x]))) {
							CharacterSheetModel exportChar = new CharacterSheetModel(exportDb.FileName);
							if (exportChar.CloneFromCharacter(castOfCharacters[x])) {
								parser.ReportProgress(++exported);
							}
						}
					}
				};
				
				//Setup the delegat/handler to update our 'progress' indicator.
				parser.ProgressChanged += delegate(object progressSender, ProgressChangedEventArgs ex) {
					this.notificationLabel.Text = "Exporting Records.\nPlease Wait...[" 
												+ (ex.ProgressPercentage / exporting)*100 + "%]";
				};
				
				//Setup delegate/handler to process the completion message.
				parser.RunWorkerCompleted += delegate(object completeSender, RunWorkerCompletedEventArgs ext) {
					newDB.VacuumDatabase();
					MessageBox.Show("" + exported + " of " + exporting +" character sheets exported to\n" + exportDb.FileName, 
					                "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				};
				
				parser.RunWorkerAsync();
				//Since we don't actually have a status update from the process, in this case, make something up.
				while(parser.IsBusy) {
					//parser.ReportProgress(exported);
					Application.DoEvents();
				}
			} else {
				this.notificationLabel.Text = "Export canceled...";
			}
		}
		
		/// <summary>
		/// Shows the about dialog.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void AboutMenuItemClicked(object sender, EventArgs e){
			AboutDialog infoBox = new AboutDialog();
			infoBox.Show(this);
		}
		
		/// <summary>
		/// This method provides an interface for accessing a CharSheet v6.0 format file and reading
		/// in the data for parsing. The CharSheet v6.0 format file is a CSV-style of text file.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void ImportCharSheetFile(object sender, EventArgs e) {
			bool success = false;
			//For activity indication
			int progress = 0;
			String[] processAnim = new String[16] {"-", "-", "-", "-", "\\", "\\", "\\", "\\", "|", 
												   "|", "|", "|","/", "/", "/", "/"};
			
			//Open a file open dialog so the user can find the file
			OpenFileDialog importDialog = new OpenFileDialog();
			importDialog.Title = "Import Character Sheet Data...";
			importDialog.InitialDirectory = System.Environment.SpecialFolder.MyDocuments.ToString();
			importDialog.Multiselect = false;
			importDialog.Filter = "CharSheet Dat Files (.dat)|*.dat|All Files (*.*)|*.*";
			DialogResult result = importDialog.ShowDialog();
			this.notificationLabel.Text = "Starting up...";
			
			//If they click the OK button
			if (result == DialogResult.OK) {
				//Updating the UI requires that we run this in a background thread
				BackgroundWorker parser = new BackgroundWorker();
				parser.WorkerReportsProgress = true;
				
				//Setup the delegat/handler to update our 'progress' indicator.
				parser.ProgressChanged += delegate(object progressSender, ProgressChangedEventArgs ex) {
					this.notificationLabel.Text = "Parsing data file.\nPlease Wait...[" 
												 + processAnim[ex.ProgressPercentage % processAnim.GetLength(0)] 
												 + "]\n\nThis may take a while...";
				};
				//Setup the delegate/handler to actually do the work.
				parser.DoWork += delegate(object workSender, DoWorkEventArgs ex) {
					success = this.database.ParseCharSheetData(File.ReadAllText(@importDialog.FileName));
				};
				//Setup the handler/delecate to handle the results of the operation.
				parser.RunWorkerCompleted += delegate(object completeSender, RunWorkerCompletedEventArgs ext) {
					if(success) {
						//When done importing, update the character list and reset with new data.
						this.notificationLabel.Text = "Data Imported...[OK]";
						MessageBox.Show("Character data import was successful!", "Import Success!", 
						                MessageBoxButtons.OK, MessageBoxIcon.Information);
						this.charList.Invalidate(true);
						this.castOfCharacters = null;
						this.InitCharacterList();
					} 
					if (ext.Error != null) {
						//Oops....
						this.notificationLabel.Text = "Error importing character data!";
						MessageBox.Show("There was an error parsing the character sheet data.\n"
						                +"The error was:\n" + ext.Error.Message + ": " 
						                + ext.Error.StackTrace + "\n\nPlease check your data file and try again!",
						                "Error", 
						                MessageBoxButtons.OK, 
						                MessageBoxIcon.Error);
					}
				};
				//Start the process in hte background
				parser.RunWorkerAsync();
				//Since we don't actually have a status update from the process, in this case, make something up.
				while(parser.IsBusy) {
					parser.ReportProgress(++progress);
					Application.DoEvents();
				}
			} else {
				if(this.charList.Items.Count > 0){
					this.notificationLabel.Text = "Ready...";
				} else {
					this.notificationLabel.Text = "Still no characters. Perhaps you should make some?";
				}
			}
		}

		/// <summary>
		/// Handle importing character records from an external database file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ImportDatabaseClick(object sender, System.EventArgs e)
		{
			//For activity indication
			int progress = 0;
			String[] processAnim = new String[16] {"-", "-", "-", "-", "\\", "\\", "\\", "\\", "|", 
												   "|", "|", "|","/", "/", "/", "/"};
			//Open file dialog
			OpenFileDialog importDBDialog = new OpenFileDialog();
			importDBDialog.InitialDirectory = System.Environment.SpecialFolder.MyDocuments.ToString();
			importDBDialog.Filter = "CharSheet Database (.db)|*.db|All Files (*.*)|*.*";
			DialogResult result = importDBDialog.ShowDialog();
			
			if (result == DialogResult.OK) { //On 'Ok'
				this.notificationLabel.Text = "Importing...";
				//Open the import DB and pull in the characters table.
				CharacterDataInterface importDB = new CharacterDataInterface(importDBDialog.FileName);
				DataTable importCharacters = importDB.GetTable("characters");
				
				//Initialize a background thread.
				BackgroundWorker parser = new BackgroundWorker();
				parser.WorkerReportsProgress = true;
				
				//Setup the delegate/handler to actually do the work.
				parser.DoWork += delegate(object workSender, DoWorkEventArgs ex) {
					if(importCharacters != null && importCharacters.Rows.Count > 0){
						//If we have characters...
						foreach(DataRow row in importCharacters.Rows){
							//Load the character object from the database.
							CharacterSheetModel importCharacter = new CharacterSheetModel(importDBDialog.FileName);
							CharacterSheetModel newCharacter = new CharacterSheetModel();
							importCharacter.LoadCharacterById(int.Parse(row.ItemArray.GetValue(0).ToString()));
							//clone the character and it's data to this data base.
							newCharacter.CloneFromCharacter(importCharacter);
							parser.ReportProgress(0);
						}
					}
				};
				
				//Setup the delegat/handler to update our 'progress' indicator.
				parser.ProgressChanged += delegate(object progressSender, ProgressChangedEventArgs ex) {
					this.notificationLabel.Text = "Importing Records.\nPlease Wait...[" 
												  + processAnim[ex.ProgressPercentage % processAnim.GetLength(0)] + "]";
				};
				
				//Setup delegate/handler to process the completion message.
				parser.RunWorkerCompleted += delegate(object completeSender, RunWorkerCompletedEventArgs ext) {
					//Clean up the database and reset the UI with new data.
					importDB.VacuumDatabase();
					MessageBox.Show("Characters imported successfully!", 
					                "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.charList.Invalidate(true);
					this.castOfCharacters = null;
					this.InitCharacterList();
				};
				
				parser.RunWorkerAsync();
				//Since we don't actually have a status update from the process, in this case, make something up.
				while(parser.IsBusy) {
					parser.ReportProgress(++progress);
					Application.DoEvents();
				}
			} else {
				this.notificationLabel.Text = "Export canceled...";
			}
		}		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Gender' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharGenderSelectionChangeCommitted(object sender, EventArgs e)
		{
			if(this.currentCharacter != null) {
				if(this.currentCharacter.characterGender != this.charGender.Items[this.charGender.SelectedIndex].ToString()) {
					this.changesMade = true;
					currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterGender = this.charGender.Items[this.charGender.SelectedIndex].ToString();
			}
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Karma Points' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void KarmaPointsValueChanged(object sender, System.EventArgs e)
		{
			if (this.currentCharacter != null) {
				if (Convert.ToInt32(this.karmaPoints.Value) != this.currentCharacter.characterKarma) {
					this.changesMade = true;
					this.currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterKarma = Convert.ToInt32(this.karmaPoints.Value);
			}
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Age' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharAgeValueChanged(object sender, EventArgs e)
		{
			if (this.currentCharacter != null) {
				if (Convert.ToInt32(this.charAge.Value) != this.currentCharacter.characterAge) {
					this.changesMade = true;
					this.currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterAge = Convert.ToInt32(this.charAge.Value);
			}
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Experience Points' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void ExperiencePointsValueChanged(object sender, EventArgs e)
		{
			if (this.currentCharacter != null) {
				if (Convert.ToInt32(this.experiencePoints.Value) != this.currentCharacter.characterExperience) {
					this.changesMade = true;
					this.currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterExperience = Convert.ToInt32(this.experiencePoints.Value);
			}
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Name' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharNameTextChanged(object sender, EventArgs e)
		{
			if(this.currentCharacter != null) {
				if(this.currentCharacter.characterName != this.charName.Text) {
					this.changesMade = true;
					currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterName = this.charName.Text;
				this.charList.Items[this.charList.SelectedIndex] = this.charName.Text;
			}
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Callsign' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharCallsignTextChanged(object sender, EventArgs e)
		{
			if(this.currentCharacter != null) {
				if(this.currentCharacter.characterCallsign != this.charCallsign.Text){
					this.changesMade = true;
					currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterCallsign = this.charCallsign.Text;
			}
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Species' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharSpeciesTextChanged(object sender, EventArgs e)
		{
			if(this.currentCharacter != null) {
				if(this.currentCharacter.characterSpecies != this.charSpecies.Text) {
					this.changesMade = true;
					currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterSpecies = this.charSpecies.Text;
			}
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Height' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharHeightTextChanged(object sender, EventArgs e)
		{
			if(this.currentCharacter != null) {
				if(this.currentCharacter.characterHeight != this.charHeight.Text) {
					this.changesMade = true;
					currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterHeight = this.charHeight.Text;
			}
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Weight' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharWeightTextChanged(object sender, EventArgs e)
		{
			if(this.currentCharacter != null) {
				if(this.currentCharacter.characterWeight != this.charWeight.Text) {
					this.changesMade = true;
					currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterWeight = this.charWeight.Text;
			}

		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Affiliation' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharAffiliationTextChanged(object sender, EventArgs e)
		{
			if(this.currentCharacter != null) {
				if(this.currentCharacter.characterAffiliation != this.charAffiliation.Text){
					this.changesMade = true;
					currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterAffiliation = this.charAffiliation.Text;
			}
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Rank' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharRankTextChanged(object sender, EventArgs e)
		{
			if(this.currentCharacter != null) {
				if(this.currentCharacter.characterRank != this.charRank.Text) {
					this.changesMade = true;
					currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterRank = this.charRank.Text;
			}
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Advantages/Disadvantages' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharAdvantagesDisadvantagesTextChanged(object sender, EventArgs e)
		{			
			if(this.currentCharacter != null) {
				if(this.currentCharacter.characterAdvantages != this.charAdvantagesDisadvantages.Text) {
					this.changesMade = true;
					currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterAdvantages = this.charAdvantagesDisadvantages.Text;
			}
			
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Background' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharBackgroundTextChanged(object sender, EventArgs e)
		{
			if(this.currentCharacter != null) {
				if(this.currentCharacter.characterBackground != this.charBackground.Text) {
					this.changesMade = true;
					currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterBackground = this.charBackground.Text;
			}
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Inventory' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharInventoryTextChanged(object sender, EventArgs e)
		{
			if(this.currentCharacter != null) {
				if(this.currentCharacter.characterInventory != this.charInventory.Text) {
					this.changesMade = true;
					currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterInventory = this.charInventory.Text;
			}
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'Notes' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharNotesTextChanged(object sender, EventArgs e)
		{
			if(this.currentCharacter != null) {
				if(this.currentCharacter.characterNotes != this.charNotes.Text){
					this.changesMade = true;
					currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.characterNotes = this.charNotes.Text;
			}
		}
		
		/// <summary>
		/// Handler fired on change of the selected value of the 'isNPC' field
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">The event arguments</param>
		public void CharNPCCheckedChanged(object sender, EventArgs e)
		{
			int state = 0;
			if(this.currentCharacter != null) {
				if(this.charNPC.Checked) {
					state = 1;
					this.charNPC.ForeColor = Color.Red;
				} else {
					state = 0;
					this.charNPC.ForeColor = Color.Black;
				}
				if (state != this.currentCharacter.isNPC) {
					this.changesMade = true;
					currentCharacter.SetVolatility(true);
				} else {
					currentCharacter.SetVolatility(false);
				}
				this.currentCharacter.isNPC = state;
			}
		}
		
		/// <summary>
		/// Event handler fired on completion of editing a cell in the data table control.
		/// </summary>
		/// <param name="sender">The object sending the event (the view, in this case).</param>
		/// <param name="e">The arguments for the event.</param>
		public void SkillDataGridViewCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{	//If we have a character loaded.
			if(this.currentCharacter != null) {
				//Get the gridview requesting the edit.
				DataGridView v = (DataGridView) sender;
				DataGridViewRow skillRow = v.Rows[e.RowIndex];
				
				//Setup key/value sets
				String[] keys = new String[skillRow.Cells.Count - 1];
				String[] values = new String[skillRow.Cells.Count - 1];
				
				//Populate the key/value sets
				for(int i = 1; i < skillRow.Cells.Count; i++)
				{
					keys[i - 1] = skillRow.Cells[i].OwningColumn.Name.ToString();
					values[i - 1] = skillRow.Cells[i].Value.ToString();
				}
				//If the row has no key id set
				if (Int32.Parse(skillRow.Cells[0].Value.ToString()) == -1) {
					//New row
					skillRow.Cells[0].Value = this.database.InsertRecord("skills", keys, values);
				} else {
					//Updating the existing row.
					this.database.UpdateRecord("skills", keys, values, "sid", skillRow.Cells[0].Value.ToString());
				}
				
				//Update the row total label.
				int skillSum = 0;
				for (int x = 0; x < this.skillDataGridView.Rows.Count; x++) {
					skillSum += Convert.ToInt32(this.skillDataGridView.Rows[x].Cells[3].Value);
				}
				this.skillPoints.Text = this.skillPoints.Text.Substring(0, this.skillPoints.Text.LastIndexOf(':') + 1) + skillSum;
			}
		}
		
		/// <summary>
		/// Event handler to address rows being deleted from the data table object.
		/// </summary>
		/// <param name="sender">The object sending the event.</param>
		/// <param name="e">The event arguments.</param>
		public void SkillDataGridViewUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			//Make sure we have a character selected.
			if(this.currentCharacter != null) {
				DialogResult response = MessageBox.Show("This operation is permanent and immediate!\nAre you sure you wish to delete this skill?\nThis cannot be undone!", 
				                						"Delete Skill", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (response == DialogResult.Yes) {
					//If we really want to delete this, get the requesting row's key id and delete.
					this.database.DeleteRecord("skills", "sid", e.Row.Cells[0].Value.ToString());
				} else {
					e.Cancel = true;
				}
			}
		}
		
		/// <summary>
		/// Handler for processing when a row is actually removed from the grid view.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event arguments.</param>
		public void SkillDataGridViewUserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			//When the row is completely gone...update the row total label.
			int skillSum = 0;
			for (int x = 0; x < this.skillDataGridView.Rows.Count; x++) {
				skillSum += Convert.ToInt32(this.skillDataGridView.Rows[x].Cells[3].Value);
			}
			this.skillPoints.Text = this.skillPoints.Text.Substring(0, this.skillPoints.Text.LastIndexOf(':') + 1) + skillSum;
		}
		
		/// <summary>
		/// Event handler fired on completion of editing a cell in the data table control.
		/// </summary>
		/// <param name="sender">The object sending the event (the view, in this case).</param>
		/// <param name="e">The arguments for the event.</param>
		public void AttributesDataGridViewCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			//Make sure we have a character selected.
			if(this.currentCharacter != null) {
				//Get the gridview requesting an edit.
				DataGridView v = (DataGridView) sender;
				DataGridViewRow attributeRow = v.Rows[e.RowIndex];
				
				//Setup key/value data sets.
				String[] keys = new String[attributeRow.Cells.Count - 1];
				String[] values = new String[attributeRow.Cells.Count - 1];
				
				//Populate the data arrays from the table.
				for(int i = 1; i < attributeRow.Cells.Count; i++)
				{
					keys[i - 1] = attributeRow.Cells[i].OwningColumn.Name.ToString();
					values[i - 1] = attributeRow.Cells[i].Value.ToString();
				}
				
				//If the row has no key id set.
				if (Int32.Parse(attributeRow.Cells[0].Value.ToString()) == -1) {
					//Insert the new row and update the gridview with the resulting key.
					attributeRow.Cells[0].Value = this.database.InsertRecord("attributes", keys, values);
				} else {
					//Update the existing record.
					this.database.UpdateRecord("attributes", keys, values, "aid", attributeRow.Cells[0].Value.ToString());
				}
				
				//Update the row total label.
				int attrSum = 0;
				for (int x = 0; x < this.attributesDataGridView.Rows.Count; x++) {
					attrSum += Convert.ToInt32(this.attributesDataGridView.Rows[x].Cells[3].Value);
				}
				this.attrPoints.Text = this.attrPoints.Text.Substring(0, this.attrPoints.Text.LastIndexOf(':') + 1) + attrSum;
			}
		}
		
		/// <summary>
		/// Event handler to address rows being deleted from the data table object.
		/// </summary>
		/// <param name="sender">The object sending the event.</param>
		/// <param name="e">The event arguments.</param>
		public void AttributesDataGridViewUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			if(this.currentCharacter != null) {
				DialogResult response = MessageBox.Show("This operation is permanent and immediate!\nAre you sure you wish to delete this attribute?\nThis cannot be undone!", 
				                						"Delete Attribute", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (response == DialogResult.Yes) {
					//If we confirm to delete the record, then delete the row from the database.
					this.database.DeleteRecord("attributes", "aid", e.Row.Cells[0].Value.ToString());
				} else {
					e.Cancel = true;
				}
			}
		}
		
		/// <summary>
		/// Handler for once a row is actually deleted from the attached data grid view.
		/// </summary>
		/// <param name="sender">The sending object</param>
		/// <param name="e">The event arguments.</param>
		public void AttributesDataGridViewUserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			//Once the row is actually deleted, update the row total label.
			int attrSum = 0;
			for (int x = 0; x < this.attributesDataGridView.Rows.Count; x++) {
				attrSum += Convert.ToInt32(this.attributesDataGridView.Rows[x].Cells[3].Value);
			}
			this.attrPoints.Text = this.attrPoints.Text.Substring(0, this.attrPoints.Text.LastIndexOf(':') + 1) + attrSum;
		}
		
		/// <summary>
		/// Empty all of the UI fields or return their values to a default state.
		/// </summary>
		public void EmptyUIFields() {
			//Set alot of fields.
			this.currentCharacter = null;
			this.charName.Text = "";
			this.charCallsign.Text = "";
			this.charSpecies.Text = "";
			this.charGender.SelectedIndex = this.charGender.FindString("Male");
			this.charAffiliation.Text = "";
			this.charAge.Value = 18;
			this.charHeight.Text = "";
			this.charWeight.Text = "";
			this.charRank.Text = "";
			this.karmaPoints.Value = 0;
			this.experiencePoints.Value = 0;
			this.charBackground.Text = "";
			this.charAdvantagesDisadvantages.Text = "";
			this.charNotes.Text = "";
			this.charInventory.Text = "";
			//..and a checkbox.
			this.charNPC.Checked = false;
			this.charNPC.ForeColor = Color.Black;
			//Wire up the Attributes data table to the grid view
			this.attributesDataGridView.DataSource = null;
			//Wire up the Skills data tabe to the grid view.
			this.skillDataGridView.DataSource = null;
			//Clear point total labels.
			this.skillPoints.Text = this.skillPoints.Text.Substring(0, this.skillPoints.Text.LastIndexOf(':') + 1) + "0";
			this.attrPoints.Text = this.attrPoints.Text.Substring(0, this.attrPoints.Text.LastIndexOf(':') + 1) + "0";
		}
		
		/// <summary>
		/// Handler for processing the uploading of character pictures to the data tables.
		/// </summary>
		/// <param name="sender">the sender object.</param>
		/// <param name="e">The event arguments.</param>
		public void PictureFramePanelDoubleClick(object sender, EventArgs e)
		{
			if(this.currentCharacter != null) {
				//Open a file open dialog so the user can find the file
				OpenFileDialog importDialog = new OpenFileDialog();
				importDialog.Title = "Import Character Photo...";
				importDialog.InitialDirectory = System.Environment.SpecialFolder.MyDocuments.ToString();
				importDialog.Multiselect = false;
				importDialog.Filter = "Image Filed|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files (*.*)|*.*";
				DialogResult result = importDialog.ShowDialog();
				
				if (result == DialogResult.OK) {
					//If we really want this image, read in he bytes and write them to the database.
					this.currentCharacter.SetCharacterImage(@importDialog.FileName);
					//now that it's written, get the image object from the database.
					this.charPictureBox.Image = this.currentCharacter.GetCharacterImage();
					this.notificationLabel.Text = "Image added! Size: " + (importDialog.OpenFile().Length / 1024) + "kb";
				}
			}
		}
		
				/// <summary>
		/// Handle adding a tool tip to the picture box holding he character's image.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event arguments.</param>
		public void CharPictureBoxMouseHover(object sender, EventArgs e)
		{
			ToolTip tt = new ToolTip();
			//Check ifwe have an image set and do what is needed.
			if (this.charPictureBox.Image == null) {
				tt.SetToolTip(this.charPictureBox, "Double click to add a new picture.");	
			} else {
				tt.SetToolTip(this.charPictureBox, this.currentCharacter.characterName + "\nDouble click to replace the\nexisting picture with a new one.");	
			}
		}
		
		/// <summary>
		/// Handler to process clearig the character image data from the data table.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		public void ClearPictureClick(object sender, EventArgs e)
		{
			//Make certain we actually have a character selected.
			if(this.currentCharacter != null) {
				//Make certin we actually have a picture.
				if (this.charPictureBox.Image != null) {
					DialogResult result = MessageBox.Show("This will remove the attached picture from the character profile for: " +  this.currentCharacter.characterName + 
					                                      ". This cannot be undone!\n\nAre you sure?", "Delete Photo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (result == DialogResult.Yes) {
						//If we really want to remove the image, delete it from the database and clear the picture box.
						this.currentCharacter.ClearCharacterImage();
						this.charPictureBox.Image = null;
						this.notificationLabel.Text = "Picture for " + this.currentCharacter.characterName + " removed!";
						//Clean up the database (because BLOBs are messy).
						this.database.VacuumDatabase();
					}
				} else {
					this.notificationLabel.Text = this.currentCharacter.characterName + " has no picture. Try adding one first!";
				}
			}
		}
		
		/// <summary>
		/// Hander for processing the "View Larger" button.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event arguments.</param>
		public void CharPictureEnlargeClick(object sender, EventArgs e)
		{
			//Ensure that we have a character selected.
			if(this.currentCharacter != null) {
				//Ensure that we have a picture loaded.
				if (this.charPictureBox.Image != null) {
					//Make a new image viewer window.
					ImageViewer viewWindow = new ImageViewer();
					//Set the viewer's picture to this image.
					viewWindow.SetImage(this.charPictureBox.Image);
					//Set the viewer width so that it fits on screen
					if (this.charPictureBox.Image.Width > SystemInformation.VirtualScreen.Width) {
						viewWindow.Width = SystemInformation.VirtualScreen.Width;
					} else {
						viewWindow.Width = this.charPictureBox.Image.Width;					
					}
					//Set the viewer height so that it fits on screen
					if (this.charPictureBox.Image.Height > SystemInformation.VirtualScreen.Height) {
						viewWindow.Height = SystemInformation.VirtualScreen.Height;					
					} else {
						viewWindow.Height = this.charPictureBox.Image.Height;	
					}
					//Show the viewer on screen
					viewWindow.Show();
				} else {
					this.notificationLabel.Text = "No picture for " + this.currentCharacter.characterName + "! Try adding one first.";
				}
			}
		}
		
		//Dice Rules:
		/* 1 = Crit Success
		 * 20 = Crit Fail
		 * Roll success is roll value less than or equal to skill/attribute value
		 * Everything up to 3 less is 'marginal' success
		 * 
		 * In VS Mode, the one with the greater difference between the roll and their skill/attribute wins
		 * If any one fails, they lose.
		 * If all rolls fail, the one with the largest difference between skill and roll fails.
		 */
		/// <summary>
		/// Handle skill checks against skills.
		/// </summary>
		/// <param name="rowIndex">Index of the row in the character's skill table.</param>
		/// <param name="type">SkillCheckType value of SKILL or ATTRIBUTE.</param>
		public void SkillCheck(int rowIndex, SkillCheckType type) {

			//Populate a data grid view object with the view we clicked on.
			DataGridView currentGridView;
			switch(type) {
				case SkillCheckType.ATTRIBUTE:
					currentGridView = this.attributesDataGridView;
					break;
				case SkillCheckType.SKILL:
				default:
					currentGridView = this.skillDataGridView;
					break;
			}
			
			if (this.charList.CheckedItems.Count > 1) {
				//Handle skill checks for multiple characters
				String statistics = "";
				//Need to confirm more characters for Brawl
				if(this.brawlers.Count < this.charList.CheckedItems.Count - 1) {
					if(Convert.ToInt32(currentGridView.Rows[rowIndex].Cells[0].Value) != -1) {
						//Populate a combatant with stats
						Combatant fighter = new Combatant();
						fighter.Name = this.currentCharacter.characterName;
						fighter.Stat = currentGridView.Rows[rowIndex].Cells[2].Value.ToString();
						fighter.Points = Convert.ToInt32(currentGridView.Rows[rowIndex].Cells[3].Value);
						fighter.Points += Convert.ToInt32(currentGridView.Rows[rowIndex].Cells[4].Value);
						
						//Check if the combatant is already set and update them (or add if new)
						if (this.brawlers.FindIndex(Combatant=>Combatant.Name.Equals(fighter.Name)) >= 0) {
							int c = this.brawlers.FindIndex(Combatant=>Combatant.Name.Equals(fighter.Name));
							this.brawlers[c].Stat = fighter.Stat;
							this.brawlers[c].Points = fighter.Points;
						} else {
							this.brawlers.Add(fighter);
							this.notificationLabel.Text = this.brawlers.Count + " of " + this.charList.CheckedItems.Count + " selected...";
						}
					} else {
						this.notificationLabel.Text = "Not a valid row. Try again.";
					}	
				} else {
					//On the last fighter, populate their obejct and add them to the list.
					Combatant fighter = new Combatant();
					fighter.Name = this.currentCharacter.characterName;
					fighter.Stat = currentGridView.Rows[rowIndex].Cells[2].Value.ToString();
					fighter.Points = Convert.ToInt32(currentGridView.Rows[rowIndex].Cells[3].Value);
					fighter.Points += Convert.ToInt32(currentGridView.Rows[rowIndex].Cells[4].Value);
					this.brawlers.Add(fighter);
					
					//Roll GM dice
					int gmDice = this.RollDie(this.diceSides, Int32.Parse(this.diceCount.Value.ToString()));
					statistics += "GM Rolled: " + gmDice + "\n------------\n\n";
					//Flag failed rolls
					for( int x = 0; x < this.brawlers.Count; x++) {
						this.brawlers[x].RollResult = gmDice;
						if (this.brawlers[x].RollResult == 20 || 
						    this.brawlers[x].RollResult > this.brawlers[x].Points) {
							this.brawlers[x].Failed = true;
						}
						statistics += this.brawlers[x].Name + " with " + this.brawlers[x].Stat + " @ " + this.brawlers[x].Points + "\n\n";
					}
					//Try to weed out failed rolls
					List<Combatant> victors = this.brawlers.FindAll(Combatant=>Combatant.Failed == false);
					if (victors.Count == 0) {
						//Everyone failed! Get the one that failed the least
						Combatant leastLoser = this.brawlers[0];
						for (int y = 1; y < this.brawlers.Count; y++) {
							if ((this.brawlers[y].RollResult - this.brawlers[y].Points) < (leastLoser.RollResult - leastLoser.Points)) {
								leastLoser = this.brawlers[y];
							}
						}
						statistics += "------------\n\n" + leastLoser.Name + " failed the least.";
						this.notificationLabel.Text = "Everyone failed the check but " + leastLoser.Name + 
													  "\nfailed the least ... and succeeded I guess?";
					} else if (victors.Count == 1){
						//Only one clear winner
						statistics += "------------\n\n" + victors[0].Name + " was the clear winner.";
						this.notificationLabel.Text = victors[0].Name + " beat everyone else out with a roll of " + victors[0].RollResult + "!";
					} else {
						// More than one winner. Who won by more?
						Combatant biggestWinner = this.brawlers[0];
						for (int y = 1; y < this.brawlers.Count; y++) {
							if ((this.brawlers[y].Points - this.brawlers[y].RollResult) > (biggestWinner.Points - biggestWinner.RollResult)) {
								biggestWinner = this.brawlers[y];
							}
						}
						statistics += "------------\n\n" + biggestWinner.Name + " came out ahead.";
						this.notificationLabel.Text = "Close, but " + biggestWinner.Name + " succeeded.";
					}
					//Dump the list and mark done.
					this.brawlers.Clear();
					this.skillCheck = false;
					//Call out the statistics
					MessageBox.Show("Statistics:\n\n" + statistics, "Results", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			} else {
				// Handle skill checks for a single character
				if(Convert.ToInt32(currentGridView.Rows[rowIndex].Cells[0].Value) != -1) {
					//Get the points to check + modifiers
					int points = Convert.ToInt32(currentGridView.Rows[rowIndex].Cells[3].Value);
					points += Convert.ToInt32(currentGridView.Rows[rowIndex].Cells[4].Value);
					//Get the GM's die roll
					int roll = this.RollDie(this.diceSides, Int32.Parse(this.diceCount.Value.ToString()));
					switch (roll) {
						case 1: //Critical Success
							this.notificationLabel.Text = "Critical success!\n(Rolled " + roll + ")";
							break;
						case 20: //Critical Failure
							this.notificationLabel.Text = "Critical failure!\n(Rolled " + roll + ")";
							break;
						default: //Everything else
							if (roll > points) {
								//Failure
								this.notificationLabel.Text = "Failed!\n(Rolled " + roll + " vs " + points + ")";
							} else if ((roll >= points-3) && (roll <= points) ) {
								//Marginal success
								this.notificationLabel.Text = "Marginal Success.\n(Rolled " + roll + " vs " + points + ")";
							} else {
								//Success
								this.notificationLabel.Text = "Success!\n(Rolled " + roll + " vs " + points + ")";
							}
							break;
					}
					//Skill check complete.
					this.skillCheck = false;
				} else {
					this.notificationLabel.Text = "Not a valid row. Try again.";
				}
			}
			this.diceCount.Value = 1;
		}
		
		/// <summary>
		/// Handler to process clicking on a row in the skill grid view.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		public void SkillDataGridViewRowEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (this.skillCheck && !this.switchingChars) {
				this.SkillCheck(e.RowIndex, SkillCheckType.SKILL);
			}
		}
		
		/// <summary>
		/// Handler to process clicking on a row in the attribute grid view.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		public void AttributesDataGridViewRowEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (this.skillCheck  && !this.switchingChars) {
				this.SkillCheck(e.RowIndex, SkillCheckType.ATTRIBUTE);
			}
		}
		
		/// <summary>
		/// Handler for clicking the clear checked button.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		public void ClearCheckedButtonClick(object sender, EventArgs e)
		{
			//Go through all items and set them to unchecked.
			for (int x = 0; x < this.charList.Items.Count; x++) {
				this.charList.SetItemCheckState(x, CheckState.Unchecked);
			}
		}
	}
	
	/// <summary>
	/// This is a placeholder/data bucket for performing multi-character skill checkes
	/// </summary>
	public class Combatant 
	{
		public String Name = "";
		public String Stat = "";
		public int Points = 0;
		public int RollResult = 0;
		public Boolean Failed = false;
	}
}

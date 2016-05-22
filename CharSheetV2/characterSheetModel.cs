/// <summary>
/// This object model represents the data and oeprations that make up a Character Sheet. It provides getters and setters
/// for the more complicated character data while giving direct access to most of the simpler data types.
/// Author: Justin Pearce <whitefox@guardianfox.net>
/// </summary>

using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using CharSheetV2.DataLayer;

namespace CharSheetV2
{
	/// <summary>
	/// Model object for representing a character sheet. This model includes the
	/// information unique to a character as well as skills and attributes tables
	/// attached to a given chracter.
	/// </summary>
	public class CharacterSheetModel
	{
		public int characterId = -1;
		public int isNPC = 0;
		public String characterName = "";
		public String characterCallsign = "";
		public String characterSpecies = "";
		public String characterGender = "";
		public String characterHeight = "";
		public String characterWeight = "";
		public int characterAge = 18;
		public String characterAffiliation = "";
		public String characterRank = "";
		public int characterKarma = 0;
		public int characterExperience = 0;
		public String characterBackground = "";
		public String characterAdvantages = "";
		public String characterNotes = "";
		public String characterInventory = "";
		
		public DataTable characterAttributes = null;
		public DataTable characterSkills = null;
		
		private bool isVolatile = false;
		private CharacterDataInterface database = null;
		
		/// <summary>
		/// Class constructor
		/// </summary>
		public CharacterSheetModel()
		{
			this.database = new CharacterDataInterface();
		}
		
		/// <summary>
		/// Overridden constructor to accept a file name 
		/// for the location of the database.
		/// </summary>
		/// <param name="fileName">Path to database file.</param>
		public CharacterSheetModel(String fileName)
		{
			this.database = new CharacterDataInterface(fileName);
		}
		
		/// <summary>
		/// Load a character, it's attributes, and skills from the relevant database
		/// tables.
		/// </summary>
		/// <param name="cid">The id of the record in he characters table.</param>
		/// <returns>Tru on success or false on no record found.</returns>
		public Boolean LoadCharacterById(int cid=-1)
		{
			//Get table object from the database.
			DataTable characterData = this.database.SelectRecordsByKey("characters", "cid", cid.ToString());
			//If the table has rows...
			if(characterData.Rows.Count > 0){
				this.characterId = cid;
				//Grab the one row that should be in the table.
				DataRow character = characterData.Rows[0];
				
				//Populate all the character's data members with items from the database.
				this.characterName = character["name"].ToString();
				Int32.TryParse(character["isnpc"].ToString(), out this.isNPC);
				this.characterCallsign = character["callsign"].ToString();
				this.characterSpecies = character["species"].ToString();
				this.characterGender = character["gender"].ToString();
				this.characterHeight = character["height"].ToString();
				this.characterWeight = character["weight"].ToString();
				Int32.TryParse(character["age"].ToString(), out this.characterAge);
				this.characterAffiliation = character["affiliation"].ToString();
				this.characterRank = character["rank"].ToString();
				this.characterKarma = Int32.Parse(character["karma"].ToString());
				Int32.TryParse(character["experience"].ToString(), out this.characterExperience);
				this.characterBackground = character["background"].ToString();
				this.characterAdvantages = character["advantages"].ToString();
				this.characterNotes = character["notes"].ToString();
				this.characterInventory = character["inventory"].ToString();
				
				//Get the attribute records from their table and populate the data table object for this model.
				this.characterAttributes = this.database.SelectRecordsByKey("attributes", "cid", cid.ToString());
				this.characterAttributes.Columns[0].DefaultValue = "-1";
				this.characterAttributes.Columns[1].DefaultValue = this.characterId.ToString();
				this.characterAttributes.Columns[3].DefaultValue = "0";
				this.characterAttributes.Columns[4].DefaultValue = "0";
				this.characterAttributes.Columns[5].DefaultValue = "0";
				
				//Get the skill records from their table and populate the data table object for this model.
				this.characterSkills = this.database.SelectRecordsByKey("skills", "cid", cid.ToString());
				this.characterSkills.Columns[0].DefaultValue = "-1";
				this.characterSkills.Columns[1].DefaultValue = this.characterId.ToString();
				this.characterSkills.Columns[3].DefaultValue = "0";
				this.characterSkills.Columns[4].DefaultValue = "0";
				this.characterSkills.Columns[5].DefaultValue = "0";
				
				//Return true for success
				return true;
			}
			//Return false on failure.
			return false;
		}
		
		/// <summary>
		/// Persists the changes to the character model objet to the 
		/// database.
		/// </summary>
		/// <returns>True on success or false on failure.</returns>
		public Boolean Save()
		{
			Boolean success = false;
			//Create a dictionary object
			Dictionary<String, String> characterData = new Dictionary<String, String>();
			
			//Populate the object with key/value pairs.
			characterData.Add("name", this.characterName);
			characterData.Add("isnpc", "" + this.isNPC);
			characterData.Add("callsign", this.characterCallsign);
			characterData.Add("species", this.characterSpecies);
			characterData.Add("gender", this.characterGender);
			characterData.Add("height", this.characterHeight);
			characterData.Add("weight", this.characterWeight);
			characterData.Add("age", "" + this.characterAge.ToString());
			characterData.Add("affiliation", this.characterAffiliation);
			characterData.Add("rank", this.characterRank);
			characterData.Add("karma", this.characterKarma.ToString());
			characterData.Add("experience", this.characterExperience.ToString());
			characterData.Add("background", this.characterBackground);
			characterData.Add("advantages", this.characterAdvantages);
			characterData.Add("notes", this.characterNotes);
			characterData.Add("inventory", this.characterInventory);
			
			//If the character id is not lread set to a valid value
			if(this.characterId == -1)
			{	//New character, so insert the record.
				this.characterId = this.database.InsertRecord("characters", characterData);
				if ( this.characterId > -1 ) {
					success = true; 
				}
			}else
			{	//Existing character, update the record.
				success = this.database.UpdateRecord("characters", characterData, "cid", "" + this.characterId);
			}
			//Set the volatility flag on this model to false
			this.SetVolatility(false);
			return success;
		}
		
		/// <summary>
		/// Executes a member-wise clone of the provided character model and saves it to the database
		/// with a new id. It also clones all attribute and skill records to the database under the new 
		/// database id and imports the character photo (if any) into the new database.
		/// </summary>
		/// <param name="original">CharacterSheetModel to be cloned.</param>
		/// <returns>Boolean success or failure.</returns>
		public bool CloneFromCharacter(CharacterSheetModel original) {
			
			//Populate this model's member values with those of the provided model object.
			this.isNPC = original.isNPC;
			this.characterName = original.characterName;
			this.characterCallsign = original.characterCallsign;
			this.characterSpecies = original.characterSpecies;
			this.characterGender = original.characterGender;
			this.characterHeight = original.characterHeight;
			this.characterWeight = original.characterWeight;
			this.characterAge = original.characterAge;
			this.characterAffiliation = original.characterAffiliation;
			this.characterRank = original.characterRank;
			this.characterKarma = original.characterKarma;
			this.characterExperience = original.characterExperience;
			this.characterBackground = original.characterBackground;
			this.characterAdvantages = original.characterAdvantages;
			this.characterNotes = original.characterNotes;
			this.characterInventory = original.characterInventory;
			//Try saving the data
			if (this.Save()) {
				//Import the image data
				this.SetCharacterImageFromBytes(original.GetCharacterImageBytes());
				//Import the attributes from the provided model into this model.
				foreach(DataRow attributeRow in original.characterAttributes.Rows) {
					Object[] arr = attributeRow.ItemArray;
					String[] keys = new String[] {"cid", "attribute", "points", "modifier", "exempt"};
					String[] values = new String[] {this.characterId.ToString(), arr[2].ToString(), arr[3].ToString(), arr[4].ToString(), arr[5].ToString()};
					this.database.InsertRecord("attributes", keys, values);
				}
				//Import the skills from the provided model into this model.
				foreach(DataRow skillRow in original.characterSkills.Rows) {
					Object[] arr = skillRow.ItemArray;
					String[] keys = new String[] {"cid", "skill", "points", "modifier", "exempt"};
					String[] values = new String[] {this.characterId.ToString(), arr[2].ToString(), arr[3].ToString(), arr[4].ToString(), arr[5].ToString()};
					this.database.InsertRecord("skills", keys, values);
				}
				return true;
			} else {
				//Save failed.
				return false;
			}
			
		}
		
		/// <summary>
		/// Accepts a binary file to break down into bytes to store in the database.
		/// </summary>
		/// <param name="fileName">The string path to the desired file.</param>
		public void SetCharacterImage(String fileName) {
			byte[] imgData = File.ReadAllBytes(fileName);
			this.database.UpdateRecordBlob("characters", "picture", imgData, "cid", this.characterId.ToString());
		}
		
		/// <summary>
		/// Accepts a byte array to store in the database.
		/// </summary>
		/// <param name="fileName">The string path to the desired file.</param>
		public void SetCharacterImageFromBytes(byte[] imgData) {
			this.database.UpdateRecordBlob("characters", "picture", imgData, "cid", this.characterId.ToString());
		}
		
		/// <summary>
		/// Retrieves the picture data for the given character from the database.
		/// </summary>
		/// <returns>System.Drawing.Image object representing the image data from the database or null (if empty)</returns>
		public Image GetCharacterImage() {
			byte[] imgData = this.database.SelectBlobFieldByKey("characters", "picture", "cid", this.characterId.ToString());
			if (imgData.Length > 0) {
				MemoryStream imageStream = new MemoryStream(imgData);
				return Image.FromStream(imageStream);
			} else {
				return null;
			}
		}
		
		/// <summary>
		/// Retrieves the picture data as bytes for the given character from the database.
		/// </summary>
		/// <returns>Byte array representing the image data from the database or null (if empty)</returns>
		public byte[] GetCharacterImageBytes() {
			byte[] imgData = this.database.SelectBlobFieldByKey("characters", "picture", "cid", this.characterId.ToString());
			if (imgData.Length > 0) {
				return imgData;
			} else {
				return null;
			}
		}
		
		/// <summary>
		/// Clears the picture data by sending empty bytes to the data base field.
		/// </summary>
		public void ClearCharacterImage() {
			byte[] imgData = new Byte[0];
			this.database.UpdateRecordBlob("characters", "picture", imgData, "cid", this.characterId.ToString());
		}
		
		/// <summary>
		/// Set the isVolatile flag to a given boolean state. Used for marking the
		/// objects with changes so they can be saved when the application is terminated.
		/// </summary>
		/// <param name="state">Whether the objet has uncommitted changes.</param>
		public void SetVolatility(bool state) {
			this.isVolatile = state;
		}
		
		/// <summary>
		/// Get the state of the isVolatile flag. Used for determining if this 
		/// object has been marked as changes or not.
		/// </summary>
		/// <returns>The boolean value of isVolatile</returns>
		public bool GetVolatility() {
			return this.isVolatile;
		}
		
		/// <summary>
		/// Remove the records attached to this object from the relevant
		/// tables in the database.
		/// </summary>
		public void Delete()
		{
			//Blank the data table objects.
			this.characterAttributes = null;
			this.characterSkills = null;
			//Delete the records from the database.
			this.database.DeleteRecord("attributes", "cid", this.characterId.ToString());
			this.database.DeleteRecord("skills", "cid", this.characterId.ToString());
			this.database.DeleteRecord("characters", "cid", this.characterId.ToString());
		}	
		
		/// <summary>
		/// Method to output the content of the character model into a fomatted text file.
		/// </summary>
		/// <param name="filePath">The location to save the file.</param>
		/// <returns>Boolean on success or failure.</returns>
		public bool ExportAsCharSheet(String filePath) {
			try {
				using(StreamWriter sheetWriter = new StreamWriter(filePath)) {
					sheetWriter.WriteLine("Character Sheet");
					sheetWriter.WriteLine("=================================================");
					sheetWriter.WriteLine("-----------------\r\nGeneral\r\n-----------------");
					sheetWriter.WriteLine("Name: " + this.characterName);
					sheetWriter.WriteLine("Age: " + this.characterAge);
					sheetWriter.WriteLine("Gender: " + this.characterGender);
					sheetWriter.WriteLine("Species: " + this.characterSpecies);
					sheetWriter.WriteLine("Height: " + this.characterHeight);
					sheetWriter.WriteLine("Weight: " + this.characterWeight);
					sheetWriter.WriteLine("Rank: " + this.characterRank);
					sheetWriter.WriteLine("Callsign: " + this.characterCallsign);
					sheetWriter.WriteLine("\r\n\r\n-----------------\r\nAttributes");
					sheetWriter.WriteLine("-----------------------------------------------");
					sheetWriter.WriteLine(String.Format("{0}{1}{2}", "Attribute".PadRight(20),
						              			   					 "Points".PadRight(8), 
						              			   				     "Modifier".PadRight(8)));
					sheetWriter.WriteLine("-----------------------------------------------");
					foreach(DataRow attrRow in this.characterAttributes.Rows) {
						Object[] attrArray = attrRow.ItemArray;
						sheetWriter.WriteLine(String.Format("{0}{1}{2}", attrArray[2].ToString().PadRight(20),
						              			   						 attrArray[3].ToString().PadRight(8), 
						              			   						 attrArray[4].ToString().PadRight(8)));
					}
					sheetWriter.WriteLine("\r\n\r\n-----------------\r\nSkills");
					sheetWriter.WriteLine("-----------------------------------------------");
					sheetWriter.WriteLine(String.Format("{0}{1}{2}", "Skill".PadRight(20),
						              			   					 "Points".PadRight(8), 
						              			   				     "Modifier".PadRight(8)));
					sheetWriter.WriteLine("-----------------------------------------------");
					foreach(DataRow skillRow in this.characterSkills.Rows) {
						Object[] skillArray = skillRow.ItemArray;
						sheetWriter.WriteLine(String.Format("{0}{1}{2}", skillArray[2].ToString().PadRight(20),
						              			   						 skillArray[3].ToString().PadRight(8), 
						              			   						 skillArray[4].ToString().PadRight(8)));
					}
					sheetWriter.WriteLine("\r\n\r\n-----------------\r\nBackground\r\n-----------------");
					sheetWriter.WriteLine(this.characterBackground);
					sheetWriter.WriteLine("\r\n\r\n-----------------\r\nAdvantages\\Disadvantages\r\n-----------------");
					sheetWriter.WriteLine(this.characterAdvantages);
					sheetWriter.WriteLine("\r\n\r\n-----------------\r\nInventory\r\n-----------------");
					sheetWriter.WriteLine(this.characterInventory);
					sheetWriter.WriteLine("\r\n\r\n-----------------\r\nNotes\r\n-----------------");
					sheetWriter.WriteLine(this.characterNotes);
					sheetWriter.WriteLine("\r\n\r\n\r\n============================================");
					sheetWriter.WriteLine(String.Format("Generated By: CharSheet V2 at {0}", DateTime.Now));
					
					sheetWriter.Flush();
					sheetWriter.Close();
					return true;
				}
			} catch (IOException ioe) {
				Debug.WriteLine(ioe.Message);
				return false;
			}
		}
	}
}

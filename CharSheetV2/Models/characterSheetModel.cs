// This object model represents the data and operations that make up a Character Sheet. It provides getters and setters
// for the more complicated character data while giving direct access to most of the simpler data types.
// Author: Justin Pearce <whitefox@guardianfox.net>

using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using CharSheetV2.DataLayer;

namespace CharSheetV2.Models
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
		public string characterName = "";
		public string characterCallsign = "";
		public string characterSpecies = "";
		public string characterGender = "";
		public string characterHeight = "";
		public string characterWeight = "";
		public int characterAge = 18;
		public string characterAffiliation = "";
		public string characterRank = "";
		public int characterKarma = 0;
		public int characterExperience = 0;
		public string characterBackground = "";
		public string characterAdvantages = "";
		public string characterNotes = "";
		public string characterInventory = "";
		
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
		public CharacterSheetModel(string fileName)
		{
			this.database = new CharacterDataInterface(fileName);
		}
		
		/// <summary>
		/// Load a character, it's attributes, and skills from the relevant database
		/// tables.
		/// </summary>
		/// <param name="cid">The id of the record in he characters table.</param>
		/// <returns>True on success or false on no record found.</returns>
		public bool LoadCharacterById(int cid=-1)
		{
			//Get table object from the database.
			DataTable characterData = database.SelectRecordsByKey("characters", "cid", cid.ToString());
			//If the table has rows...
			if(characterData.Rows.Count > 0){
				characterId = cid;
				//Grab the one row that should be in the table.
				DataRow character = characterData.Rows[0];
				
				//Populate all the character's data members with items from the database.
                characterName = character["name"].ToString();
				Int32.TryParse(character["isnpc"].ToString(), out isNPC);
				characterCallsign = character["callsign"].ToString();
				characterSpecies = character["species"].ToString();
				characterGender = character["gender"].ToString();
				characterHeight = character["height"].ToString();
				characterWeight = character["weight"].ToString();
				Int32.TryParse(character["age"].ToString(), out characterAge);
				characterAffiliation = character["affiliation"].ToString();
				characterRank = character["rank"].ToString();
				characterKarma = Int32.Parse(character["karma"].ToString());
				Int32.TryParse(character["experience"].ToString(), out characterExperience);
				characterBackground = character["background"].ToString();
				characterAdvantages = character["advantages"].ToString();
				characterNotes = character["notes"].ToString();
				characterInventory = character["inventory"].ToString();
				
				//Get the attribute records from their table and populate the data table object for this model.
				characterAttributes = database.SelectRecordsByKey("attributes", "cid", cid.ToString());
				characterAttributes.Columns[0].DefaultValue = "-1";
				characterAttributes.Columns[1].DefaultValue = characterId.ToString();
				characterAttributes.Columns[3].DefaultValue = "0";
				characterAttributes.Columns[4].DefaultValue = "0";
				characterAttributes.Columns[5].DefaultValue = "0";
				
				//Get the skill records from their table and populate the data table object for this model.
				characterSkills = database.SelectRecordsByKey("skills", "cid", cid.ToString());
				characterSkills.Columns[0].DefaultValue = "-1";
				characterSkills.Columns[1].DefaultValue = characterId.ToString();
				characterSkills.Columns[3].DefaultValue = "0";
				characterSkills.Columns[4].DefaultValue = "0";
				characterSkills.Columns[5].DefaultValue = "0";
				
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
		public bool Save()
		{
			bool success = false;
			//Create a dictionary object
			var characterData = new Dictionary<string, string>();
			
			//Populate the object with key/value pairs.
			characterData.Add("name", characterName);
			characterData.Add("isnpc", "" + isNPC);
			characterData.Add("callsign", characterCallsign);
			characterData.Add("species", characterSpecies);
			characterData.Add("gender", characterGender);
			characterData.Add("height", characterHeight);
			characterData.Add("weight", characterWeight);
			characterData.Add("age", "" + characterAge.ToString());
			characterData.Add("affiliation", characterAffiliation);
			characterData.Add("rank", characterRank);
			characterData.Add("karma", characterKarma.ToString());
			characterData.Add("experience", characterExperience.ToString());
			characterData.Add("background", characterBackground);
			characterData.Add("advantages", characterAdvantages);
			characterData.Add("notes", characterNotes);
			characterData.Add("inventory", characterInventory);
			
			//If the character id is not lread set to a valid value
			if(characterId == -1)
			{	//New character, so insert the record.
				characterId = database.InsertRecord("characters", characterData);
                success |= characterId > -1;
			}else
			{	//Existing character, update the record.
				success = database.UpdateRecord("characters", characterData, "cid", "" + characterId);
			}
			//Set the volatility flag on this model to false
			SetVolatility(false);
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
			isNPC = original.isNPC;
			characterName = original.characterName;
			characterCallsign = original.characterCallsign;
			characterSpecies = original.characterSpecies;
			characterGender = original.characterGender;
			characterHeight = original.characterHeight;
			characterWeight = original.characterWeight;
			characterAge = original.characterAge;
			characterAffiliation = original.characterAffiliation;
			characterRank = original.characterRank;
			characterKarma = original.characterKarma;
			characterExperience = original.characterExperience;
			characterBackground = original.characterBackground;
			characterAdvantages = original.characterAdvantages;
			characterNotes = original.characterNotes;
			characterInventory = original.characterInventory;
			//Try saving the data
			if (Save()) {
				//Import the image data
				SetCharacterImageFromBytes(original.GetCharacterImageBytes());
				//Import the attributes from the provided model into this model.
				foreach(DataRow attributeRow in original.characterAttributes.Rows) {
					Object[] arr = attributeRow.ItemArray;
					string[] keys = {"cid", "attribute", "points", "modifier", "exempt"};
					string[] values = {characterId.ToString(), arr[2].ToString(), arr[3].ToString(), arr[4].ToString(), arr[5].ToString()};
					database.InsertRecord("attributes", keys, values);
				}
				//Import the skills from the provided model into this model.
				foreach(DataRow skillRow in original.characterSkills.Rows) {
					Object[] arr = skillRow.ItemArray;
					string[] keys = {"cid", "skill", "points", "modifier", "exempt"};
					string[] values = {characterId.ToString(), arr[2].ToString(), arr[3].ToString(), arr[4].ToString(), arr[5].ToString()};
					database.InsertRecord("skills", keys, values);
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
		public void SetCharacterImage(string fileName) {
			byte[] imgData = File.ReadAllBytes(fileName);
			database.UpdateRecordBlob("characters", "picture", imgData, "cid", characterId.ToString());
		}
		
		/// <summary>
		/// Accepts a byte array to store in the database.
		/// </summary>
		/// <param name="imgData">The string path to the desired file.</param>
		public void SetCharacterImageFromBytes(byte[] imgData) {
			database.UpdateRecordBlob("characters", "picture", imgData, "cid", characterId.ToString());
		}
		
		/// <summary>
		/// Retrieves the picture data for the given character from the database.
		/// </summary>
		/// <returns>System.Drawing.Image object representing the image data from the database or null (if empty)</returns>
		public Image GetCharacterImage() {
			byte[] imgData = database.SelectBlobFieldByKey("characters", "picture", "cid", characterId.ToString());
			if (imgData.Length > 0) {
				var imageStream = new MemoryStream(imgData);
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
			byte[] imgData = database.SelectBlobFieldByKey("characters", "picture", "cid", characterId.ToString());
            return imgData.Length > 0 ? imgData : null;
		}
		
		/// <summary>
		/// Clears the picture data by sending empty bytes to the data base field.
		/// </summary>
		public void ClearCharacterImage() {
			byte[] imgData = new Byte[0];
			database.UpdateRecordBlob("characters", "picture", imgData, "cid", characterId.ToString());
		}
		
		/// <summary>
		/// Set the isVolatile flag to a given boolean state. Used for marking the
		/// objects with changes so they can be saved when the application is terminated.
		/// </summary>
		/// <param name="state">Whether the objet has uncommitted changes.</param>
		public void SetVolatility(bool state) {
			isVolatile = state;
		}
		
		/// <summary>
		/// Get the state of the isVolatile flag. Used for determining if this 
		/// object has been marked as changes or not.
		/// </summary>
		/// <returns>The boolean value of isVolatile</returns>
		public bool GetVolatility() {
			return isVolatile;
		}
		
		/// <summary>
		/// Remove the records attached to this object from the relevant
		/// tables in the database.
		/// </summary>
		public void Delete()
		{
			//Blank the data table objects.
			characterAttributes = null;
			characterSkills = null;
			//Delete the records from the database.
			database.DeleteRecord("attributes", "cid", characterId.ToString());
			database.DeleteRecord("skills", "cid", characterId.ToString());
			database.DeleteRecord("characters", "cid", characterId.ToString());
		}	
		
		/// <summary>
		/// Method to output the content of the character model into a fomatted text file.
		/// </summary>
		/// <param name="filePath">The location to save the file.</param>
		/// <returns>Boolean on success or failure.</returns>
		public bool ExportAsCharSheet(string filePath) {
			try {
				using(var sheetWriter = new StreamWriter(filePath)) {
					sheetWriter.WriteLine("Character Sheet");
					sheetWriter.WriteLine("=================================================");
					sheetWriter.WriteLine("-----------------\r\nGeneral\r\n-----------------");
					sheetWriter.WriteLine("Name: " + characterName);
					sheetWriter.WriteLine("Age: " + characterAge);
					sheetWriter.WriteLine("Gender: " + characterGender);
					sheetWriter.WriteLine("Species: " + characterSpecies);
					sheetWriter.WriteLine("Height: " + characterHeight);
					sheetWriter.WriteLine("Weight: " + characterWeight);
					sheetWriter.WriteLine("Rank: " + characterRank);
					sheetWriter.WriteLine("Callsign: " + characterCallsign);
					sheetWriter.WriteLine("\r\n\r\n-----------------\r\nAttributes");
					sheetWriter.WriteLine("-----------------------------------------------");
					sheetWriter.WriteLine(string.Format("{0}{1}{2}", "Attribute".PadRight(20),
						              			   					 "Points".PadRight(8), 
						              			   				     "Modifier".PadRight(8)));
					sheetWriter.WriteLine("-----------------------------------------------");
					foreach(DataRow attrRow in characterAttributes.Rows) {
						Object[] attrArray = attrRow.ItemArray;
						sheetWriter.WriteLine(string.Format("{0}{1}{2}", attrArray[2].ToString().PadRight(20),
						              			   						 attrArray[3].ToString().PadRight(8), 
						              			   						 attrArray[4].ToString().PadRight(8)));
					}
					sheetWriter.WriteLine("\r\n\r\n-----------------\r\nSkills");
					sheetWriter.WriteLine("-----------------------------------------------");
					sheetWriter.WriteLine(string.Format("{0}{1}{2}", "Skill".PadRight(20),
						              			   					 "Points".PadRight(8), 
						              			   				     "Modifier".PadRight(8)));
					sheetWriter.WriteLine("-----------------------------------------------");
					foreach(DataRow skillRow in characterSkills.Rows) {
						Object[] skillArray = skillRow.ItemArray;
						sheetWriter.WriteLine(string.Format("{0}{1}{2}", skillArray[2].ToString().PadRight(20),
						              			   						 skillArray[3].ToString().PadRight(8), 
						              			   						 skillArray[4].ToString().PadRight(8)));
					}
					sheetWriter.WriteLine("\r\n\r\n-----------------\r\nBackground\r\n-----------------");
					sheetWriter.WriteLine(characterBackground);
					sheetWriter.WriteLine("\r\n\r\n-----------------\r\nAdvantages\\Disadvantages\r\n-----------------");
					sheetWriter.WriteLine(characterAdvantages);
					sheetWriter.WriteLine("\r\n\r\n-----------------\r\nInventory\r\n-----------------");
					sheetWriter.WriteLine(characterInventory);
					sheetWriter.WriteLine("\r\n\r\n-----------------\r\nNotes\r\n-----------------");
					sheetWriter.WriteLine(characterNotes);
					sheetWriter.WriteLine("\r\n\r\n\r\n============================================");
					sheetWriter.WriteLine(string.Format("Generated By: CharSheet V2 at {0}", DateTime.Now));
					
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

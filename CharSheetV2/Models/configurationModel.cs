/// <summary>
/// This object model represents the data and operations that make up the application configuration. 
/// It provides getters and setters for configuration values to make them consistant.
/// Author: Justin Pearce <whitefox@guardianfox.net>
/// </summary>

using System;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using CharSheetV2.DataLayer;

namespace CharSheetV2.Models
{

	/// <summary>
	/// Model object for representing a configuration. This model includes the
	/// information used by the application and stored as generic key/value pairs
	/// in the database.
	/// </summary>
	public class ConfigurationModel
	{
		private bool fatesHandSetting = false;
		private int diceSides = 20; //D20 FTW!
		private int fatesHandTimer = 900; //15 minutes by default
				
		private bool isVolatile = false;
		
		private CharacterDataInterface database = null;
		
		/// <summary>
		/// Class constructor
		/// </summary>
		public ConfigurationModel()
		{
			this.database = new CharacterDataInterface();
		}
		
		/// <summary>
		/// Overridden constructor to accept a file name 
		/// for the location of the database.
		/// </summary>
		/// <param name="fileName">Path to database file.</param>
		public ConfigurationModel(String fileName)
		{
			this.database = new CharacterDataInterface(fileName);
		}
		
		/// <summary>
		/// Loads the system configuration from the database
		/// </summary>
		public void loadConfiguration() {
			DataTable config = this.database.GetTable("config");
			foreach(DataRow row in config.Rows) {
				String key = row["configKey"].ToString();
				switch(key) {
					case "fatesHandSetting":
						this.fatesHandSetting = Boolean.Parse(row["configValue"].ToString());
						break;
					case "diceSides":
						this.diceSides = Int32.Parse(row["configValue"].ToString());
						break;
					case "fatesHandTimer":
						this.fatesHandTimer = Int32.Parse(row["configValue"].ToString());
						break;
				}
			}
		}
		
		/// <summary>
		/// Saves the current configuration state.
		/// </summary>
		/// <returns>True on success or false on failure.</returns>
		public Boolean saveConfiguration() {
			//TODO: Save the configuration state.
			this.isVolatile = false;
			
			return this.isVolatile;
		}
		
		/// <summary>
		/// Sets the number of sides for the dice to be rolled.
		/// </summary>
		/// <returns>Number of sides to the dice.</returns>
		public int GetDiceSides() {
			return this.diceSides;
		}
		
		/// <summary>
		/// Sets the side of dice to be rolled.
		/// </summary>
		/// <param name="sides">Number of sides to the dice to be rolled.</param>
		public void SetDiceSides(int sides) {
			this.isVolatile = true;
			this.diceSides = sides;
		}
		
		/// <summary>
		/// Gets the current state of Fate's Hand.
		/// </summary>
		/// <returns>Boolean indicating it's state.</returns>
		public bool GetFatesHand() {
			return this.fatesHandSetting;
		}
		
		/// <summary>
		/// Sets the state of Fate's Hand (0 for off, 1 for on)
		/// </summary>
		/// <param name="state">Integer representing the value of Fate's Hand</param>
		public void SetFatesHand(bool state) {
			this.isVolatile = true;
			this.fatesHandSetting = state;
		}
		
		/// <summary>
		/// Gets the current configured time for Fate's Hand.
		/// </summary>
		/// <returns>Number of seconds until Fate's Hand is triggered.</returns>
		public int GetFatesHandTimer() {
			return this.fatesHandTimer;
		}
		
		/// <summary>
		/// Sets the value for Fate's Hand
		/// </summary>
		/// <param name="seconds">Number of seconds before Fate's Hand is triggered.</param>
		public void SetFatesHandTimer(int seconds) {
			this.isVolatile = true;
			this.fatesHandTimer = seconds;
		}
	}
}

/// <summary>
/// This object model represents the data and operations that make up the application configuration. 
/// It provides getters and setters for configuration values to make them consistant.
/// Author: Justin Pearce <whitefox@guardianfox.net>
/// </summary>

using System;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Collections;
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
		//Defaults
		const bool fatesHandSetting = false;
		const int diceSides = 20; //D20 FTW!
		const int fatesHandTimer = 900000; //15 minutes by default
				
		private bool isVolatile = false;
		private Dictionary<String, String> configurationStore;
		
		private CharacterDataInterface database = null;
		
		/// <summary>
		/// Class Constructor
		/// </summary>
		/// <param name="datasource">The application's CharacterDatainterface object.</param>
		public ConfigurationModel(CharacterDataInterface datasource)
		{
			this.database = datasource;
			this.configurationStore = new Dictionary<String, String>();
		}
		
		/// <summary>
		/// Loads the system configuration from the database
		/// </summary>
		public void LoadConfiguration() {
			try {
				DataTable config = this.database.GetTable("config");
				String[] keys = new String[2]{"configKey", "configValue"};
				if (config.Rows.Count < 1) {
					this.configurationStore.Add("diceSides", diceSides.ToString());
					this.configurationStore.Add("fatesHandSetting", fatesHandSetting.ToString());
					this.configurationStore.Add("fatesHandTimer", fatesHandTimer.ToString());
					//Get the enumerator for the dictionary.
					IDictionaryEnumerator entries = (IDictionaryEnumerator)this.configurationStore.GetEnumerator();
					//Iterate over the key/value pairs and populate the key/value arrays.
					while(entries.MoveNext()){
						String[] values = new String[2]{entries.Key.ToString(), entries.Value.ToString()};
						this.database.InsertRecord("config", keys, values);
					}
				} else {
					foreach(DataRow row in config.Rows) {
						this.configurationStore.Add(row["configKey"].ToString(), row["configValue"].ToString());
					}
				}
			} catch(Exception e) {
				throw new DataException("Cannot Load Configuration!", e);
			}
		}
		
		/// <summary>
		/// Saves the current configuration state.
		/// </summary>
		/// <returns>True on success or false on failure.</returns>
		public Boolean SaveConfiguration() {
			try {
				this.database.TruncateTable("config");
				foreach(KeyValuePair<String, String> setting in this.configurationStore) {
					this.database.InsertRecord("config", new String[2]{"configKey", "configValue"}, new String[2]{setting.Key, setting.Value});
				}
				if(this.database.GetRecordCount("config", "cid") > 0){
					this.isVolatile = false;
					return true;
				} else {
					return false;
				}
			} catch(Exception e){
				throw new DataException("Cannot Save Configuration!", e);
			}
		}
				
		/// <summary>
		/// Get the state of the configuration
		/// </summary>
		/// <returns>bool configuration changed.</returns>
		public bool IsConfigChanged() {
			return this.isVolatile;
		}
		
		/// <summary>
		/// Sets the number of sides for the dice to be rolled.
		/// </summary>
		/// <returns>Number of sides to the dice.</returns>
		public int GetDiceSides() {
			return Int32.Parse(this.configurationStore["diceSides"]);
		}
		
		/// <summary>
		/// Sets the side of dice to be rolled.
		/// </summary>
		/// <param name="sides">Number of sides to the dice to be rolled.</param>
		public void SetDiceSides(int sides) {
			this.isVolatile = true;
			this.configurationStore["diceSides"] = sides.ToString();
		}
		
		/// <summary>
		/// Gets the current state of Fate's Hand.
		/// </summary>
		/// <returns>Boolean indicating it's state.</returns>
		public bool GetFatesHand() {
			return Boolean.Parse(this.configurationStore["fatesHandSetting"]);
		}
		
		/// <summary>
		/// Sets the state of Fate's Hand (0 for off, 1 for on)
		/// </summary>
		/// <param name="state">Integer representing the value of Fate's Hand</param>
		public void SetFatesHand(bool state) {
			this.isVolatile = true;
			this.configurationStore["fatesHandSetting"] = state.ToString();
		}
		
		/// <summary>
		/// Gets the current configured time for Fate's Hand.
		/// </summary>
		/// <returns>Number of seconds until Fate's Hand is triggered.</returns>
		public int GetFatesHandTimer() {
			return Int32.Parse(this.configurationStore["fatesHandTimer"]);
		}
		
		/// <summary>
		/// Sets the value for Fate's Hand
		/// </summary>
		/// <param name="seconds">Number of seconds before Fate's Hand is triggered.</param>
		public void SetFatesHandTimer(int seconds) {
			this.isVolatile = true;
			this.configurationStore["fatesHandTimer"] = seconds.ToString();
		}
	}
}

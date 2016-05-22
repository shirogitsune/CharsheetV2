/// <summary>
/// This object provides an interface into interacting with a SQLite database. This allows the character sheet model 
/// to get and set data in the database.
/// Author: Justin Pearce <whitefox@guardianfox.net>
/// </summary>

using System;
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CharSheetV2.DataLayer
{
	/// <summary>
	/// This class provides a methods to interact with a SQLite database system. It provides a variety of methods for performing
	/// CRUD operations on records in the database as well as a few utility methods for testing and maintaining he database file.
	/// </summary>
	public class CharacterDataInterface
	{
		/// <summary>
		/// Database connection string.
		/// </summary>
		private String dbConnectionString;
		/// <summary>
		/// Database file location on disk.
		/// </summary>
		private String dbFileLocation;
		
		/// <summary>
		/// Default Constructor
		/// </summary>
		public CharacterDataInterface()
		{
			//Set the file path and test for it's exsistence.
			this.dbFileLocation = System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + "CharSheetData.db";
			if(!System.IO.File.Exists(this.dbFileLocation)){
				try{
					//If it does not exist, then create the file and close it.
					System.IO.File.Create(this.dbFileLocation).Close();
					//Set the connection string.
					this.dbConnectionString=String.Format("Data Source={0};Version=3;New=True;UTF8Encoding=true;compress=true;", this.dbFileLocation);
				}catch (Exception e){
					throw new Exception("Could not create database file.", e);
				}
			}
			//Set the connection string.
			this.dbConnectionString=String.Format("Data Source={0};Version=3;New=True;UTF8Encoding=true;compress=true;", this.dbFileLocation);
			//Call to creation SQL statements.
			this.PopulateSystemTables();
		}
		
		/// <summary>
		/// Constructor, accepting a filename parameter to allow for a diffrent datafile to be used.
		/// </summary>
		/// <param name="filename">File path to desired data file.</param>
		public CharacterDataInterface(String filename)
		{ 
			//Set the file location to the provided file path and test for it's existence.
			this.dbFileLocation = filename;
			if(!System.IO.File.Exists(this.dbFileLocation)){
				try{
					//If it does not exist, create and close the file and set the connection string.
					System.IO.File.Create(this.dbFileLocation).Close();
					this.dbConnectionString=String.Format("Data Source={0};Version=3;New=True;UTF8Encoding=true;compress=true;", this.dbFileLocation);
				}catch (Exception e){
					throw new Exception("Could not create database file.", e);
				}
			}
			//Set the connection string.
			this.dbConnectionString=String.Format("Data Source={0};Version=3;New=True;UTF8Encoding=true;compress=true;", this.dbFileLocation);
			//Call to creation SQL statements.
			this.PopulateSystemTables();
		}
		
		/***************
		 * 
 		 * SELECT 
 		 * 
 		 ***************/
		
		/// <summary>
		/// Provides for getting a selected collection of fields from a specified table as a DataTable
		/// object.
		/// </summary>
		/// <param name="tableName">Name of the table to retrieve fields from.</param>
		/// <param name="fields">The names of the fields to retrieve.</param>
		/// <returns>A DataTable containing the information for the requested table.</returns>
		public DataTable SelectFields(String tableName, String[] fields){
			
			//Throw exception if the table name is empty.
			if(tableName == String.Empty){
				throw new Exception("You must specify a table to select from.");
			}
			
			//Create a data set object
			DataSet retrievedData = new DataSet();
			//Build a SQL query as a formatted string (for protection against bad characters)
			String selectQuery = String.Format("SELECT {0} FROM [{1}]", 
			                                   String.Join(",", fields),
			                                   tableName);
			try{
				using(SQLiteConnection db = new SQLiteConnection(this.dbConnectionString)){
					db.Open();
					//Test if the table exists
					if(this.TableExists(db, tableName)){
						//Use a prepared statement to query th database
						using(SQLiteCommand cmd = new SQLiteCommand(selectQuery, db)){
							cmd.Prepare();
							//Use the Dataset object pipe data into an adapter
							SQLiteDataAdapter quiriedTable = new SQLiteDataAdapter(cmd);
							quiriedTable.Fill(retrievedData);
						}
					}else{
						throw new Exception("The table "+tableName+" does not exist.");
					}
					db.Close();
				}
				//If tables are returned, return the table.
				if(retrievedData.Tables.Count>0){
					return retrievedData.Tables[0];
				}
				//If no tables are returned, create an empty table and return it.
				DataTable dummyTable = new DataTable(tableName);
				for(int i=0; i<fields.Length; i++){
					dummyTable.Columns.Add(fields[i].ToString());
				}
				return dummyTable;
			}catch(Exception e){
				throw new Exception("Could not retrieve from table "+tableName, e);
			}
			                                   
		}
		
		/// <summary>
		/// Provides a method for selecting specific records based on the value of a key field.
		/// </summary>
		/// <param name="tableName">Name of the table to select from</param>
		/// <param name="fields">Fields to return</param>
		/// <param name="key">Index of the key field</param>
		/// <param name="search">Value to select by</param>
		/// <returns>Selected rows as a DataTable</returns>
		public DataTable SelectFieldsByKey(String tableName, String[] fields, String key, String search){
			
			//If either table name or key are empty, throw an exception.
			if(tableName == String.Empty){
				throw new Exception("You must specify a table to select from.");
			}
			if(key == String.Empty){
				throw new Exception("You must specify a key to select by.");
			}
			
			//Set up a dataset object
			DataSet retrievedData = new DataSet();
			//Build the SQL query
			String selectQuery = String.Format("SELECT {0} FROM [{1}] WHERE {2}='{3}'", 
			                                   String.Join(",", fields),
			                                   tableName,
			                                   key,
			                                   search);
			try{
				using(SQLiteConnection db = new SQLiteConnection(this.dbConnectionString)){
					db.Open();
					//Test if the table exists.
					if(this.TableExists(db, tableName)){
						//Use prepared statement to query the database.
						using(SQLiteCommand cmd = new SQLiteCommand(selectQuery, db)){
							cmd.Prepare();
							
							//Use the data set to populate the dataadapter
							SQLiteDataAdapter quiriedTable = new SQLiteDataAdapter(cmd);
							quiriedTable.Fill(retrievedData);
						}
					}else{
						throw new Exception("The table "+tableName+" does not exist.");
					}
					db.Close();
				}
				//If tables were returned, return the table
				if(retrievedData.Tables.Count>0){
					return retrievedData.Tables[0];
				}
				
				//If no tables were returned, build a dummy table.
				DataTable dummyTable = new DataTable(tableName);
				for(int i=0; i<fields.Length; i++){
					dummyTable.Columns.Add(fields[i].ToString());
				}
				return dummyTable;
			}catch(Exception e){
				throw new Exception("Could not retrieve from table "+tableName, e);
			}
		}
		
		/// <summary>
		/// Returns the content of the BLOB field defined by the field parameter, selecting the row by the primary key parameter.
		/// </summary>
		/// <param name="tableName">The name of the table to select from</param>
		/// <param name="field">The BLOB field</param>
		/// <param name="primaryKey">The key to select the record by</param>
		/// <param name="search">The value of the key to search for</param>
		/// <returns></returns>
		public byte[] SelectBlobFieldByKey(String tableName, String field, String primaryKey, String search) {
			
			//Ensure tablename, primarykey, and field are not empty.
			if(tableName == String.Empty){
				throw new Exception("You must specify a table to select from.");
			}
			if(primaryKey == String.Empty){
				throw new Exception("You must specify a key to select with.");
			}
			if(field == String.Empty) {
				throw new Exception("You must specify a field to return.");
			}
			
			try{
				using(SQLiteConnection db = new SQLiteConnection(this.dbConnectionString)){
					db.Open();
					//Setup the query and a variable to hold the result.
					byte[] dataValue;
					String selectQuery = String.Format("SELECT {0} FROM [{1}] WHERE {2} = '{3}'", 
								                               field, tableName, primaryKey, search);
					using(SQLiteCommand cmd = new SQLiteCommand()){
						cmd.CommandText = selectQuery;
						cmd.Connection= db;
						cmd.Prepare();
						//Execute teh command a a scalar to return the resultant field as an object.
						object result = cmd.ExecuteScalar();
						//If the result is a 'null', return anempty byte array
						if (result == DBNull.Value) {
							dataValue = new Byte[0];
						} else {
							//Return the result 
							dataValue = (byte[]) result;	
						}
					}
					//Close the database and return the value.
					db.Close();
					return dataValue;
				}
			}catch(Exception e){
				throw new Exception("Could not retrieve from table "+tableName, e);
			}					
		}
		
		/// <summary>
		/// Provides a method for selecting specific records based on the value of a key field.
		/// </summary>
		/// <param name="tableName">Name of the table to select from</param>
		/// <param name="fields">Fields to return</param>
		/// <param name="key">Index of the key field</param>
		/// <param name="search">Value to select by</param>
		/// <returns>Selected rows as a DataTable</returns>
		public DataTable SelectRecordsByKey(String tableName, String key, String search){
			
			//throw exceptions of tablename or key are empty.
			if(tableName == String.Empty){
				throw new Exception("You must specify a table to select from.");
			}
			if(key == String.Empty){
				throw new Exception("You must specify a key to select with.");
			}
			
			try{
				
				DataSet retrievedData;
				String[] fields;
				using(SQLiteConnection db = new SQLiteConnection(this.dbConnectionString)){
					db.Open();
					//Setup a dataset and get hte fields for the requested table from the database.
					retrievedData = new DataSet();
					fields = this.TableColumns(db, tableName);
					String selectQuery = String.Format("SELECT {0} FROM [{1}] WHERE {2}=\"{3}\"", 
								                                   String.Join(",", fields),
								                                   tableName,
								                                   key,
								                                   search);
					//If the table exists, use the data set to fill the data adapter.
					if(this.TableExists(db, tableName)){
						using(SQLiteCommand cmd = new SQLiteCommand(selectQuery, db)){
							cmd.Prepare();
							SQLiteDataAdapter quiriedTable = new SQLiteDataAdapter(cmd);
							quiriedTable.Fill(retrievedData);
						}
					}else{
						throw new Exception("The table "+tableName+" does not exist.");
					}
					db.Close();
				}
				//Return the table that was retrieved.
				if (retrievedData.Tables.Count > 0) {
					return retrievedData.Tables[0];
				}
				
				//If there was no table, build a dummy and return it.
				DataTable dummyTable = new DataTable(tableName);
				for(int i=0; i<fields.Length; i++){
					dummyTable.Columns.Add(fields[i].ToString());
				}
				return dummyTable;
			}catch(Exception e){
				throw new Exception("Could not retrieve from table "+tableName, e);
			}
		}
		
		/// <summary>
		/// Retrieves the requested table from the database as a DataTable collection. 
		/// </summary>
		/// <param name="tableName">Name of the desired table</param>
		/// <returns>The requested table as a DataTable</returns>
		public DataTable GetTable(String tableName){
			try{
				DataTable retrievedTable = null;
				using(SQLiteConnection db = new SQLiteConnection(this.dbConnectionString)){
					db.Open();
					DataSet retrievedData = new DataSet();
					//Check that the table exists...because typos and drunk coding happen
					String query = String.Format("SELECT * FROM [{0}] WHERE 1", tableName);
					if(this.TableExists(db, tableName)){
						using(SQLiteCommand cmd = new SQLiteCommand(query, db)){
							SQLiteDataAdapter queriedData = new SQLiteDataAdapter(cmd);
							queriedData.Fill(retrievedData);
							retrievedTable = retrievedData.Tables[0];
						}
					}
					retrievedData.Dispose();
					db.Close();
				}
				return retrievedTable;
			}catch(Exception e){
				throw new Exception("Cannot get table "+tableName, e);
			}
		}

		/***************
		 * 
 		 * INSERT 
 		 * 
 		 ***************/
		
		/// <summary>
		/// Provides the ability to insert a single row into the desired table using arrays of keys/values.
		/// </summary>
		/// <param name="tableName">Name of the table to insert into</param>
		/// <param name="keys">Fields to insert</param>
		/// <param name="values">Field values to insert</param>
		/// <returns>Integer row id</returns>
		public int InsertRecord(String tableName, String[] keys, String[] values){
			//If the number of keys do not match the number of values
			//fail because the idea is just dumb.
			if(keys.Length != values.Length){
				return -1;
			}
			//If the name of the table is empty, fail.
			if(tableName == String.Empty){
				return -1;
			}
			
			try{
				using(SQLiteConnection db = new SQLiteConnection(this.dbConnectionString)){
					db.Open();
					int id = -1;
					// Prepare the INSERT query with the table name and keys
					String insertQuery = String.Format("INSERT INTO [{0}]({1}) VALUES(", tableName, String.Join(",", keys));
					
					// Prepair the placeholders for the parameterized query
					String[] valuePlaceholders = new String[values.Length];
					for(int i=0; i< values.Length; i++){
						valuePlaceholders[i] = "@"+i;
					}
					insertQuery += String.Join(",", valuePlaceholders) +")";
					//Create a parameterized command 
					using(SQLiteCommand cmd = new SQLiteCommand()){
						cmd.Connection = db;
						cmd.CommandType = CommandType.Text;
						cmd.CommandText = insertQuery;
						//Set each of the parameters 
						for(int j=0; j<keys.Length; j++){
							cmd.Parameters.AddWithValue(valuePlaceholders[j], values[j]);
						}
						cmd.Prepare();
						if(cmd.ExecuteNonQuery() > 0){
							id = this.GetLastInsertId(db);
							db.Close();
							return id;
						}
					}
					db.Close();
					return id;
				}
			}catch(Exception e){
				e.ToString();
				//Something broke...fail. D:
				return -1;
			}
		}
		
		/// <summary>
		/// Provides the ability to insert a single record into the desired table. This function is a 
		/// wrapper for insertRecord(String, String[], String[]) to accept Dictionary collection types.
		/// </summary>
		/// <param name="tableName">Name of the table to insert into.</param>
		/// <param name="recordInfo">Dictionary collection containing the key/value pairs</param>
		/// <returns>Integer row id</returns>
		public int InsertRecord(String tableName, Dictionary<String, String> recordInfo){
			String[] keys = new String[recordInfo.Keys.Count];
			String[] values = new String[recordInfo.Values.Count];
			int i=0;
			//Get the enumerator for the dictionary.
			IDictionaryEnumerator entries = (IDictionaryEnumerator)recordInfo.GetEnumerator();
			//Iterate over the key/value pairs and populate the key/value arrays.
			while(entries.MoveNext()){
				keys[i] = entries.Key.ToString();
				values[i] = entries.Value.ToString();
				i++;
			}
			//Return the result of the record insert.
			return this.InsertRecord(tableName, keys, values);
		}
		
		/***************
		 * 
 		 * UPDATE 
 		 * 
 		 ***************/
		
		/// <summary>
		/// Provides a method for updating a record in the database
		/// </summary>
		/// <param name="tableName">Name of the table to update</param>
		/// <param name="keys">The fields to update</param>
		/// <param name="values">Values to update</param>
		/// <param name="primaryKey">Key to select records by</param>
		/// <param name="search">Value to select records by</param>
		/// <returns>Boolean success/failure</returns>
		public Boolean UpdateRecord(String tableName, String[] keys, String[] values, String primaryKey, String search){
			//If the string values are empty, fail because they are kinda important
			if(tableName == String.Empty || primaryKey == String.Empty || search == String.Empty){
				return false;
			}
			//If the number of keys don't match the number of values, fail because
			//that's just dumb.
			if(keys.Length != values.Length){
				return false;				
			}
			try{
				using(SQLiteConnection db = new SQLiteConnection(this.dbConnectionString)){
					//Create the query text and placeholders for the parameters
					db.Open();
					String[] setParameterization = new String[keys.Length];
					String updateQuery = "UPDATE ["+tableName+"] SET ";
					//Setup the placeholders with the named parameter placeholders
					for(int i=0; i<keys.Length; i++){
						setParameterization[i] = keys[i]+" = @"+keys[i];
					}
					//Build the query for UPDATE
					updateQuery += String.Join(",", setParameterization)
								+" WHERE "+String.Format("{0} = {1}", primaryKey, search);
					//Setup the command to the database
					using(SQLiteCommand cmd = new SQLiteCommand()){
						cmd.Connection = db;
						cmd.CommandType = CommandType.Text;
						//Set the command
						cmd.CommandText = updateQuery;
						//Input the parameters with the correct key/value pairs
						for(int j=0; j<keys.Length; j++){
							cmd.Parameters.AddWithValue("@"+keys[j], values[j]);
						}
						//Prepare the query for execution
						cmd.Prepare();
						//If we updated one or more rows, the query was successful.
						if(cmd.ExecuteNonQuery() > 0){
							db.Close();
							return true;
						}
					}
					db.Close();
				}
				return false;
			}catch(Exception e){
				e.ToString();
				return false;
			}
		}
		
		/// <summary>
		/// Provides a method for updating a record in the database. 
		/// This method overrides updateRecord to provide an interface 
		/// for updating via a Dictionary collection.
		/// </summary>
		/// <param name="tableName">Name of the table to update</param>
		/// <param name="record">Dictionary collection of key/value pairs</param>
		/// <param name="primaryKey">Key to select records by</param>
		/// <param name="search">Value to search records by</param>
		/// <returns>Boolean success/failure</returns>
		public Boolean UpdateRecord(String tableName, Dictionary<String, String> recordInfo, String primaryKey, String search){
			//Break open the dictionary in to an array for keys and one for values
			String[] keys = new String[recordInfo.Keys.Count];
			String[] values = new String[recordInfo.Values.Count];
			int i=0;
			//Get the enumerator for hte dictionary object.
			IDictionaryEnumerator entries = (IDictionaryEnumerator)recordInfo.GetEnumerator();
			//Iterate over the dictionary and populate the key/value arrays.
			while(entries.MoveNext()){
				keys[i] = entries.Key.ToString();
				values[i] = entries.Value.ToString();
				i++;
			}
			//Pass the key/value arrays to the overloaded function.
			return this.UpdateRecord(tableName, keys, values, primaryKey, search);
		}
		
		/// <summary>
		/// Provides a method for updating a blob field for a record in the database
		/// </summary>
		/// <param name="tableName">Name of the table to update</param>
		/// <param name="key">The field name fo the blob</param>
		/// <param name="value">The byte array value of the blob</param>
		/// <param name="primaryKey">Key to select records by</param>
		/// <param name="search">Value to select records by</param>
		/// <returns>Boolean success/failure</returns>
		public Boolean UpdateRecordBlob(String tableName, String key, byte[] binaryValue, String primaryKey, String search){
			//If the string values are empty, fail because they are kinda important
			if(tableName == String.Empty || primaryKey == String.Empty || search == String.Empty || key == String.Empty){
				return false;
			}
			try{
				using(SQLiteConnection db = new SQLiteConnection(this.dbConnectionString)){
					//Create the query text and placeholders for the parameters
					db.Open();
					String updateQuery = String.Format("UPDATE [{0}] SET {1} = @1 WHERE {2} = '{3}'", tableName, key, primaryKey, search);
					//Setup the command to the database
					using(SQLiteCommand cmd = new SQLiteCommand()){
						cmd.Connection = db;
						cmd.CommandType = CommandType.Text;
						//Set the command
						cmd.CommandText = updateQuery;
						cmd.Prepare();
						//Input the binary content for the BLOB.
						cmd.Parameters.Add("@1", DbType.Binary, binaryValue.Length);
						cmd.Parameters["@1"].Value = binaryValue;
						//If we updated one or more rows, the query was successful.
						if(cmd.ExecuteNonQuery() > 0){
							db.Close();
							return true;
						}
					}
					db.Close();
				}
				return false;
			}catch(Exception e){
				e.ToString();
				return false;
			}
		}
		
		/***************
		 * 
 		 * DELETE 
 		 * 
 		 ***************/
		
		/// <summary>
		/// Provides for deleting a record from the database
		/// </summary>
		/// <returns>Boolean success/failure</returns>
		public Boolean DeleteRecord(String tableName, String key, String search){
			// If the parameters are empty, fail
			if(tableName == String.Empty || key == String.Empty || search == String.Empty){
				return false;
			}
			
			try{
				using(SQLiteConnection db = new SQLiteConnection(this.dbConnectionString)){
					db.Open();
					//Setup the command
					String query = "DELETE FROM [{0}] WHERE {1} = @0";
					//Input the parameters
					query = String.Format(query, tableName, key);
					using(SQLiteCommand cmd = new SQLiteCommand()){
						cmd.Connection = db;
						cmd.CommandType = CommandType.Text;
						cmd.CommandText = query;
						cmd.Parameters.AddWithValue("@0", search);
						//If the query effects more than one row, it's successful.
						if(cmd.ExecuteNonQuery() > 0){
							db.Close();
							return true;
						}
					}
					//Fail
					db.Close();
					return false;
				}
			}catch(Exception e){
				e.ToString();
				return false;
			}
		}
		
		/***************
		 * 
 		 * UTILITES 
 		 * 
 		 ***************/
		
		/// <summary>
		/// Parses the raw String data from a character sheet file.
		/// </summary>
		/// <remarks>
		/// The processing algorithm sucks pretty hard because instead of normalizing the data on write and transforming on read, 
		/// the data was written raw in a CSV-like propretary format, so quote marks broke the original program and the arbitrary \n in the 
		/// middle of a row made parsing a nightmare...hence the stupid 'lets scan all characters!' algorithm. D:
		/// </remarks>
		/// <param name="rawData">The raw file data as a string.</param>
		/// <returns>boolean complete</returns>
		public bool ParseCharSheetData(String rawData) {
			bool openString = false;
			long charCounter = 0;
			String buffer = "";
			List<String> columns = new List<String>();
			List<String[]> rows = new List<String[]>();
			if (rawData.Contains("CharSheet v6.0 File")) {
				//We need to replace the first \r\n for each record with a comma because the %$@# file format is not 'CSV' but something retarded
				Regex conformer = new Regex(@"(?<=\d)\r\n", RegexOptions.Compiled);
				rawData = conformer.Replace(rawData.Substring(rawData.IndexOf('\n')+1), ",");
				
				//Now, we break the file down into individual characters.. x.o
				char[] data = rawData.ToCharArray();
				while(charCounter < data.GetLength(0)) {
					//Get the current character and...
					char currentChar = data[charCounter];
					switch (currentChar) {
						//Handle the Open/Close double quotes. They're 'string delimeters'.
						case '"':
							if (openString) {
								openString = false;
							} else {
								openString = true;
							}
						break;
						//Handle new line characters
						case '\n':
							//If they belong in a string, keep them
							if (openString) {
								buffer += currentChar;	
							} else {
								//Otherwise, they are end of record markers, so we start over with the next record.
								columns.Add(buffer);
								rows.Add(columns.ToArray());
								columns.Clear();
								buffer = "";
							}
						break;
						//Commas separate fields...unless they are in a string field.
						case ',':
							if (openString) {
								buffer += currentChar;
							} else {
								columns.Add(buffer);
								buffer = "";
							}
						break;
						//Otherwise, it's a normal, usable character...so add it to the buffer.
						default:
							if(!openString && currentChar == '\r'){
								//Unless carriage return outside a string. Discard it.
							} else {
								buffer += currentChar;
							}
						break;
						
					}
					//Next %*#$ character
					charCounter++;
				}
				
				/* FUCK! Ok...now we can get some sanity in here. D: */
				
				//Now that all the ducks are in a row
				IEnumerator<String[]> allRows = rows.GetEnumerator();
				//Setup out table keys and placeholders
				String[] charKeys = new String[10] {"name", "callsign", "species", "gender", "age", "affiliation", "experience", "background", "advantages", "notes"};
				String[] attrKeys = new String[4] {"cid", "attribute", "points", "modifier"};
				String[] attrPlaceHolders = new String[4] {"@0", "@1", "@2", "@3"};
				String[] skillKeys = new String[4] {"cid", "skill", "points", "modifier"};
				String[] skillPlaceHolders = new String[4] {"@0", "@1", "@2", "@3"};
				
				//Set up our attribute names
				String[] attrNames = new String[5] {"Strength", "Agility", "Cunning", "Willpower", "Charisma"};
				
				//Process each row
				while (allRows.MoveNext()) {
					//Get the current record.
					String[] record = allRows.Current;
					
					//Insert the character attributes into the table and get the row ID.
					String[] charValues = new String[10] {record[0], record[76], record[74], record[73], record[72], record[75], record[11], record[77], record[78], record[79]};
					int cid = this.InsertRecord("characters", charKeys, charValues);
					
					if(cid != -1){
						int jump = 1;
						//Open a connection to the database
						using (SQLiteConnection db = new SQLiteConnection(this.dbConnectionString)) {
							db.Open();
							//Use a transaction object to bulk import character records.
							using (SQLiteTransaction charDataTransaction = db.BeginTransaction()) {
								
								//Insert command for attributes.
								String attrInsertQuery = String.Format("INSERT INTO [{0}]({1}) VALUES(", "attributes", String.Join(",", attrKeys)) + 
														 String.Join(",", attrPlaceHolders) + ")";
								
								//Iterate over the attributes
								for (int atIndex = 0; atIndex < attrNames.Length; atIndex++) {
									//Build a parameterized command
									using (SQLiteCommand attrCmd = new SQLiteCommand()) {
										attrCmd.CommandType = CommandType.Text;
										attrCmd.CommandText = attrInsertQuery;
										attrCmd.Connection = db;
										
										//Gather the relevant parts.
										String[] attrValues = new String[4] {""+cid, attrNames[atIndex], record[atIndex + jump], record[atIndex + jump + 1]};
										jump++;
										
										//Dump the values into the parameter buckets.
										for(int atvIndex = 0; atvIndex < attrValues.Length; atvIndex++) {
											attrCmd.Parameters.AddWithValue("@"+atvIndex, attrValues[atvIndex]);
										}
										
										//Queue the command in transaction.
										attrCmd.Prepare();
										attrCmd.ExecuteNonQuery();
									}
							
								}
								
								//Insert command for skills
								String skillInsertQuery = String.Format("INSERT INTO [{0}]({1}) VALUES(", "skills", String.Join(",", skillKeys)) + 
														  String.Join(",", skillPlaceHolders) + ")";
								
								//Parse the record for skills
								for (int skIndex = 12; skIndex < 72; skIndex += 3) {
									//But only non-empty skill records
									if (record[skIndex] != "") {
										//Build a command for importing the skills
										using (SQLiteCommand skillCmd = new SQLiteCommand()) {
											skillCmd.CommandType = CommandType.Text;
											skillCmd.CommandText = skillInsertQuery;
											skillCmd.Connection = db;
											
											//Gather the relevant parts.
											String[] skill = new String[4] {""+cid, record[skIndex], record[skIndex + 1], record[skIndex + 2]};
											
											//Dump the values into the parameter buckets
											for(int skvIndex = 0; skvIndex < skill.Length; skvIndex++) {
												skillCmd.Parameters.AddWithValue("@"+skvIndex, skill[skvIndex]);
											}
											
											//Queue the command in the transaction.
											skillCmd.Prepare();
											skillCmd.ExecuteNonQuery();
										}
									}
								}
								//Commit the transaction.
								charDataTransaction.Commit();
							}
							//Close the connection.
							db.Close();
							this.VacuumDatabase();
						}
					} else {
						throw new FileFormatException("Failed importing character " + record[0] + "! Halting...");
					}
				}
				
			} else {
				throw new FileFormatException("The data file is not a CharSheet v6.0 format file!");
			}
	
			return true;
		}
		
		/// <summary>
		/// Provides an interface into SQLite's integity check method so that the database can be 
		/// checked before data is stored or retrieved.
		/// </summary>
		/// <returns>Boolean as to whether or not the database is good.</returns>
		public Boolean IntegrityCheck(){
			try{
				Boolean isGood = false;
				using(SQLiteConnection db = new SQLiteConnection(this.dbConnectionString)){
					db.Open();
					//SQlite has a command to check database integrity. We execute this 
					//query against the database and check the reply...
					String query = "PRAGMA integrity_check";
					using(SQLiteCommand cmd = new SQLiteCommand(query, db)){
						SQLiteDataReader reader = cmd.ExecuteReader();
						if(reader.HasRows){
							//Get the rows back from the database.
							while(reader.Read()){
								//If the database is good, we get a single row of 'ok'
								if(reader.GetValue(0).ToString() == "ok"){
									isGood = true;
								}
								//Otherwise, we get a litany of errors from the database
							}
						}
					}
					db.Close();
				}
				return isGood;
			}catch(Exception e){
				e.ToString();
				return false;
			}
		}
		
		/// <summary>
		/// Provides a method for getting the primary key id of the last inserted record.
		/// </summary>
		/// <returns>int Id or -1 on failure</returns>
		public int GetLastInsertId(SQLiteConnection db ){
			try{
				String query = "SELECT last_insert_rowid()";
				using(SQLiteCommand cmd = new SQLiteCommand(query, db)){
					SQLiteDataReader reader = cmd.ExecuteReader();
					if(reader.HasRows){
						if(reader.Read()){
							return Convert.ToInt32(reader.GetValue(0).ToString());
						}
					}
					return -1;
				}
			}catch(Exception e){
				throw(new Exception("Failed to get last insert id!", e));
			}
		}
		
		/// <summary>
		/// Provides a method for determining if a table exists within the current database
		/// </summary>
		/// <param name="db">The currently open database connection</param>
		/// <param name="tableName">The name of the table to check.</param>
		/// <returns>Boolena whether or not the table exists.</returns>
		private Boolean TableExists(SQLiteConnection db, String tableName){
			try{
				//Setup query to find the table in the sqlite database
				String query = String.Format("SELECT name FROM sqlite_master WHERE name = "
				                             +"\"{0}\" AND type = \"table\"", tableName);
				using(SQLiteCommand cmd = new SQLiteCommand(query, db)){
					cmd.Prepare();
					SQLiteDataReader reader = cmd.ExecuteReader();
					//If there are tables in the returned data, the
					//table in question exists.
					if(reader.HasRows){
						return true;
					}
				}
			} catch(Exception e) {
				throw new Exception("Cannot determine if table " + tableName + " exists!", e);
			}
			//No table. D:
			return false;
		}
		
		/// <summary>
		/// Runs the VACUUM command on the SQLite database to free up space in he data file.
		/// </summary>
		public void VacuumDatabase() {
			try {
				using(SQLiteConnection db = new SQLiteConnection(this.dbConnectionString)) {
					db.Open();
					using(SQLiteCommand cmd = new SQLiteCommand("VACUUM", db)) {
						cmd.ExecuteNonQuery();
						db.Close();
					}
				}
			} catch(Exception e) {
				throw new Exception("Cannot vacuum database!", e);
			}
		}
		
		/// <summary>
		/// Provides a method for getting the column names for a table
		/// </summary>
		/// <param name="db">The currently open database connection</param>
		/// <param name="tableName">Name of the table the interrogate</param>
		/// <returns>String array of column names</returns>
		private String[] TableColumns(SQLiteConnection db, String tableName){
			try{
				List<String> columns = new List<String>();
					//Setup query to find the table in the sqlite database
					String query = String.Format("PRAGMA table_info(\"{0}\")", tableName);
					using(SQLiteCommand cmd = new SQLiteCommand(query, db)){
						cmd.Prepare();
						SQLiteDataReader reader = cmd.ExecuteReader();
						//If there are tables in the returned data, the
						//table in question exists.
						if(reader.HasRows){
							while(reader.Read()) {
								columns.Add(reader.GetString(1));
							}
							return columns.ToArray();
						} else {
							return new String[0];
						}
					}
				} catch(Exception e){
					throw(new Exception("Cannot get info on table "+tableName, e));
				}
		}
		
		/// <summary>
		/// Builds out the database tables required for the program, 
		/// should there be the need to initialize the data file from scratch.
		/// </summary>
		private void PopulateSystemTables() {
			//All of the SQL queries are 'CREATE IF NOT EXISTS', so it does 
			//not hurt the database to run them even if the tables are there.
			String createCharacters = "CREATE TABLE IF NOT EXISTS characters " +
									  "(cid INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name TEXT NULL, " +
									  "callsign TEXT NULL, species TEXT NULL, gender TEXT NULL, height TEXT NULL, " +
									  "weight TEXT NULL, age INTEGER NOT NULL DEFAULT 18, affiliation TEXT NULL, " +
									  "rank TEXT NULL, karma INTEGER NOT NULL DEFAULT 0, experience INTEGER NOT NULL DEFAULT 0, " +
									  "background TEXT NULL, advantages TEXT NULL, inventory TEXT NULL, notes TEXT NULL, picture BLOB NULL, " +
									  "isnpc NUMERIC NOT NULL DEFAULT 0);";
			String createAttributes = "CREATE TABLE IF NOT EXISTS attributes " + 
									  "(aid INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, cid INTEGER NULL, attribute TEXT NULL, points INTEGER NULL NOT NULL DEFAULT 0, " +
									  "modifier INTEGER NOT NULL DEFAULT 0,exempt NUMERIC NOT NULL DEFAULT 0);";
			String createSkills = "CREATE TABLE IF NOT EXISTS skills (sid INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, cid INTEGER NULL, skill TEXT NULL, " + 
								  "points INTEGER NULL NOT NULL DEFAULT 0, modifier INTEGER NOT NULL DEFAULT 0, " +
				                  "exempt NUMERIC NOT NULL DEFAULT 0);";
			using(SQLiteConnection db = new SQLiteConnection(this.dbConnectionString)){
				db.Open();
				using(SQLiteCommand cmd = new SQLiteCommand(createCharacters, db)){
					cmd.ExecuteNonQuery();
				}
				using(SQLiteCommand cmd = new SQLiteCommand(createAttributes, db)){
					cmd.ExecuteNonQuery();
				}
				using(SQLiteCommand cmd = new SQLiteCommand(createSkills, db)){
					cmd.ExecuteNonQuery();
				}
				db.Close();
			}
		}
	}
}

CharSheet V2
============

This program is designed to replace a Gamemaster character management and dice rolling tool developed for the StarFox RPG (SFRPG) 
game by Terrin Fox (David Thurlow: <terrin_fox@hotmail.com>). 

Working with JWolfman (<csunfoxy@gmail.com>) to isolate requirements, some issues were identified:

* The data stored was limited to text only.
* The original application had issues with storing certain characters. This was because the data in the program was stored
in a CSV-like format, making it fragile.
* Editing the fields in the original application required the whole program be put in 'Edit Mode' and the changes saved 
manually.
* Some data fields were obsolite and unused.
* The number of Skill and Attribute fields were static.
* The labels for the Attribute fields were static.

After talking to JWolfman about what we would like to see, I spent some time writing this replacement.

Features
--------
* SQLite data store!
* Supports dice other than D20!
* Prameterized queries and Unicode support for no character limits!
* Auto prompt to save changes!
* Upload character images into the character profile (with built-in preview)!
* Attributes and Skills are extensible and flexible between characters.
* Multi-character versus Skill/Attribute Rolls!
* Export selected character sheets as text files or separate database file.
* Fate's Hand: Program randomly selects a character on a configurable interval and calls them out to the game master for evil shenanigans!
* Application configuration lives with character data so game settings follows the game.
* Import for older CharSheet 6 data files

Running the program
-------------------

To run the program, we only need the three files in the same folder:
* CharSheetV2.exe
* System.Data.SQLite.dll (from the DataLayer folder)
* CharSheetData.db

The application is portable and requires .NET 4.0 or greater to run.

Tip Jar
-------------------

If you think it's worthwhile, toss some coffee money my way [here](https://paypal.me/whitefoxstudios). I would appreciate it. ^.^
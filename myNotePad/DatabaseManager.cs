using System;
using System.IO;
using System.Collections.Generic;


namespace myNotePad
{
	public class DatabaseManager
	{
		static string dbName = "NeatNotes.sqlite";
		string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);

		public DatabaseManager()
		{
		}
			
		public List<Notes> ViewAllNotes ()
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)){
					var cmd = new SQLite.SQLiteCommand(conn);
					cmd.CommandText = "Select * from tblNeatNotes";
					var NoteList = cmd.ExecuteQuery<Notes>();
					return NoteList;
				}
			} catch (Exception e) 
			{
				Console.WriteLine ("Error:" + e.Message);
				return null;
				}
		}

        public List<Notes> SearchNotes(string searchtext)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    cmd.CommandText = "Select * from tblNeatNotes where Title like '%" + searchtext + "%'";
                    var NoteList = cmd.ExecuteQuery<Notes>();
                    return NoteList;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return null;
            }
        }

        public void AddNote(string title, string details)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection (dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    cmd.CommandText = "insert into tblNeatNotes(Title,NoteDetails) values('" + title + "','" + details + "')";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
        }

        public void EditItem(string title, string details, int noteid)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    cmd.CommandText = "update tblNeatNotes set Title='" + title + "', NoteDetails='" + details + "' where NoteId=" + noteid;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error:" + ex.Message);
            }
        }

        public void DeleteItem(int noteid)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    cmd.CommandText = "delete from tblNeatNotes where NoteId = " + noteid;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }

		}
	}


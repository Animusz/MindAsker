using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.IO;

namespace MindAsker
{
    class DatabaseManager : Database
    {

        private SQLiteConnection dbConnection;
        //private const string fileName = "MindAskerDatabase.sqlite";
        private const string fileName = "MindAskerDatabase.db3";

        public DatabaseManager()
        {
            try
            {
                bool exist = File.Exists(fileName);

                if (!exist)
                    SQLiteConnection.CreateFile(fileName);

                this.dbConnection = new SQLiteConnection("Data Source="+fileName+";Version=3;");

                if (!exist)
                {
                    // Create table
                    this.connect();
                    MindAsker.Table table = new Event();
                    SQLiteCommand command = new SQLiteCommand(table.createTableTemplateSQL(), this.dbConnection);
                    command.ExecuteNonQuery();
                    this.disconnect();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool connect()
        {
            try
            {
                this.dbConnection.Open();
            } 
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return true;
        }

        public bool disconnect()
        {
            try
            {
                this.dbConnection.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return true;
        }

        public long save(Record rec)
        {
            long saveId = 0;
            try
            {
                SQLiteCommand command = new SQLiteCommand(rec.saveTemplateSQL(), this.dbConnection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand(this.dbConnection);
                command.CommandText = "select last_insert_rowid()";
                saveId = (long)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return saveId;
        }

        public Event edit(Record rec)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(this.dbConnection);
                command.CommandText = rec.existTemplateSQL();
                long number = (long)command.ExecuteScalar();

                if (number > 0)
                {
                    command = new SQLiteCommand(rec.editTemplateSQL(), this.dbConnection);
                    command.ExecuteNonQuery();
                }
                else
                {
                    throw new Exception("Record doesn't exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return (Event)rec;
        }

        public List<Event> find(Record rec, bool bState)
        {
            List<Event> eventsList = new List<Event>();

            try
            {
                SQLiteCommand command = new SQLiteCommand(rec.findTemplateSQL(bState), this.dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Event e = new Event();
                    e.Id = (long)reader["id"];
                    e.Title = (string)reader["title"];
                    e.Desc = (string)reader["desc"];
                    e.AddDate = (DateTime)reader["addDate"];
                    e.ReminderDate = (DateTime)reader["reminderDate"];
                    e.State = (State)((long)reader["state"]);

                    eventsList.Add(e);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return eventsList;
        }

        public bool remove(Record rec)
        {
            try
            {

                SQLiteCommand command = new SQLiteCommand(this.dbConnection);
                command.CommandText = rec.existTemplateSQL();
                long number = (long)command.ExecuteScalar();

                if (number > 0)
                {
                    command = new SQLiteCommand(rec.removeTemplateSQL(), this.dbConnection);
                    command.ExecuteNonQuery();
                }
                else
                {
                    throw new Exception("Record doesn't exist");
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return true;
        }
    }
}

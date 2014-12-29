using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindAsker
{
    class Event : Table, Record
    {
        private long id;
        private string title;
        private string desc;
        private DateTime? addDate;
        private DateTime? reminderDate;
        private MindAsker.State state;

        private static string tableName = "events";

        public Event()
        {
            id = 0;
            title = "";
            desc = "";
            addDate = null;
            reminderDate = null;
            state = MindAsker.State.MIND;
        }

        // Getters and Setters
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Title
        {
            get { return title; }
            set { title = value; }
        }

        public String Desc
        {
            get { return desc; }
            set { desc = value; }
        }
        
        public DateTime? AddDate
        {
            get { return addDate; }
            set { addDate = value; }
        }
        public DateTime? ReminderDate
        {
            get { return reminderDate; }
            set { reminderDate = value; }
        }

        internal MindAsker.State State
        {
            get { return state; }
            set { state = value; }
        }


        public string saveTemplateSQL()
        {
            string sqlStatement = "";
            string values = "";

            if (this.addDate == null)
                this.addDate = DateTime.Now;

            if (this.reminderDate == null)
                this.reminderDate = DateTime.MinValue;

            values += string.IsNullOrEmpty(this.title) ? "' ', " : ("'" + this.title + "', ");
            values += string.IsNullOrEmpty(this.desc) ? "' ', " : ("'" + this.desc + "', ");
            values += "'" + this.addDate + "', ";
            values += "'" + this.reminderDate + "', ";
            values += "" + (int)this.state + " ";

            sqlStatement += "INSERT INTO " + Event.tableName + " ";
            sqlStatement += "(title, desc, addDate, reminderDate, state) VALUES ";
            sqlStatement += "(" + values + ");";

            return sqlStatement;
        }

        public string editTemplateSQL()
        {
            string sqlStatement = "";
            string setValues = "";

            setValues += "title = '" + this.title + "', ";
            setValues += "desc = '" + this.desc + "', ";
            //setValues += "addDate = '" + this.addDate + "', ";
            setValues += "reminderDate = '" + (this.reminderDate == null ? DateTime.MinValue.ToString() : this.reminderDate.ToString()) + "', ";
            setValues += "state = " + (int)this.state + " ";

            sqlStatement += "UPDATE " + Event.tableName + " ";
            sqlStatement += "SET " + setValues;
            sqlStatement += "WHERE id =  " + this.id + ";";

            return sqlStatement;
        }

        public string findTemplateSQL(bool bState)
        {
            string sqlStatement = "";
            string setValues = "";

            bool prevValue = false;

            if (!string.IsNullOrEmpty(this.title))
            {
                setValues += "title = '" + this.title + "' ";
                prevValue = true;
            }
            if (!string.IsNullOrEmpty(this.desc))
            {
                if (prevValue) 
                    setValues += "AND ";
                setValues += "desc = '" + this.desc + "' ";
                prevValue = true;
            }
            if (this.addDate != null)
            {
                if (prevValue)
                    setValues += "AND ";
                setValues += "addDate = '" + this.addDate + "' ";
                prevValue = true;
            }
            if (this.reminderDate != null)
            {
                if (prevValue)
                    setValues += "AND ";
                setValues += "reminderDate = '" + this.reminderDate + "' ";
                prevValue = true;
            }
            if (id != 0)
            {
                if (prevValue)
                    setValues += "AND ";
                setValues += "id = " + this.id + " ";
                prevValue = true;
            }
            if (bState)
            {
                if (prevValue)
                    setValues += "AND ";
                setValues += "state = " + (int)this.state + " ";
            }

            if (!bState && !prevValue)
            {
                sqlStatement += "SELECT * FROM " + Event.tableName + "; ";
            }
            else
            {
                sqlStatement += "SELECT * FROM " + Event.tableName + " ";
                sqlStatement += "WHERE " + setValues + ";";
            }

            //Console.WriteLine(sqlStatement);
            return sqlStatement;
        }

        public string removeTemplateSQL()
        {
            string sqlStatement = "";

            sqlStatement += "DELETE FROM " + Event.tableName + " ";
            sqlStatement += "WHERE id =  " + this.id + ";";

            return sqlStatement;
        }


        public string existTemplateSQL()
        {
            string sqlStatement = "";

            sqlStatement += "SELECT COUNT(*) FROM " + Event.tableName + " ";
            sqlStatement += "WHERE id =  " + this.id + ";";

            return sqlStatement;
        }

        public string createTableTemplateSQL()
        {
            string sqlStatement = "";
            string tableFields = "";

            tableFields += "id INTEGER PRIMARY KEY AUTOINCREMENT, ";
            tableFields += "title TEXT,";
            tableFields += "desc TEXT, ";
            tableFields += "addDate DATETIME, ";
            tableFields += "reminderDate DATETIME, ";
            tableFields += "state INTEGER NOT NULL DEFAULT '" + (int)MindAsker.State.MIND + "' ";

            //sqlStatement += "CREATE TABLE IF NOT EXISTS `" + Event.tableName + "` ";
            sqlStatement += "CREATE TABLE " + Event.tableName + " ";
            sqlStatement += "(" + tableFields + ");";

            return sqlStatement;
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;

namespace ComponentsGame.Records
{
    public class RecordReader
    {
        private readonly string datapath = "Database.db";
        private DataSet data = new DataSet();
        public DataSet Data { get => data; }
        public RecordReader() {
            string query = "SELECT PLAYER.NAME AS NAME, SCOREBOARD.SCORE, SCOREBOARD.MODEID " +
                "FROM PLAYER INNER JOIN SCOREBOARD ON PLAYER.ID = SCOREBOARD.PLAYERID" +
                " WHERE MODEID = 1;";
            //string query = $"SELECT ID, NAME AS 'STT', 'TEN' FROM PLAYER";
            data = GetRecord(query);
        }

        DataSet GetRecord(string query)
        {
            string connectionString = $"Data Source={datapath};Version=3;";

            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Data");
            connection.Close();
            data = dataSet;
            return dataSet;
        }
        public DataSet GetRecordAll()
        {
            string query = "SELECT PLAYER.NAME AS NAME, SCOREBOARD.SCORE, MODE.NAME AS MODE" +
                " FROM PLAYER INNER JOIN SCOREBOARD ON PLAYER.ID = SCOREBOARD.PLAYERID" +
                " INNER JOIN MODE ON MODE.ID = SCOREBOARD.MODEID" ;
            return GetRecord(query);
        }
        public DataSet GetRecordPlayer()
        {
            string query = "SELECT * FROM PLAYER;";
            return GetRecord(query);
        }

        public DataSet GetRecordMode(int numb)
        {
            try
            {
                string query = $"SELECT PLAYER.NAME AS NAME, SCOREBOARD.SCORE FROM PLAYER INNER JOIN SCOREBOARD ON PLAYER.ID = SCOREBOARD.PLAYERID WHERE MODEID = {numb};";
                return GetRecord(query);
            }
            catch (Exception e)
            {
                throw new Exception("Mode isnt exist" + '\n' + e.ToString());
            }
        }
    }
}

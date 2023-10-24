using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ComponentsGame.Records
{
    public class RecordWriter
    {
        string datapath = "Database.db";
        string[] modeName = { "classic", "timer", "matrix" };
        int id_user;
        public RecordWriter()
        {
            if (!File.Exists(datapath))
            {
                SQLiteConnection.CreateFile(datapath);
                using (SQLiteConnection connect = new SQLiteConnection($"Data Source={datapath}; Version=3"))
                {
                    connect.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connect))
                    {
                        command.CommandText = "CREATE TABLE IF NOT EXISTS PLAYER (ID TINYINT PRIMARY KEY, NAME VARCHAR(16));";
                        command.ExecuteNonQuery();

                        command.CommandText = "CREATE TABLE IF NOT EXISTS MODE (ID TINYINT PRIMARY KEY, NAME VARCHAR(10));";
                        command.ExecuteNonQuery();

                        //ADD MODES
                        for (int i = 0; i < modeName.Length; i++)
                        {
                            command.CommandText = $"INSERT OR IGNORE INTO MODE (ID, NAME) VALUES ({i}, '{modeName[i]}')"; //SQLite Language
                            command.ExecuteNonQuery();
                        }

                        command.CommandText = "CREATE TABLE IF NOT EXISTS SCOREBOARD " +
                                            "(PLAYERID TINYINT, SCORE MEDIUMINT, MODEID TINYINT, " +
                                            "FOREIGN KEY (PLAYERID) REFERENCES PLAYER(ID), " +
                                            "FOREIGN KEY (MODEID) REFERENCES MODE(ID), " +
                                            "PRIMARY KEY (PLAYERID, SCORE, MODEID));";
                        command.ExecuteNonQuery();
                    }
                }
            }
            
            using (SQLiteConnection connect = new SQLiteConnection($"Data Source={datapath}; Version=3"))
            {
                connect.Open();
                using (SQLiteCommand command = new SQLiteCommand(connect))
                {
                    command.CommandText = $"SELECT COUNT(*) FROM PLAYER;";
                    id_user = Convert.ToInt32(command.ExecuteScalar());
                }

            }
        }

        private void AddMode()
        {
            using (SQLiteConnection connect = new SQLiteConnection($"Data Source={datapath}; Version=3"))
            {
                connect.Open();
                using (SQLiteCommand command = new SQLiteCommand(connect))
                {
                    for (int i = 0; i < modeName.Length; i++) 
                    {
                        command.CommandText = $"INSERT OR IGNORE INTO MODE (ID, NAME) VALUES ({i}, '{modeName[i]}')";
                        command.ExecuteNonQuery();
                    }

                }

            }
        }

        public int AddUser(string username)
        {
            if (username.Length > 16)
                return -1;
            using (SQLiteConnection connect = new SQLiteConnection($"Data Source={datapath}; Version=3"))
            {
                connect.Open();
                using (SQLiteCommand command = new SQLiteCommand(connect))
                {
                    try
                    {
                        command.CommandText = $"INSERT INTO PLAYER (ID, NAME) VALUES ( {id_user++}, '{username}')";
                        command.ExecuteNonQuery();
                    }
                    catch 
                    {
                        return 0;
                    }
                }
            }
            return 1;
        }

        public int SaveScore(Record player)
        {
            try
            {
                using (SQLiteConnection connect = new SQLiteConnection($"Data Source={datapath}; Version=3"))
                {
                        connect.Open();
                        using (SQLiteCommand cmd = new SQLiteCommand(connect))
                        {
                            cmd.CommandText = $"INSERT INTO SCOREBOARD (PLAYERID, SCORE, MODEID) VALUES ({player.ID}, {player.Score}, {player.ID_MODE})";
                            cmd.ExecuteNonQuery();
                        }
                    }
            } 
            catch
            {
                return 0;
            }
            return 1;
        }
    }
}

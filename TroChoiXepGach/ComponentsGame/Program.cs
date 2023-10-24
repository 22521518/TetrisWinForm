using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentsGame.Records;
using System.Data.SQLite;

namespace ComponentsGame
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string datapath = "Database.db";
            if (!File.Exists(datapath))
                SQLiteConnection.CreateFile(datapath);
            using (SQLiteConnection con = new SQLiteConnection($"Data Source={datapath}; Version=3"))
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS PLAYER (ID TINYINT PRIMARY KEY, NAME VARCHAR(16));";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO PLAYER (ID, NAME) VALUES (3, 'aloalo')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT * FROM PLAYER";
                    Console.WriteLine(cmd.CommandText);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($" {reader.GetInt32(0)}  {reader.GetString(1)}");
                        }
                    }
                }
            }
        }
    }
}

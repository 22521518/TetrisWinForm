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
            //basic code for database
            /*string datapath = "database.db";
            if (!File.Exists(datapath))
                SQLiteConnection.CreateFile(datapath);
            using (SQLiteConnection con = new SQLiteConnection($"data source={datapath}; version=3"))
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "create table if not exists player (id tinyint primary key, name varchar(16));";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "insert into player (id, name) values (3, 'aloalo')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select * from player";
                    Console.WriteLine(cmd.CommandText);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($" {reader.GetInt32(0)}  {reader.GetString(1)}");
                        }
                    }
                }
            } */
        }
    }
}

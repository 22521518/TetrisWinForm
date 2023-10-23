using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentsGame.Records
{
    public class RecordWriter
    {
        string filePath = "./database/scores.txt";
        public void SaveScore(string name, long score, string date)
        {
            try
            {
            using (var streamWriter = new StreamWriter(filePath,true))
                    streamWriter.WriteLine($"{NameValidation(name)} : {score} : {date}");
            } catch (Exception e)
            {
                throw e; 
            }
        }

        private string NameValidation(string name)
        {
            name = name.Trim();
            name = name.Replace(":", "");
            if (name.Length > 16)
                throw new Exception("Invalid Name");
            return name;

        }
    }
}

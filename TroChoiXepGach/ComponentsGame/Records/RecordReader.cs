using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace ComponentsGame.Records
{
    public class RecordReader
    {
        private readonly string filePath = "./database/scores.txt";
        List<Record> listOfRecords = new List<Record>();
        public RecordReader() {
            UpdateRecord();
        }

        public void UpdateRecord(bool sorted = true)
        {
            listOfRecords = ReadFile(sorted);
        }

        public void print()
        {
            foreach (var record in listOfRecords)
                Console.WriteLine(record.ToString());
        }

        private List<Record> ReadFile(bool sorted = false)
        {
            var tmp = new List<Record>();
            try
            {
                var lineFromFile = File.ReadLines(filePath);
                foreach (string line in lineFromFile)
                {
                    Record data = SplitData(line);
                    tmp.Add(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ReadFile() gets bugs with {filePath}");
                Console.WriteLine(e.Message);
            }
            return sorted ? tmp.OrderByDescending(row => row.Score).ToList() : tmp;
        }
        private Record SplitData(string line)
        {
            List<string> data = line.Trim().Split(':').ToList();

            string name = data[0];
            long score = long.TryParse(data[1], out var t_score) ? t_score : 0; 
            //string date = data[2];
            return new Record(name, score);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentsGame.Records
{
    internal class Record
    {
        public string Name { set; get; } 
        public long Score { set; get; }
        public string Date { set; get; }

        public Record(string n = "anonymous", long s = 0, string d = "null")
        {
            Score = s;
            Name = n;
            Date = d;
        }

        public override string ToString()
        {
            return $"{Name.PadRight(15)} : {Score.ToString().PadRight(5)} : {Date}";
        }
    }
}

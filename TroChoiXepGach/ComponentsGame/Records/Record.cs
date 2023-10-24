using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ComponentsGame.Records
{
    public class Record
    {
        public string Name { set; get; } 
        public long Score { set; get; }
        public int ID { set; get; }
        public int ID_MODE { set; get; }

        public Record(string n = "anonymous", long s = 0, int d = 0)
        {
            Score = s;
            Name = n;
            ID = d;
        }

        public override string ToString()
        {
            return $"{Name.PadRight(15)} : {Score.ToString().PadRight(5)} : {ID}";
        }
    }
}

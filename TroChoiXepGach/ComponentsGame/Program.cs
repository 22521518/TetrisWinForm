using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentsGame.Records;

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
                RecordReader recordReader = new RecordReader();
            recordReader.UpdateRecord(false);
            recordReader.print();
        }
    }
}

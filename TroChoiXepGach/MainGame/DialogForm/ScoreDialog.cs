using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentsGame.Records;

namespace MainGame.DialogForm
{
    public partial class ScoreDialog : Form
    {
        string datapath = "Database.db";
        RecordReader recordReader = new RecordReader();
        RecordWriter recordWriter = new RecordWriter();

        public ScoreDialog()
        {
            InitializeComponent();
        }

        private MenuGame mainmenu = null;
        public ScoreDialog(MenuGame form)
        {
            mainmenu = form;
            InitializeComponent();
        }

        private void ScoreDialog_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = recordReader.Data.Tables["Data"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            recordReader.GetRecordMode(1);
            dataGridView1.DataSource = recordReader.Data.Tables["Data"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            recordReader.GetRecordAll();
            dataGridView1.DataSource = recordReader.Data.Tables["Data"];
        }
    }

}

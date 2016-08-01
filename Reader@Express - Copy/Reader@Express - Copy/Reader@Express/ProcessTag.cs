using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Reader_Express
{
    public partial class ProcessTag : Form
    {
        public delegate void sendDataWrite(String data);
        sendDataWrite SendDataWrite;
        public ProcessTag()
        {
            InitializeComponent();
        }
        private void btnWriteClick(object sender, EventArgs e)
        {
            mainForm MainFrom = new mainForm();
            SendDataWrite = new sendDataWrite(MainFrom.getDataWrite);
            string text = textWrite.Text;
            SendDataWrite(text);
            this.Close();
        }
        private void ProcessTag_Load(object sender, EventArgs e)
        {

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textWrite_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

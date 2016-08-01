using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using idro.reader.api;
using idro.controls;

namespace Reader_Express
{
    public partial class Configures : Form
    {
        private Reader reader = null;
        public delegate void sendDataConfig(int power, string st2, string st3, string st4);
        sendDataConfig SendDataConfig;
        public Configures()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            mainForm mainform = new mainForm();
            SendDataConfig = new sendDataConfig(mainform.getDataConfig);
            ReportingType none = ReportingType.None;
            if (this.chbRssi.Checked)
            {
                none |= ReportingType.RSSI;
            }
            if (!this.chbChecksum.Checked)
            {
                none |= ReportingType.Checksum;
            }
            if (this.chbRealtime.Checked)
            {
                none |= ReportingType.ReadTime;
            }
          //  this.reader.SetReportingType(none);
            if (this.chbBeep.Checked)
            {
                reader.SetBuzzer(true);
            }
            if (this.chbBeep.Checked == false)
            {
                reader.SetBuzzer(false);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

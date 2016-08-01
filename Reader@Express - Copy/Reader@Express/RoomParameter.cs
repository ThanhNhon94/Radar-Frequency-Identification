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
    public partial class RoomParameter : Form
    {
        public RoomParameter()
        {
            InitializeComponent();
        }
        public delegate void sendData(string st1, string st2, string st3, string st4);
        sendData SendData;
        private void btnOK_Click(object sender, EventArgs e)
        {
            Map map = new Map();
            string st1_, st2_, st3_, st4_;
            st1_ = cbbWidth.Text;
            st2_ = cbbHeight.Text;
            st3_ = cbbDeep.Text;
            st4_ = cbbRatio.Text;
            SendData = new sendData(map.getData);
            if ((chb2D.Checked) && (!chb3D.Checked))
            {
                SendData(st1_, st2_, "", "");
                this.Close();
            }
            else if ((!chb2D.Checked) && (chb3D.Checked))
            {
                SendData(st1_, st2_, st3_, st4_);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please, choice 2D or 3D", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}

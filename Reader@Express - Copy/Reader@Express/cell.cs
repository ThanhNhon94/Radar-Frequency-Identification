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
    public partial class cell : Form
    {
        public delegate void sendCell(string cell);
        sendCell SendCell;
        public cell()
        {
            InitializeComponent();
        }

        private void btnCell_click(object sender, EventArgs e)
        {
            mainForm mainF = new mainForm();
            string cell;
            cell = cbCell.Text;
            SendCell = new sendCell(mainF.getCell);
            SendCell(cell);
            this.Hide();
        }

        private void cbCell_SelectedIndexChanged(object sender, EventArgs e)
        {

        }   
    }
}

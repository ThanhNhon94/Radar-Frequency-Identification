using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Timers;
using idro.reader.api;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

using System.IO;
using System.IO.Ports;
using idro.controls;
using idro.profile;
using idro.controls.panel;
using MySql.Data.MySqlClient;

namespace Reader_Express
{
    public partial class mainForm : Form
    {
        private static uint totalReadCounts1 = 0;
        private static uint totalReadCounts2 = 0;
        private static uint totalReadCounts3 = 0;
        private static uint totalReadCounts4 = 0;
        private uint totalRead;
        private string num5, text;
        private int[] arrRssi = new int[20];
        static System.Windows.Forms.Timer traningTimer = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer inventoryTimer = new System.Windows.Forms.Timer();
        private static int timeDelay = 20000;
        private static int inventoryTimeDelay = 11000;
        private static int timeDelay_ = 6000;
        private static ArrayList arrRssi1 = new ArrayList();
        private static ArrayList arrRssi2 = new ArrayList();
        private static ArrayList arrRssi3 = new ArrayList();
        private static ArrayList arrRssi4 = new ArrayList();
        ArrayList tagId = new ArrayList();
        ArrayList tagId1 = new ArrayList();
        List<String> t = new List<string>();
        CipherAlgorithm cipher = new CipherAlgorithm();
        string[] stt = new string[2];
       // private static List<Dictionary<String, Double>> dataTraining = new List<Dictionary<string, double>>();
        private static List<Lut> dataTraining = new List<Lut>();
        private static List<Lut> dataRssi = new List<Lut>();     
        DateTime time;
        private bool trainingFlag = false;
        long tick;
        int count = 0;
        private static Reader reader = null;
        public delegate void sendBox(int box);      
        public mainForm()
        {
            time = DateTime.Now;
            tick =  time.Ticks;
            InitializeComponent();
            reader = new Reader();
            tsFunctions.Visible = true;
            traningTimer.Tick += new EventHandler(trainingTimerEventProcessor);          
            traningTimer.Interval = timeDelay;
            inventoryTimer.Tick += new EventHandler(inventoryTimerEventProcessor);
            inventoryTimer.Interval = inventoryTimeDelay;
        }
        // Init Server localhost, Table database is rssi, Port 3306, Id = root, pass=''
        static String connString = "Server=localhost;Database=rfidreader;Port=3306;User ID=root;Password=";
        // Create a connected database
        //MySqlConnection connect = new MySqlConnection(connString);
        private static MySqlConnection conn = new MySqlConnection(connString);
        // command query
        private static MySqlCommand cmd;
        
        #region P/Invoke...
        [DllImport("User32.dll")]
        static extern Boolean MessageBeep(UInt32 beepType);
        #endregion
        
        #region User Interface...
        //Funtion Open connected from databases localhost
        private static void OpenConnection()
        {
            try
            {
                conn.Open();
                //MessageBox.Show("OK");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        //  close connect
        private static bool CloseConnection()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void Delete()
        {
            string query = "DELETE FROM rfidreader.rfid";
            cmd = new MySqlCommand(query, conn);
            MySqlDataReader MyReader;
            try
            {
                OpenConnection();
                MyReader = cmd.ExecuteReader();
                MessageBox.Show("Delete all Database", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                CloseConnection();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                CloseConnection();
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            tsModelType_Click(tsModelIDRO900F, EventArgs.Empty);
            tsConnectType_Click(tsConnectSerial, EventArgs.Empty);
            tsTagType_Click(tsMi6C1Gen2, EventArgs.Empty);
        }
        public ModelType ModelType
        {
            get
            {
                ModelType modelType = (ModelType)Enum.Parse(typeof(ModelType), tsModelType.Tag.ToString());
                return modelType;
            }
        }
        public ConnectType ConnectType
        {
            get
            {
                ConnectType connectType = tsConnectType.Text.Contains("TCP/IP") ? ConnectType.Tcp : ConnectType.Serial;
                return connectType;
            }

           
        }
        public TagType TagType
        {
            get
            {
                TagType tagType = (TagType)Enum.Parse(typeof(TagType), tsTagType.Tag.ToString());
                return tagType;
            }
        }

        private void tsConnectType_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem nConnect = sender as ToolStripMenuItem;
            tsConnectType.Text = nConnect.Text;
            bool bTcpIp = ConnectType == ConnectType.Tcp;
            tsLbAddress.Text = bTcpIp ? "IP:":"Port:";
            tsCbbAddress.Visible = bTcpIp;
            tsCbbSerial.Visible = !bTcpIp;
        }
        private void tsModelType_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem nItem = sender as ToolStripMenuItem;
            tsModelType.Tag = nItem.Tag;
            tsModelType.Text = nItem.Text;

        }

        private void tsTagType_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem nItem = sender as ToolStripMenuItem;
            tsTagType.Tag = nItem.Tag;
            tsTagType.Text = nItem.Text;
            if (reader != null)
                reader.TagType = TagType;
        }

        private void tsCbbSerialPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        #endregion
       
        #region Process Button..
        private void tsBtConnect_Click(object sender, EventArgs e)
        {
            lbxResponses.Items.Add("Attempting Connection...");
            if (reader != null)
            {
                if (reader.IsHandling)
                {
                    reader.Close(CloseType.Close);
                    return;
                }
            }       
            reader.ReaderEvent += new ReaderEventHandler(OnReaderEvent);
            reader.TagType = TagType;
            reader.ConnectType = ConnectType;
            reader.ModelType = ModelType;

            if (ConnectType == ConnectType.Tcp)
            {
                reader.Open(tsCbbAddress.Text, 5578);
            }
            else
                reader.Open(tsCbbSerial.Text, 115200);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(reader !=null)
            {
                    if (reader.IsHandling){
                        MessageBeep(0);
                        e.Cancel = true;
                        reader.Close(CloseType.FormClose);
                        return;
                    }
            }
            base.OnClosing(e);
        }
        private void OnReaderEvent(object sender, ReaderEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ReaderEventHandler(OnReaderEvent), new object[] { sender, e });
                return;
            }
            switch (e.Type)
            {
                case EventType.Connected:
                    {
                        tsLbState.Text = e.Message;
                        tsBtConnect.Text = "&Disconnect";
                        tsFunctions.Visible = true;
                        break;
                    }
                case EventType.Disconnected:
                    {
                        tsLbState.Text = e.Message;
                        tsBtConnect.Text = "&Connect";
                        tsFunctions.Visible = true;
                        if (e.CloseType == CloseType.FormClose)
                            Close();
                        else
                            setTimer(6000, "Reconnect");
                        break;
                    }
                case EventType.Timeout:
                    {
                        tsLbState.Text = e.Message;
                        break;
                    }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                // BASIC OPERATIONS EVENTS
                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                case EventType.Inventory:
                case EventType.ReadMemory:
                case EventType.WriteMemory:
                case EventType.Command:
                case EventType.Lock:
                case EventType.Kill:
                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                // CONFIGURATIONS EVENTS
                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                case EventType.Buzzer:
                case EventType.ContinueMode:
                case EventType.Power:
                case EventType.Version:
                case EventType.AccessPwd:
                case EventType.GlobalBand:
                case EventType.Port:
                case EventType.Selection:
                case EventType.Filtering:
                case EventType.Algorithm:
                case EventType.TcpIp:
                    {
                       
                        //Received data are in the byte array (e.payload), it decodes into string using 
                        //following function.
                        string szPayload = Encoding.ASCII.GetString(e.Payload);
                       // MessageBox.Show(szPayload);
                        //Many responses can be contained in the generated event and it separates first.
                        //Received data may contain tag ID, set value, response code, etc. according to call 
                        //function, byte array(e.payload) may contain more than 1 response therefore it
                        //separates using following separator.

                        string[] szResponses = szPayload.Split(new string[] { "\r\n>" }, StringSplitOptions.RemoveEmptyEntries);
                        ///////////////////////////////////////////////////////////////////////////////////////////////////
                        // Response Code : [#]C##
                        // Tag Memory : [#]T3000111122223333444455556666[##]
                        // RSSI: [#]RFD##
                        // Settings Values : p0, c1, ...
                        ///////////////////////////////////////////////////////////////////////////////////////////////////
                        char code;
                        string szValue;
                        bool bCheckSum = reader.IsFixedType();
                        bool bMultiPort = reader.IsMultiPort();
                        int nPos = bMultiPort ? 1 : 0;              
                        int flag1 = 0;
                        int rssi1 = 0, rssi2 = 0, rssi3 = 0, rssi4 = 0, rssi = 0 ;
                        Dictionary<string, int> dRss1 = new Dictionary<string, int>();
                        Dictionary<string, int> dRss2 = new Dictionary<string, int>();
                        Dictionary<string, int> dRss3 = new Dictionary<string, int>();
                        Dictionary<string, int> dRss4 = new Dictionary<string, int>();
                       // lbxResponses.Items.Insert(0, szPayload);
                        string szTxt = string.Empty;
                        foreach (string szResponse in szResponses)
                        {
                            code = szResponse[nPos];
                            this.totalRead += 1;
                          //  code = 'R';
                            switch (code)
                            {
                                case 'T':
                                    {
                                        szValue = szResponse.Substring(nPos + 1, szResponse.Length - (nPos + 1 + (bCheckSum ? 2 : 0)));//exclude [#]T/C, CheckSum
                                        num5 = szValue;
                                        if (szValue.Length > 4)
                                        {
                                            string hex = szValue.Substring(4, szValue.Length - 4);
                                            szTxt = Reader.MakeTextFromHex(hex).ToString();
                                            text = szTxt ;
                                        }
                                        switch (szResponse[0])
                                        {
                                            case '1':
                                                tbTagId.Text = szValue;
                                                tbPort.Text = szResponse[0].ToString();
                                                tbText.Text = szTxt;
                                                foreach (string value in tagId)
                                                {
                                                    if (value.CompareTo(szValue) == 0)
                                                    {
                                                        flag1 = 1;
                                                        break;
                                                    }
                                                }
                                                if (flag1 == 0)
                                                {
                                                    tagId.Add(szValue);
                                                }
                                                break;
                                            case '2':
                                                tbTagId1.Text = szValue;
                                                tbPort1.Text = szResponse[0].ToString();
                                                tbText1.Text = szTxt;
                                                break;
                                            case '3':
                                                tbTagId2.Text = szValue;
                                                tbPort2.Text = szResponse[0].ToString();
                                                tbText2.Text = szTxt;
                                                break;
                                            case '4':
                                                tbTagId3.Text = szValue;
                                                tbPort3.Text = szResponse[0].ToString();
                                                tbText3.Text = szTxt ;
                                                break;
                                            default:
                                                break;
                                        }
                                        lbxResponses.Items.Insert(0, "Tag ID: " + szValue);
                                        lbxResponses.Items.Insert(1, "Port: " + szResponse[0]);
                                        lbxResponses.Items.Insert(2, "Text: " + text);
                                    }
                                    break;
                                case 'C':
                                    {
                                        szValue = szResponse.Substring(nPos + 1, szResponse.Length - (nPos + 1));//exclude [#]T/C
                                        szValue = szValue + "-" + reader.Responses(szValue);
                                        //if HEARTBEAT response, fire heartbeat timer again.
                                        if (string.Compare(szValue, "FF", StringComparison.Ordinal) == 0)
                                        {
                                            setTimer(9000, "Heartbeat");
                                        }
                                        lbxResponses.Items.Insert(0, "Data" + szValue + );
                                    }
                                    break;
                                case 'R':
                                    {
                                        szValue = szResponse.Substring(szResponse.Length - 2, 2);
                                        rssi = Convert.ToInt32(szValue, 0x10);
                                        
                                        double a = sdk.distanceFromRssi(rssi);  
                                       
                                        lbxResponses.Items.Insert(0, "Rssi " + szResponse[0] + ":" + " -" + rssi);
                                        switch (szResponse[0])
                                        {
                                            case '1':
                                                {
                                                    rssi1 = rssi;
                                                    tbRssi.Text = "-" + rssi.ToString();
                                                    arrRssi1.Add(rssi);
                                                    totalReadCounts1 ++;
                                                    tbRead.Text = totalReadCounts1.ToString();                                                   
                                                }
                                                break;
                                            case '2':
                                                {
                                                    rssi2 = rssi;
                                                    tbRssi1.Text = "-" + rssi.ToString();
                                                    arrRssi2.Add(rssi);
                                                    totalReadCounts2++;
                                                    tbRead1.Text = totalReadCounts2.ToString();
                                                }
                                                break;
                                            case '3':
                                                {
                                                    this.tbRssi2.Text = "-" + rssi.ToString();
                                                    rssi3 = rssi;
                                                    arrRssi3.Add(rssi);
                                                    totalReadCounts3++;
                                                    tbRead2.Text = totalReadCounts3.ToString();
                                                }
                                                break;
                                            case '4':
                                                {
                                                    rssi4 = rssi;
                                                    tbRssi3.Text = "-" + rssi.ToString() ;
                                                    arrRssi4.Add(rssi);
                                                    totalReadCounts4++;
                                                    tbRead3.Text = totalReadCounts4.ToString();
                                                }
                                                break;
                                            default:
                                                break;
                                        }                                  
                                        if (trainingFlag ==false)
                                          {

                                            string query = "INSERT INTO rfidreader.rfid(TagId ,Rssi1,Rssi2, Rssi3, Rssi4, Text, TotalRead1, TotalRead2, TotalRead3, TotalRead4,Position) VALUE('" + num5 + "', '" + rssi1.ToString() + "','" + rssi2.ToString() + "',  '" + rssi3.ToString() + "', '" + rssi4.ToString() + "','" + text + "' , '" + totalReadCounts1.ToString() + "','" + totalReadCounts2.ToString() + "','" + totalReadCounts3.ToString() + "','" + totalReadCounts4.ToString() + "', '" + "" + "')";
                                            cmd = new MySqlCommand(query, conn);
                                            MySqlDataReader myReader;
                                            try
                                            {
                                                OpenConnection();
                                                myReader = cmd.ExecuteReader();
                                                CloseConnection();
                                            }
                                            catch (MySqlException ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                                CloseConnection();
                                            }
                                            rssi1 = rssi2 = rssi3 = rssi4 = 0;
                                            this.totalRead = 0;
                                        }
                                    }
                                    break;

                                default:
                                       lbxResponses.Items.Insert(1, szResponse);
                                    break;
                            }
                            
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        private static void trainingTimerEventProcessor(Object obj, EventArgs args)
        {          
            reader.StopOperation();
            traningTimer.Stop();
            
                if (totalReadCounts2 >= 20 && totalReadCounts1 >= 20 && totalReadCounts3 >=20)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        cmd = new MySqlCommand("INSERT INTO rfidreader.rfid(TagId ,Rssi1,Rssi2,Rssi3,Text, TotalRead1, TotalRead2,TotalRead3,Position) VALUE('" + "khanh" + "', '" + arrRssi1[i].ToString() + "','" + arrRssi2[i].ToString() + "','" + arrRssi3[i].ToString() + "','" + "4" + "' , '" + totalReadCounts1.ToString() + "','" + totalReadCounts2.ToString() + "','" + totalReadCounts3.ToString() + "','" + "" + "')", conn);
                        MySqlDataReader myReader;
                        try
                        {
                            OpenConnection();
                            myReader = cmd.ExecuteReader();
                            CloseConnection();
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                            CloseConnection();
                        }
                    }
                    traningTimer.Stop();
                }
            else
            {
                timeDelay += timeDelay_;
                if (timeDelay > 60000)
                {
                    if (totalReadCounts1 >= 20 && totalReadCounts2 >= 20)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            cmd = new MySqlCommand("INSERT INTO rfidreader.rfid(TagId ,Rssi1,Rssi2,Rssi3,Text, TotalRead1, TotalRead2,TotalRead3,Position) VALUE('" + "khanh" + "', '" + arrRssi1[i].ToString() + "','" + arrRssi2[i].ToString() + "','" + "999" + "','" + "4" + "' , '" + totalReadCounts1.ToString() + "','" + totalReadCounts2.ToString() + "','" + totalReadCounts3.ToString() + "','" + "" + "')", conn);
                            MySqlDataReader myReader;
                            try
                            {
                                OpenConnection();
                                myReader = cmd.ExecuteReader();
                                CloseConnection();
                            }
                            catch (MySqlException ex)
                            {
                                MessageBox.Show(ex.Message);
                                CloseConnection();
                            }
                        }
                    }
                    if (totalReadCounts1 >= 20 && totalReadCounts3 >= 20)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            cmd = new MySqlCommand("INSERT INTO rfidreader.rfid(TagId ,Rssi1,Rssi2,Rssi3,Text, TotalRead1, TotalRead2,TotalRead3,Position) VALUE('" + "khanh" + "', '" + arrRssi1[i].ToString() + "','" + "0" + "','" + arrRssi3[i].ToString() + "','" + "4" + "' , '" + totalReadCounts1.ToString() + "','" + totalReadCounts2.ToString() + "','" + totalReadCounts3.ToString() + "','" + "" + "')", conn);
                            MySqlDataReader myReader;
                            try
                            {
                                OpenConnection();
                                myReader = cmd.ExecuteReader();
                                CloseConnection();
                            }
                            catch (MySqlException ex)
                            {
                                MessageBox.Show(ex.Message);
                                CloseConnection();
                            }
                        }
                    }
                    if (totalReadCounts2 >= 20 && totalReadCounts3 >= 20)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            cmd = new MySqlCommand("INSERT INTO rfidreader.rfid(TagId ,Rssi1,Rssi2,Rssi3,Text, TotalRead1, TotalRead2,TotalRead3,Position) VALUE('" + "khanh" + "', '" + "0" + "','" + arrRssi2[i].ToString() + "','" + arrRssi3[i].ToString() + "','" + "4" + "' , '" + totalReadCounts1.ToString() + "','" + totalReadCounts2.ToString() + "','" + totalReadCounts3.ToString() + "','" + "" + "')", conn);
                            MySqlDataReader myReader;
                            try
                            {
                                OpenConnection();
                                myReader = cmd.ExecuteReader();
                                CloseConnection();
                            }
                            catch (MySqlException ex)
                            {
                                MessageBox.Show(ex.Message);
                                CloseConnection();
                            }
                        }
                    }
                    if (totalReadCounts1 >= 20 && totalReadCounts2 < 20 && totalReadCounts3 < 20)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            cmd = new MySqlCommand("INSERT INTO rfidreader.rfid(TagId ,Rssi1,Rssi2,Rssi3,Text, TotalRead1, TotalRead2,TotalRead3,Position) VALUE('" + "khanh" + "', '" + arrRssi1[i].ToString() + "','" + "0" + "','" + "0" + "','" + "4" + "' , '" + totalReadCounts1.ToString() + "','" + totalReadCounts2.ToString() + "','" + totalReadCounts3.ToString() + "','" + "" + "')", conn);
                            MySqlDataReader myReader;
                            try
                            {
                                OpenConnection();
                                myReader = cmd.ExecuteReader();
                                CloseConnection();
                            }
                            catch (MySqlException ex)
                            {
                                MessageBox.Show(ex.Message);
                                CloseConnection();
                            }
                        }
                    }
                    if (totalReadCounts2 >= 20 && totalReadCounts1 < 20 && totalReadCounts3 < 20)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            cmd = new MySqlCommand("INSERT INTO rfidreader.rfid(TagId ,Rssi1,Rssi2,Rssi3,Text, TotalRead1, TotalRead2,TotalRead3,Position) VALUE('" + "khanh" + "', '" + "0" + "','" + arrRssi2[i].ToString() + "','" + "0" + "','" + "4" + "' , '" + totalReadCounts1.ToString() + "','" + totalReadCounts2.ToString() + "','" + totalReadCounts3.ToString() + "','" + "" + "')", conn);
                            MySqlDataReader myReader;
                            try
                            {
                                OpenConnection();
                                myReader = cmd.ExecuteReader();
                                CloseConnection();
                            }
                            catch (MySqlException ex)
                            {
                                MessageBox.Show(ex.Message);
                                CloseConnection();
                            }
                        }

                    }
                    if (totalReadCounts3 >= 20 && totalReadCounts1 < 20 && totalReadCounts2 < 20)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            cmd = new MySqlCommand("INSERT INTO rfidreader.rfid(TagId ,Rssi1,Rssi2,Rssi3,Text, TotalRead1, TotalRead2,TotalRead3,Position) VALUE('" + "khanh" + "', '" + "0" + "','" + "0" + "','" + totalReadCounts3.ToString() + "','" + "4" + "' , '" + totalReadCounts1.ToString() + "','" + totalReadCounts2.ToString() + "','" + totalReadCounts3.ToString() + "','" + "" + "')", conn);
                            MySqlDataReader myReader;
                            try
                            {
                                OpenConnection();
                                myReader = cmd.ExecuteReader();
                                CloseConnection();
                            }
                            catch (MySqlException ex)
                            {
                                MessageBox.Show(ex.Message);
                                CloseConnection();
                            }

                        }
                    }
                }
                else
                {
                    traningTimer.Interval = timeDelay_;
                    traningTimer.Start();
                    reader.InventoryMultiple();

                }
           }
        }
        private static void inventoryTimerEventProcessor(object obj, EventArgs args)
        {
            reader.StopOperation();
            traningTimer.Stop();
            List<Double> diffRssi;
            double rssi1_ = 0, rssi2_ = 0, rssi3_ = 0;
            double diffRssi1 = 0, diffRssi2 = 0, diffRssi3 = 0;
            int box = 0;
            Dictionary<String, Double> dicDiff = new Dictionary<string, double>();
            Map map = new Map();
            sendBox SendBox = new sendBox(map.getBox);            
            if ((totalReadCounts1 >= 5) && (totalReadCounts2 >= 5) && (totalReadCounts3 >= 5))
            {
                rssi1_ = averageRssi(arrRssi1);
                rssi2_ = averageRssi(arrRssi2);
                rssi3_ = averageRssi(arrRssi3);  
                diffRssi = new List<Double>();
                for (int i = 0; i < dataRssi.Count; i++)
                {
                    diffRssi1 = Math.Abs(rssi1_ - dataRssi[i].getRssi1());
                    diffRssi2 = Math.Abs(rssi2_ - dataRssi[i].getRssi2());
                    diffRssi3 = Math.Abs(rssi3_ - dataRssi[i].getRssi3());
                    diffRssi.Add(diffRssi1 + diffRssi2 + diffRssi3);
                }
                box = findMin(diffRssi);
            }
            else if ((totalReadCounts2 >= 5) && (totalReadCounts3 >= 5) && totalReadCounts1 <5)
            {
                rssi1_ = 0;
                rssi2_ = averageRssi(arrRssi2);
                rssi3_ = averageRssi(arrRssi3);
                diffRssi = new List<Double>();
                for (int i = 0; i < dataRssi.Count; i++)
                {
                    diffRssi1 = Math.Abs(rssi1_ - dataRssi[i].getRssi1());
                    diffRssi2 = Math.Abs(rssi2_ - dataRssi[i].getRssi2());
                    diffRssi3 = Math.Abs(rssi3_ - dataRssi[i].getRssi3());
                    diffRssi.Add(diffRssi1 + diffRssi2 + diffRssi3);
                }
                box = findMin(diffRssi);
            }
            else if ((totalReadCounts1 >= 5) && (totalReadCounts3 >= 5) && totalReadCounts2 < 5)
            {
                rssi1_ = averageRssi(arrRssi1);
                rssi2_ = 0;
                rssi3_ = averageRssi(arrRssi3);
                diffRssi = new List<Double>();
                for (int i = 0; i < dataRssi.Count; i++)
                {
                    diffRssi1 = Math.Abs(rssi1_ - dataRssi[i].getRssi1());
                    diffRssi2 = Math.Abs(rssi2_ - dataRssi[i].getRssi2());
                    diffRssi3 = Math.Abs(rssi3_ - dataRssi[i].getRssi3());
                    diffRssi.Add(diffRssi1 + diffRssi2 + diffRssi3);
                }
                box = findMin(diffRssi);
            }
            else if ((totalReadCounts1 >= 5) && (totalReadCounts2 >= 5) && totalReadCounts3 < 5)
            {
                rssi1_ = averageRssi(arrRssi1);
                rssi2_ = averageRssi(arrRssi2);
                rssi3_ = 0;
                diffRssi = new List<Double>();
                for (int i = 0; i < dataRssi.Count; i++)
                {
                    diffRssi1 = Math.Abs(rssi1_ - dataRssi[i].getRssi1());
                    diffRssi2 = Math.Abs(rssi2_ - dataRssi[i].getRssi2());
                    diffRssi3 = Math.Abs(rssi3_ - dataRssi[i].getRssi3());
                    diffRssi.Add(diffRssi1 + diffRssi3 + diffRssi2);
                }
                box = findMin(diffRssi);
            }
           // MessageBox.Show(box.ToString());
            SendBox(box);
            totalReadCounts1 = 0;
            totalReadCounts2 = totalReadCounts3 = totalReadCounts4 = 0;
            rssi1_ = rssi2_ = rssi3_ = 0;
            arrRssi1.Clear();
            arrRssi1.Clear();
            arrRssi3.Clear();
            arrRssi4.Clear();  
            reader.InventoryMultiple();
            traningTimer.Start();
        }
        private void tsBtInventoryMulti_Click(object sender, EventArgs e)
        {
            traningTimer.Stop();                  
            trainingFlag = false;
            totalReadCounts1 = totalReadCounts2 = totalReadCounts3 = totalReadCounts4 = 0;
            arrRssi1.Clear();
            arrRssi1.Clear();
            arrRssi3.Clear();
            arrRssi4.Clear();      
            reader.InventoryMultiple();
            inventoryTimer.Start();
        }
        private static int findMin(List<Double> list)
        {
            double min;
            int box = 0;
            min = list[0];
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] < min)
                {
                    min = list[i];
                    box = i;
                }
            }
            return box;
        }
        public static double averageRssi(ArrayList arr)
        {
            double result = 0;
            foreach (int item in arr)
            {
                result += item;
            }
            return (double)(result/(arr.Count));
        }
        private void tsBtInventorySingle_Click(object sender, EventArgs e)
        {
            reader.InventorySingle();
        }

        private void tsBtStop_Click(object sender, EventArgs e)
        {
            reader.StopOperation();
            traningTimer.Stop();     
            // trainingTim
        }

        private void tsBtReadMemeory_Click(object sender, EventArgs e)
        {
            reader.ReadMemory(MemoryType.EPC, 1,3);
        }

        private void tsBtWriteMemory_Click(object sender, EventArgs e)
        {
            string tx = null;
            List<string> tx1 = cipher.transpositionCipherCryptography(cbTextWrite.Text, "ascii.txt");
            for (int i = 0; i < tx1.Count; i++)
            {
                tx += tx1[i];
            }
            
            reader.WriteMemory(tx);
        }

        private void tsBtLock_Click(object sender, EventArgs e)
        {
            //reader.Lock("0030",);
           // reader.SetPortPower(1, 300);
        }
        private void tsBtKill_Click(object sender, EventArgs e)
        {
            //reader.Kill("12345678");
        }

        private void tsBtClear_Click(object sender, EventArgs e)
        {
            lbxResponses.Items.Clear();
        }
        private void tsBtConfigurations_Click(object sender, EventArgs e)
        {
            //Configures conf = new Configures();
           // conf.Show();
           // reader.SetDefaultSettings();//3E 78 20 64

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //GENERAL - COMMON
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //reader.SetPower(1);//3E 78 20 70 20 31
            //reader.SetAccessPwd("12345678");//3E 78 20 77 20 31 32 33 34 35 36 37 38
            //reader.SetGlobalBand(1);//3E 78 20 66 20 31

            //reader.GetPower();//3E 79 20 70
            //reader.GetFwVersion();//3E 79 20 76 0A 0D
            ////3E 79 31 31 
            //reader.GetAccessPwd();
            //reader.GetGlobalBand();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //OPTIONS - COMMON
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
           // reader.SetBuzzer(false);
            //reader.SetContinueMode(false);
            //reader.GetBuzzer();
            //reader.GetContinueMode();
         //   reader.SetPower(0);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // PORT - FIXED TYPE
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            reader.SetPortActive(1);
           reader.SetPortActive(2);
       reader.SetPortActive(3);
          reader.SetPortActive(4);
           reader.SetPortPower(1, 300);
           reader.SetPortPower(2, 300);
        reader.SetPortPower(3, 300);
         reader.SetPortPower(4, 300);
         reader.SetPortInventoryCount(50);
            reader.SetPortInventoryTime(100);
            reader.SetPortIdleTime(110);
            
            //reader.GetPortActive();
         //   reader.GetPortPower(1);
         //   reader.GetPortPower(2);
            //reader.GetPortPower(3);
            //reader.GetPortPower(4);
            //reader.GetPortInventoryCount();
            //reader.GetPortInventoryTime();
            //reader.GetPortIdleTime();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //SELECTION - FIXED TYPE
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //reader.SetSelectionBank(MemoryType.EPC);
            //reader.SetSelectionOffset(16);
            //reader.SetSelectionAction(SelectionActionType.Matching);
            //reader.SetSelectionSession(0);

            //reader.GetSelectionBank();
            //reader.GetSelectionOffset();
            //reader.GetSelectionAction();
            //reader.GetSelectionSession();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //ALGORITHM - FIXED TYPE
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //reader.SetAlgorithmParameter(Algorithm.DYNAMICQ_THRESH, 0, 7);
            //reader.SetAlgorithmParameter(Algorithm.DYNAMICQ_THRESH, 1, 0);
            //reader.SetAlgorithmParameter(Algorithm.DYNAMICQ_THRESH, 2, 15);

            //reader.GetAlgorithmParameter(Algorithm.DYNAMICQ_THRESH, 0);
            //reader.GetAlgorithmParameter(Algorithm.DYNAMICQ_THRESH, 1);
            //reader.GetAlgorithmParameter(Algorithm.DYNAMICQ_THRESH, 2);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //TCP/IP - FIXED TYPE
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //reader.SetConnectionSetting(ConnectionSetting.Address, "192.168.9.6");

            //reader.GetConnectionSetting(ConnectionSetting.Address);
            //reader.GetConnectionSetting(ConnectionSetting.Port);
            //reader.GetConnectionSetting(ConnectionSetting.Baudrate);
        }
        private void tsBtGetDataStore_Click(object sender, EventArgs e)
        {
            //In the case of a large store of data,
            //Because it may cause problems, The following process is recommended if necessary
            //1. stop heartbeat
            //2. stop inventory
            reader.Command("o", false);//Get Stored data
        }

        #endregion
        #region Timer Reconnect...
        System.Windows.Forms.Timer timer = null;
        private string timerMessage = string.Empty;
        public void setTimer(int nInterval, string nMessage)
        {
            if (timer == null)
            {
                timer = new System.Windows.Forms.Timer();
                timer.Tick += new System.EventHandler(TimerHandler);
            }
            timer.Interval = nInterval;
            timerMessage = nMessage;
            timer.Enabled = true;
        }
        public void StopTimer()
        {
            if (timer != null)
                timer.Enabled = false;
        }
        private void TimerHandler(object sender, EventArgs e)
        {
            timer.Enabled = false;
            OnReaderEvent(this, new ReaderEventArgs(EventType.Timer, timerMessage));
        }
        public bool IsTiming
        {
            get { return timer == null ? false : timer.Enabled; }
        }
        private void tsBtClearDatabase_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void btMapping_Click(object sender, EventArgs e)
        {
            Map map = new Map();
            map.ShowDialog();
           /* String path1 = "ascii.txt";
            List<String> m = new List<string>();
            char[,] b = null;
            double c;
            string input1 = "AEMHKUDJG";
            Dictionary<String, Double> res;
            m = cipher.transpositionCipherCryptography(input1, path1);
            b = cipher.transpositionCipherDecryption(m);
            string d = cipher.substitutionCipherDecryption(b, path1);
            MessageBox.Show(d);*/
        }     
        #endregion
        #region Process Delegate
        public void getDataConfig(int power, string st2, string st3, string st4)
        {
        }
        public void getDataWrite(String str)
        {
            stt[0] = str;
        }
        public void getCell(string cell)
        {
            double r1 = 0, r2 = 0, r3 = 0, r4 = 0;

            foreach (int item in arrRssi1)
            {
                r1 += item;
            }
            // MessageBox.Show("Rssi1: " + ((double)(r1 / arrRssi1.Count)).ToString());
            foreach (int item in arrRssi2)
            {
                r2 += item;
            }
            //MessageBox.Show("Rssi2: " + ((double)(r2 / arrRssi2.Count)).ToString());
            foreach (int item in arrRssi3)
            {
                r3 += item;
            }
            // MessageBox.Show("Rssi3: " + ((double)(r3 / arrRssi3.Count)).ToString());
            foreach (int item in arrRssi4)
            {
                r4 += item;
            }
            //MessageBox.Show("Rssi4: " + ((double)(r4 / arrRssi4.Count)).ToString());
            cmd = new MySqlCommand("INSERT INTO rfidreader.training(cell ,rssi1,rssi2, rssi3, rssi4) VALUE('" + cell + "', '" + ((double)(r1 / arrRssi1.Count)).ToString() + "','" + ((double)(r2 / arrRssi2.Count)).ToString() + "',  '" + ((double)(r3 / arrRssi3.Count)).ToString() + "', '" + ((double)(r4 / arrRssi4.Count)).ToString() + "')", conn);
           MySqlDataReader myReader;
            try
            {
                OpenConnection();
                myReader = cmd.ExecuteReader();
                CloseConnection();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                CloseConnection();
            }
        }
       #endregion

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_Bluetooth(object sender, EventArgs e)
        {
            long endTick = time.Ticks;
            MessageBox.Show((endTick - tick).ToString());       
        }

        private void tbTagId_Click(object sender, EventArgs e)
        {

        }

        private void tbText_Click(object sender, EventArgs e)
        {

        }

        

        private void trainingData_click(object sender, EventArgs e)
        {
            totalReadCounts1 = totalReadCounts2 = totalReadCounts3 = totalReadCounts4 = 0;
            trainingFlag = true;
            arrRssi1.Clear();
            arrRssi1.Clear();
            arrRssi3.Clear();
            arrRssi4.Clear();
            traningTimer.Start();
            reader.InventoryMultiple();  
        }

        private void calculator_click(object sender, EventArgs e)
        {
            cell Cell = new cell();
            Cell.ShowDialog();
        }

        private void btnLut_click(object sender, EventArgs e)
        {
            cmd = new MySqlCommand("SELECT * FROM rfidreader.training", conn);
            MySqlDataReader myReader;
            Lut lut;
            double rssi1 = 0, rssi2 = 0, rssi3 = 0;
            int cell = 0;
            try
            {
                OpenConnection();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    lut = new Lut(myReader.GetInt16(1), myReader.GetDouble(2), myReader.GetDouble(3), myReader.GetDouble(4));
                    dataTraining.Add(lut);
                };
                CloseConnection();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                CloseConnection();
            }
            lut = new Lut();
            while (count <= dataTraining.Count -8 )
            {
                for (int j = 0; j < 8; j++)
                {
                    lut = dataTraining[j + count];
                    cell = lut.getCell();
                    rssi1 += lut.getRssi1();
                    rssi2 += lut.getRssi2();
                    rssi3 += lut.getRssi3();                
                }
                lut.setCell(cell);
                lut.setRssi1(rssi1 / 8);
                lut.setRssi2(rssi2 / 8);
                lut.setRssi3(rssi3 / 8);               
                dataRssi.Add(lut);           
                rssi1 = rssi2 = rssi3 = 0;          
                count += 8;
            }
          //  MessageBox.Show(dataRssi.Count.ToString());
        }

        private void tsCbbAddress_Click(object sender, EventArgs e)
        {

        }

        private void tsMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tsFunctions_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tbTagId2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test test = new Test();
            test.ShowDialog();
        }

        
       


    }
    
}

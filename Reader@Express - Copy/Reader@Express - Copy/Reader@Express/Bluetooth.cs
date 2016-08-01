using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using InTheHand.Net;
using InTheHand.Net.Sockets;
using InTheHand.Windows.Forms;
using InTheHand.Net.Bluetooth;
using System.IO;

namespace Reader_Express
{
    public partial class Bluetooth : Form
    {
        readonly Guid OurServiceClassId = new Guid("{29913A2D-EB93-40cf-BBB8-DEEE26452197}");
        readonly string OurServiceName = "32feet.NET Chat2";
        TextWriter _connWtr;
        BluetoothListener _lsnr;
        public Bluetooth()
        {
            InitializeComponent();
        }
        #region Bluetooth Start/Listen/Connect
        private void StartBluetooth()
        {
            try
            {
                new BluetoothClient();
            }
            catch (Exception ex)
            {
                var msg = "Bluetooth init failed: " + ex;
                MessageBox.Show(msg);
                throw new InvalidOperationException(msg, ex);
            }
            // TODO Check radio?
            //
            // Always run server?
            StartListener();
        }
        private void StartListener()
        {
            var lsnr = new BluetoothListener(OurServiceClassId);
            lsnr.ServiceName = OurServiceName;
            lsnr.Start();
            _lsnr = lsnr;
            ThreadPool.QueueUserWorkItem(ListenerAccept_Runner, lsnr);
        }
        void ListenerAccept_Runner(object state)
        {
            var lsnr = (BluetoothListener)_lsnr;
            // We will accept only one incoming connection at a time. So just
            // accept the connection and loop until it closes.
            // To handle multiple connections we would need one threads for
            // each or async code.
            while (true)
            {
                var conn = lsnr.AcceptBluetoothClient();
                var peer = conn.GetStream();
                SetConnection(peer, false, conn.RemoteEndPoint);
               // ReadMessagesToEnd(peer);
            }
        }
        BluetoothAddress BluetoothSelect()
        {
            var dlg = new SelectBluetoothDeviceDialog();
            var rslt = dlg.ShowDialog();
            if (rslt != DialogResult.OK)
            {
                //AddMessage(MessageSource.Info, "Cancelled select device.");
                return null;
            }
            var addr = dlg.SelectedDevice.DeviceAddress;
            return addr;
        }
        #endregion
        #region Connection Set/Close
        private void SetConnection(Stream peerStream, bool outbound, BluetoothEndPoint remoteEndPoint)
        {
            if (_connWtr != null)
            {
                //AddMessage(MessageSource.Error, "Already Connected!");
                return;
            }
            var connWtr = new StreamWriter(peerStream);
            connWtr.NewLine = "\r\n"; // Want CR+LF even on UNIX/Mac etc.
            _connWtr = connWtr;
           // ClearScreen();
           // AddMessage(MessageSource.Info,
              //  (outbound ? "Connected to " : "Connection from ")
                // Can't guarantee that the Port is set, so just print the address.
                // For more info see the docs on BluetoothClient.RemoteEndPoint.
             //   + remoteEndPoint.Address);
        }

        private void ConnectionCleanup()
        {
            var wtr = _connWtr;
            //_connStrm = null;
            _connWtr = null;
            if (wtr != null)
            {
                try
                {
                    wtr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        void BluetoothDisconnect()
        {
          //  AddMessage(MessageSource.Info, "Disconnecting");
            ConnectionCleanup();
        }
        #endregion

        
        private void btn_ScanDevice(object sender, EventArgs e)
        {
            BluetoothSelect();
        }

    }
}

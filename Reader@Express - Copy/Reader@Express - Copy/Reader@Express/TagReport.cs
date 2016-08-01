using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Reader_Express
{
    class TagReport
    {

        #region TagReport...
            private int port;
            private double readRate;
            private double rssi;
            private string tagIdHex;
            private string tagIdText;
            private long totalRead;

            public TagReport()
            {
            }
            public TagReport(int port, double readRate, double rssi, string tagIdHex, string tagIdText, long totalRead)
            {
                this.port = port;
                this.readRate = readRate;
                this.rssi = rssi;
                this.tagIdHex = tagIdHex;
                this.tagIdText = tagIdText;
                this.totalRead = totalRead;
            }
            public int Port
            {
                get
                {
                    return this.port;
                }
                set
                {
                    this.port = value;
                }
            }
            public double ReadRate
            {
                get
                {
                    return this.readRate;
                }
                set
                {
                    this.readRate = value;
                }
            }
            public double RSSI
            {
                get
                {
                    return this.rssi;
                }
                set
                {
                    this.rssi = value;
                }
            }
            public string TagIdHex
            {
                get
                {
                    return this.tagIdHex;
                }
                set
                {
                    this.tagIdHex = value;
                }
            }
            public string TagIdText
            {
                get
                {
                    return this.tagIdText;
                }
                set
                {
                    this.tagIdText = value;
                }
            }
            public long TotalRead
            {
                get
                {
                    return this.totalRead;
                }
                set
                {
                    this.totalRead = value;
                }
            }

        #endregion
    }
}

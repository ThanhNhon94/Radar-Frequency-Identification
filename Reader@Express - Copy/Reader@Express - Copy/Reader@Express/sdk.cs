using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using idro.controls.panel;
using idro.reader.api;
using idro.reader;

namespace Reader_Express
{
    class sdk
    {
        
        public const string Any = "Any";
        public const string ApplicationType = "ApplicationType";
        public const string BaudRate = "BaudRate";
        public const string Broadcast = "Broadcast";
        public const string BrowserPort = "BrowserPort";
        public static StringBuilder builder = new StringBuilder(0x400);
        public const string Connecting = "Connecting";
        public const string ConnectionType = "ConnectionType";
        public const string Controller = "Controller";
        public const string ControllerPort = "ControllerPort";
        public const string Decode = "Decode";
        public const string Disable = "Disable";
        public const string Ellipsis = "...";
        public const string Enable = "Enable";
        public const string Engineer = "Engineer";
        public const string Filtering = "Filtering";
        public const string IP = "IP";
        public const string Mask = "Mask";
        public const string Mask1 = "Mask1";
        public const string Mask2 = "Mask2";
        public const string ModelNames = "ModelNames";
        public const string ModelType = "ModelType";
        public const string Noname = "Noname";
        public const string Number = "9876543210";
        public const string Options = "Options";
        public static string[] PacketSeparator = new string[] { "\r\n>" };
        public const string Ready = "Ready";
        public const string Relation = "Relation";
        public const string Search = "Search";
        public const string Selection = "Selection";
        public const string Separator1 = "\r\n>";
        public const string SerialPort = "SerialPort";
        public const string Settings = "Settings";
        public const string SkinStyle = "SkinStyle";
        public const string Stop = "Stop";
        public const string Success = "Success";
        public const string Timeout = "Timeout";
        public const string Waiting = "Waiting";
        public static int hammu(int coso, int somu)
        {
            for(int i=0;i<somu;i++){
                coso *=coso;
            }
            return coso;
        }

        public static string ChangeWindowText(string caption, string change)
        {
            int index = caption.IndexOf('-');
            if (index >= 0)
            {
                return (caption.Substring(0, index + 2) + change);
            }
            return (caption + " - " + change);
        }

        public static int GetTickCount()
        {
            return Environment.TickCount;
        }

        public static bool IsHex(string hex)
        {
            foreach (char ch in hex)
            {
                if ((!char.IsNumber(ch) && ((ch < 'a') || (ch > 'f'))) && ((ch < 'A') || (ch > 'F')))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsIpAddr(string ipAddr)
        {
            bool flag = true;
            foreach (char ch in ipAddr)
            {
                if (!char.IsNumber(ch) && (ch != '.'))
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                string[] strArray = ipAddr.Split(new char[] { '.' }, StringSplitOptions.None);
                if (strArray.Length != 4)
                {
                    return false;
                }
                foreach (string str in strArray)
                {
                    if (str.Length == 0)
                    {
                        return false;
                    }
                }
            }
            return flag;
        }

        public static bool IsMacAddr(string addr)
        {
            if (string.IsNullOrEmpty(addr))
            {
                return false;
            }
            return (addr.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries).Length == 6);
        }

        public static bool IsPassword(string password)
        {
            bool flag = true;
            if (password.Length <= 0)
            {
                return flag;
            }
            if (password.Length != 8)
            {
                return false;
            }
            return IsHex(password);
        }

        public static bool RunInventoryOption(ShowOptionType type)
        {
            return ((type & ShowOptionType.RunInventory) > ShowOptionType.None);
        }

        public static bool SearchOption(ShowOptionType type)
        {
            return ((type & ShowOptionType.Search) > ShowOptionType.None);
        }

        public static void SetState(ref ShowOptionType type, ShowOptionType nSet, ShowOptionType nClear)
        {
            if (nSet > ShowOptionType.None)
            {
                type |= nSet;
            }
            if (nClear > ShowOptionType.None)
            {
                type &= ~nClear;
            }
        }

        public static int ToInt32(string value, int defaultValue)
        {
            int num = defaultValue;
            try
            {
                num = Convert.ToInt32(value);
            }
            catch
            {
            }
            return num;
        }
        public static int ToInt16(string value, int defaultValue)
        {
            int num = defaultValue;
            try
            {
                num = Convert.ToInt16(value);
            }
            catch
            {
            }
            return num;
        }
        public static ulong ToUInt64(string value, ulong defaultValue)
        {
            ulong num = defaultValue;
            try
            {
                num = Convert.ToUInt64(value);
            }
            catch
            {
            }
            return num;
        }
        public static double distanceFromRssi(double rssi)
        {
            double pLoss, temp, distance;
            double Pt = 30.0;
            double Pcl = 0.1;
            double Ga = 9.0;
            double wavelength = 0.327869;
            pLoss = (Pt - Pcl + Ga  + rssi) / 2.0;
            temp = pLoss / 20.0;
            temp = Math.Pow(10.0, temp);
            distance = (temp * wavelength) / (4 * Math.PI);
            return distance;
        }
        public static double averageRssi(int[] a, int size)
        {
            double average;
            int  i = 0;
            int temp =0;
            for(i = 0; i <size; i++){
                temp = temp + a[i];
            }
            average = temp / size;
            return average;
        }
        public int a(int[] arr)
        {
            int result = 0;
            int count = arr.Length;
            return result;
               
        }
            
    }
}

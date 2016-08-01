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
        public  int a(int[] arr)
        {
            int result = 0;
            int count = arr.Length;
            return result;
               
        }

        public static int int_(double a)
        {
            int k = 0;
            double min = a;
            for (; k <= a; k++)
            {
                double sub = a - k;
                min = (sub < min) ? sub : min;
            }
            return k - 1;
        }

        //Ham noi suy tuyen tinh
        public static double Interpol(double[,] mat, double a, double b)
        {
            int a0, b0, a1, b1;
            if (a == 3)
            {
                a0 = 2;
                a1 = 3;
                b0 = int_(b);
                b1 = b0 + 1;
            }
            else
            {
                if (b == 3)
                {
                    a0 = int_(a);
                    a1 = a0 + 1;
                    b0 = 2;
                    b1 = 3;
                }
                else
                {
                    a0 = int_(a);
                    a1 = a0 + 1;
                    b0 = int_(b);
                    b1 = b0 + 1;
                }
            }

            double res = mat[a0, b0] * (b1 - b) * (a - a0) + mat[a1, b0] * (b - b0) * (a - a0) + mat[a0, b1] * (b1 - b) * (a1 - a) + mat[a1, b1] * (b - b0) * (a1 - a);
            return res;
        }

        //public static int convert_num(double x, double y)
        //{
        //    int num = 0;
        //    switch (int_(x))
        //    {
        //        case 0: num = 1; break;
        //        case 1: num = 4; break;
        //        case 2: num = 7; break;
        //        case 3: num = 11; break;
        //        case 4: num = 11; break;
        //        //case 5: num = 15; break;
        //        //case 6: num = 26; break;
        //    }
        //    for (int k = 0; k != int_(y); k++)
        //    {
        //        num = num + 1;
        //    }
        //    if (int_(y) == 3) return num;
        //    else return num + 1;
        //}
        public static int convert_num(int x, int y)
        {
            int num;
            if ((x == 0) && (y == 0)) num = 1;
            else if ((x == 0) && (y == 1)) num = 2;
            else if ((x == 0) && (y == 2)) num = 3;
            else if ((x == 1) && (y == 0)) num = 4;
            else if ((x == 1) && (y == 1)) num = 5;
            else if ((x == 1) && (y == 2)) num = 6;
            else if ((x == 2) && (y == 0)) num = 7;
            else if ((x == 2) && (y == 1)) num = 8;
            else if ((x == 2) && (y == 2)) num = 9;
            else num = 0;
            return num;
        }

        //Ham kiem tra phan tu thuoc mang
        public static bool in_array(int[] w, int v)
        {
            bool in_ = false;
            for (int p = 0; p < w.Length; p++)
            {
                if (v == w[p])
                {
                    in_ = true;
                    break;
                }
                else in_ = false;
            }
            return in_;
        }

        //Ham xac dinh toa do
        //public static int[] local_xy(double[,] mt, double z, double err, double pix)
        //{
        //    int[] local = new int[30];
        //    int l = 0;
        //    double inter;

        //    for (int k = 0; k < (3 / pix); k++)
        //        for (int n = 0; n <= (3 / pix); n++)
        //        {
        //            inter = Interpol(mt, pix * k, pix * n);
        //            if (Math.Abs(z - inter) < err)
        //                if (in_array(local, convert_num(pix * k, pix * n)) == false) local[l++] = convert_num(pix * k, pix * n);
        //        }
        //    return local;
        //}
        public static int local_xy(double[,] mt, double z)
        {
            int local =0;
            if (z == 0) return 0;
            double sub = Math.Abs(mt[0,0]-z);
            //double inter;

            for (int k = 0; k < 3; k++)
                for (int n = 0; n < 3; n++)
                {
                    //inter = Interpol(mt, pix * k, pix * n);
                    if (Math.Abs(z - mt[k, n]) < sub)
                        local = convert_num(k, n); 
                        //if (in_array(local, convert_num(pix * k, pix * n)) == false) local[l++] = convert_num(pix * k, pix * n);
                }
            return local;
        }

        public static int in_array1(int[] x1,int[] x2,int[] x3,int[] x4,int a)
        {
            int c=0,c1=0,c2=0,c3=0,c4=0,co = 0;
            if (in_array(x1, a) == true) c1 = 1000;
            if (in_array(x2, a) == true) c2 = 100;
            if (in_array(x3, a) == true) c3 = 10;
            if (in_array(x4, a) == true) c4 = 1;
            c = c1 + c2 + c3 + c4;
            if(c == 1111) co =4;
            else if((c ==1110)||(c == 1101)||(c== 1011)||(c==111) ) co =3;
            else if((c==1100)||(c==1001)||(c==11)||(c==1010)||(c==101)||(c == 110)) co =2;
            else co = 1;
            return co;
           }
        }
       
            
    }


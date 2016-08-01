using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;

namespace Reader_Express
{
    public partial class Test : Form
    {
       //Khai bao du lieu thu duoc cua 4 anten luu vao 4 mang 2 chieu
        double[,] data1 = new double[7,6];
        double[,] data2 = new double[7,6];
        double[,] data3 = new double[7,6];
        double[,] data4 = new double[7,6];
        double[] x = { 0, 1, 2, 3, 4, 5 };
        double[] y = { 0, 1, 2, 3, 4, 5, 6 };
        //Ham tinh phan nguyen
        public int int_(double a) 
        {
            int k = 0;
            double min = a;
            for (; k <= a; k++)
            {
                double sub = a - k;
                min = (sub < min) ? sub : min;
            }
            return k-1;
        }

        //Ham noi suy tuyen tinh
        public double Interpol(double[,] mat, double a, double b)
        {
            int a0, b0, a1, b1;
            if (a == 6)
            {
                a0 = 5;
                a1 = 6;
                b0 = int_(b);
                b1 = b0 + 1;
            }
            else
            {
                if (b == 5)
                {
                    a0 = int_(a);
                    a1 = a0 + 1;
                    b0 = 4;
                    b1 = 5;
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
    
        
        //Ham noi suy newton
        /*public double[] diff(double[] a)
        {
            //count++;
            int m = 0;
            double[] diff0 = new double[100];
            for (int k = 0; k < a.Length - 1; k++)
            {
                diff0[m] = a[k + 1] - a[k];
                m++;
            }
            return diff0;
        }
        public double[] diffn(double[] b, int n)
        {
            int k = 1;
            if (n == 0) return b;
            else
            {
                while (k <= n)
                {
                    b = diff(b);
                    k++;
                }
                return b;
            }
        }




        public double[] index(double[] c)
        {
            int m = 1;
            double[] diffe = new double[100];

            diffe[0] = c[0];
            for (int k = 1; k < c.Length; k++)
            {

                diffe[m++] = diffn(c, k)[0];

            }
            return diffe;
        }

        public double func(double[] a, double b, int n)
        {
            double g = 1;
            if (n == -1) return 1;
            else
            {
                for (int k = 0; k <= n; k++)
                {
                    g = g * (b - a[k]);
                }
                return g;
            }

        }
        public double fact(int n)
        {
            double m = 1;
            if (n == 0) return 1;
            else
            {
                for (int k = 1; k <= n; k++)
                {
                    m = m * k;
                }
                return m;
            }
        }
        public double pow(double x, double y)
        {
            double h = 1;
            if (y == 0) return 1;
            else
            {
                for (int k = 1; k <= y; k++)
                {
                    h = h * x;
                }
                return h;
            }
        }

        public double[] cut(double[,] mat,int n)
        {
            double[] mat_x = new double[7];
            for (int b = 0; b < 7; b++)
                mat_x[b] = mat[b, n];
            return mat_x;
        }
        public double Newton(double[] a, double[] b, double z)
        {
            double h = a[1] - a[0];
            double Lx = 0;

            for (int k = 0; k < a.Length; k++)
            {
                Lx = Lx + (index(b)[k] * func(a, z, k - 1)) / (pow(h, k) * fact(k));
                // Lx = Lx + b[k] / ((z - a[k]) * wx(a, k));
            }
            return Lx;

        }
        public double Interpol(double[,] mat, double a, double b)
        {
            double[] newton_y = new double[6];
            for (int c = 0; c < 6; c++)
            newton_y[c] = Newton(y, cut(mat, c), a);
            double res = Newton(x, newton_y, b);
            return res;

        }

        */
        //Ham doi sang gia tri dinh vi
        public int convert_num(double x, double y)
        {
            int num = 0;
            switch (int_(x))
            {
                case 0: num = 1; break;
                case 1: num = 6; break;
                case 2: num = 11; break;
                case 3: num = 16; break;
                case 4: num = 21; break;
                case 5: num = 26; break;
                case 6: num = 26; break;
            }
            for (int k = 0; k != int_(y); k++)
            {
                num = num + 1;
            }
            if (int_(y) == 5) return num;
            else return num + 1;
            }

        //Ham kiem tra phan tu thuoc mang
        public bool in_array(int[] w,int v)
        {
            bool in_ = false;
            for (int p = 0; p < w.Length ; p++)
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
        public int[] local_xy(double[,] mt, double z, double err,double pix)
        {
            int[] local = new int[30];
            int l = 0;
            double inter;
          
            for(int k=0;k<(6/pix);k++)
                for (int n = 0; n <= (5 / pix); n++)
                {
                    inter = Interpol(mt, pix * k, pix * n);
                    if (Math.Abs(z - inter) < err)
                        if (in_array(local, convert_num(pix * k, pix * n)) == false) local[l++] = convert_num(pix * k, pix * n); 
                }
            return local;
        }
        
        //Load form
        public Test()
        {
            InitializeComponent();
            int i = 0, j = 0,k = 0;
            string cs = @"server=localhost;userid=root;
            password=;database=rfidreader";

            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                string stm = "SELECT * FROM training";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    
                    data1[i, j] = rdr.GetDouble(2);
                    data2[i, j] = rdr.GetDouble(3);
                    data3[i, j] = rdr.GetDouble(4);
                    data4[i, j++] = rdr.GetDouble(5);
                    if (j == 6)
                    {
                        i++;
                        j = 0;
                    }
                    k++;
                }

             //   MessageBox.Show("Data loaded");
            }
            catch (MySqlException ex)
            {
                //  Console.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error:" + ex.ToString());
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }
        }
        public void sudungthread()
        {
            int k = 0;


            double rssx1 = double.Parse(textBox1.Text);
            double rssx2 = double.Parse(textBox2.Text);
            double rssx3 = double.Parse(textBox3.Text);
            double rssx4 = double.Parse(textBox4.Text);
            double error = double.Parse(textBox5.Text);
            double pixel = double.Parse(textBox6.Text);
            int[] local_xy1 = local_xy(data1, rssx1, error, pixel);
            int[] local_xy2 = local_xy(data2, rssx2, error, pixel);
            int[] local_xy3 = local_xy(data3, rssx3, error, pixel);
            int[] local_xy4 = local_xy(data4, rssx4, error, pixel);



            while (k < 30)
            {
                if (local_xy1[k] != 0)
                    listView1.Invoke((MethodInvoker)delegate
                    {
                        listView1.Items.Insert(0, local_xy1[k].ToString());
                    });
                if (local_xy2[k] != 0)
                    listView2.Invoke((MethodInvoker)delegate
                    {
                        listView2.Items.Insert(0, local_xy2[k].ToString());
                    });
                if (local_xy3[k] != 0)
                    listView3.Invoke((MethodInvoker)delegate
                    {
                        listView3.Items.Insert(0, local_xy3[k].ToString());
                    });
                if (local_xy4[k] != 0)
                    listView1.Invoke((MethodInvoker)delegate
                {
                    listView4.Items.Insert(0, local_xy4[k].ToString());
                });
                k++;
                Thread.Sleep(2);

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(sudungthread));
            th.ApartmentState = ApartmentState.MTA;
            th.IsBackground = true;
            th.Start();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            listView2.Clear();
            listView3.Clear();
            listView4.Clear();
        }
    }
}


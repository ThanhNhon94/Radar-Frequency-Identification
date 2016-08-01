using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.IO;
namespace Reader_Express
{
    class CipherAlgorithm
    {
        static String connString = "Server=localhost;Database=cryptography;Port=3306;User ID=root;Password=";
        MySqlConnection con = new MySqlConnection(connString);
        MySqlCommand cmd;
        public void OpenConnection()
        {
            try
            {
                con.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Cannot Connected Server: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CloseConnection()
        {
            try
            {
                con.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Cannot Close Connect: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /**************************************Cryptography**************************************************/
        /** Read File Function
         *  Read file from hardist and write each line to List
         * @path : path to directory contain file
         * @return: return list contain each line in file
         */
        private List<String> ReadFile(string path)
        {
            List<string> list_datas = new List<string>();
            StreamReader s_reader = new StreamReader(path);
            String line;
            while ((line = s_reader.ReadLine()) != null)
            {
                list_datas.Add(line);
            }
            s_reader.Close();
            return list_datas;
        }
        /**
         * convertCharToBinary Funtion:
         * this function use to convert a charater to arrays binary 
         * @character: input
         * @list: return from readFile function
         * @return @result: arrays char contain binarys bit
         */
        private char[] convertCharToBinary(char character, List<String> list)
        {
            char[] result = new char[8];
            foreach (string item in list)
            {
                char[] line = item.ToCharArray();
                if (line[0] == character)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        result[j] = line[j + 2];
                    }
                }
            }
            return result;
        }
        /**
         * convertArrayCharToBinary Function
         * this funtion convert arrays characters to two-dimensional arrays contain
         * binarys bit 
         * @characters: arrars contain character input
         * @path: path to directory contain file ascii.txt 
         * @return:@result two-dimensional arrays contain binarys bit  
         */
        private char[,] convertArrayCharToBinary(char[] characters, string path)
        {
            int size = characters.Length;
            List<String> list = ReadFile(path);
            char[] temp = new char[8];
            char[,] result = new char[size, 8];
            for (int i = 0; i < size; i++)
            {
                temp = convertCharToBinary(characters[i], list);
                for (int j = 0; j < 8; j++)
                {
                    result[i, j] = temp[j];
                }
            }
            return result;
        }
        /**
         * substitutionCipherCryptography Function
         * this function use Cryptography from string clear text input to string opaque text with use Substitution Cipher
         * @plane_text : string clear text input 
         * @return: arrays contain character opaque
         */
        public char[] substitutionCipherCryptography(string plane_text)
        {

            char[] p_text_char = plane_text.ToCharArray();
            string keyCode = null;
            Random ran = new Random();
            for (int i = 0; i < plane_text.Length; i++)
            {
                char text_random = (char)ran.Next(33, 126);
                p_text_char[i] = text_random;
                keyCode += Convert.ToString(text_random);
            }
            string query = "INSERT INTO cryptography.keycode(code) VALUE('" + keyCode + "')";
            cmd = new MySqlCommand(query, con);
            MySqlDataReader my_reader;
            try
            {
                OpenConnection();
                my_reader = cmd.ExecuteReader();
                CloseConnection();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Cannot Excute Command: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CloseConnection();
            }
            return p_text_char;
        }
        /**
         * convertBinaryToHex Function
         * this function use to convert arrays binarys bit to Hex
         * @bin : arrays binarys bit input
         * @return: string hex compatible with arrays binarys input
         */
        public string convertBinaryToHex(char[] bin)
        {
            int decimal_ = 0;
            string hex;
            for (int i = 0; i < bin.Length; i++)
            {
                if (bin[i] == '1')
                {
                    decimal_ += (int)Math.Pow(2, 7 - i);
                }
            }
            hex = string.Format("{0:x2}", decimal_);
            return hex;
        }
        /** 
         * transpositionCipherCryptography Function
         * this function use Cryptography this function use Cryptography from string clear text input to string opaque text with use Transposition Cipher
         * @plane_text: string input need Cryptography
         * @path: path to file ascii.txt
         * @return :@list contain hex code Cryptography from string input
         */
        public List<String> transpositionCipherCryptography(string plane_text, string path)
        {
            char[,] binary_data = null;
            List<String> list = new List<string>();
            char tmp;
            char[] binarys = new char[8];
            char[] plane_texts = substitutionCipherCryptography(plane_text);
            binary_data = convertArrayCharToBinary(plane_texts, path);
            for (int i = 0; i < (int)(binary_data.Length / 8); i++)
            {
                for (int j = 0; j < 7; j = j + 2)
                {
                    tmp = binary_data[i, j];
                    binary_data[i, j] = binary_data[i, j + 1];
                    binary_data[i, j + 1] = tmp;
                }
            }
            for (int i_ = 0; i_ < binary_data.Length / 8; i_++)
            {
                for (int j_ = 0; j_ < 8; j_ = j_ + 1)
                {
                    binarys[j_] = binary_data[i_, j_];
                }
                list.Add(convertBinaryToHex(binarys));
            }
            return list;
        }
        /***********************Decryption***************************/
        /**
         * convertHexToDecimal Function
         * this function use convert Hex to decimal 
         * @hex: charater hex input
         * @return:@decimal_ is decimal compatible with hex input 
         */
        public int convertHexToDecimal(char hex)
        {
            int decimal_ = 0;
            switch (hex)
            {
                case '0':
                    {
                        decimal_ = 0;

                    } break;
                case '1':
                    {
                        decimal_ = 1;
                    } break;
                case '2':
                    {
                        decimal_ = 2;

                    } break;
                case '3':
                    {
                        decimal_ = 3;

                    } break;
                case '4':
                    {
                        decimal_ = 4;

                    } break;
                case '5':
                    {
                        decimal_ = 5;

                    } break;
                case '6':
                    {
                        decimal_ = 0;

                    } break;
                case '7':
                    {
                        decimal_ = 7;

                    } break;
                case '8':
                    {
                        decimal_ = 8;

                    } break;
                case '9':
                    {
                        decimal_ = 9;

                    } break;
                case 'a':
                    {
                        decimal_ = 10;

                    } break;
                case 'b':
                    {
                        decimal_ = 11;

                    } break;
                case 'c':
                    {
                        decimal_ = 12;
                    } break;
                case 'd':
                    {
                        decimal_ = 13;

                    } break;

                case 'e':
                    {
                        decimal_ = 14;

                    } break;
                case 'f':
                    {
                        decimal_ = 15;

                    } break;
                default: break;
            }
            return decimal_;
        }
        /**
         *  convertHexToBinary Function is reverse of convertBinaryToHex
         */
        public char[] convertHexToBinary(char dec)
        {
            char[] result = new char[4];
            char temp1 = ' ';
            int i = 3;
            int mod, div;
            div = convertHexToDecimal(dec);
            while (div > 0)
            {
                mod = div % 2;
                div = div / 2;
                if (mod == 1)
                {
                    temp1 = '1';
                }
                if (mod == 0)
                {
                    temp1 = '0';
                }
                result[i] = temp1;
                i--;
            }
            for (int j = i; j >= 0; j--)
            {
                result[j] = '0';
            }
            return result;
        }
        /**
         * convertBinaryToChar Functions
         * this function is reverse function of convertCharToBinary Function
         */
        public char convertBinaryToChar(char[] bin, string path)
        {
            List<String> list;
            char result = ' ';
            list = ReadFile(path);
            string inp1 = null, inp2 = null; ;
            foreach (string item in list)
            {
                char[] line = item.ToCharArray();
                for (int i = 0; i < 8; i++)
                {
                    inp1 += bin[i].ToString();
                    inp2 += line[i + 2].ToString();
                }
                if (inp1 == inp2)
                {
                    result = line[0];
                }
                inp1 = null;
                inp2 = null;
            }
            return result;
        }
        /**
         * substitutionCipherDecryption Function
         * this function is reverse of substitutionCipherCryptography function
         */
        public string substitutionCipherDecryption(char[,] data, string path)
        {
            string result = null;
            char[] temp = new char[8];
            for (int i = 0; i < (int)(data.Length / 8); i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    temp[j] = data[i, j];
                }
                result += convertBinaryToChar(temp, path);

            }
            return result;
        }
        public char[,] transpositionCipherDecryption(List<String> list)
        {
            char[] temp = null;
            char[,] result = new char[list.Count, 8];
            char[] itemChar;
            int count = 0;
            char tmp;
            int k;
            foreach (string item in list)
            {
                k = 0;
                itemChar = item.ToCharArray();

                for (int i = 0; i < itemChar.Length; i++)
                {
                    temp = convertHexToBinary(itemChar[i]);
                    for (int j = 0; j < temp.Length; j++)
                    {
                        result[count, j + k] = temp[j];
                    }
                    k += 4;
                    temp = null;
                }
                count++;
            }
            for (int i_ = 0; i_ < (int)(result.Length / 8); i_++)
            {
                for (int j_ = 0; j_ < 7; j_ = j_ + 2)
                {
                    tmp = result[i_, j_];
                    result[i_, j_] = result[i_, j_ + 1];
                    result[i_, j_ + 1] = tmp;
                }
            }
            return result;
        }

        /************** Giai phuong trinh bac hai**************************/
        /**
         * quadratic function
         * this function use to the quadratic equation
         * @a: coefficient of x^2
         * @b: coefficient of x^1
         * @c: coefficient of x^0
         * @return: @x1, @x2 is 
         */
        public double[] quadraticEquation(double a, double b, double c)
        {
            double[] result = new double[2];
            double x1, x2;
            double delta = Math.Pow(b, 2) - 4 * a * c;
            if (delta > 0)
            {
                x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                result[0] = x1;
                result[1] = x2;
                return result;
            }
            else if (delta == 0)
            {
                x1 = x2 = -(c / a);
                result[0] = x1;
                result[1] = x2;
                return result;
            }
            else
            {
                MessageBox.Show("Phương Trình Vô Nghiệm", "Waning");
                return result;
            }
        }
        /**
         * O1(x1, y1, r1);
         * O2(x2, y2, r2);
         * (x - x1)^2 + (y-y1)^2 = R1^2; (1)
         * (x - x2)^2 + (y-y2)^2 = R2^2; (2)
         * (1) <=> x^2 -2xx1 +x1^2 + y^2 -2*y*y1 + y1^2 = r1^2 <=>x^2 - 2xx1 +y^2 - 2yy1 =r1^2 - x1^2 - y1^2 = a_coefficient
         * (2) <=> ..........................................................................................= b_coefficient
         * (1) - (2) 2*x*(x2-x1) + 2*y*(y2-y1) = a_coefficient - b_coefficient
         *                  ||           ||
         *           c_coefficient     d_coefficient  
         *  => y =(a_coefficient - b_coefficient)/2*d_coefficient -  c_coefficient*x/d_coefficient;
         *                         ||                                           ||
         *                     u_coefficient                                v_coefficient  
         *  => y=? =>(1) =>x=?
         *  e_coefficient, f_coefficient, g_coefficient is coefficient of quadratic Equation from (1);
         */
        public Dictionary<String, Double> circleEquation(double x1, double y1, double r1, double x2, double y2, double r2)
        {
            Dictionary<String, Double> result = new Dictionary<string, double>();
            double a_coefficient = 0;
            double b_coefficient = 0;
            double c_coefficient = 0;
            double d_coefficient = 0;
            double e_coefficient = 0;
            double f_coefficient = 0;
            double g_coefficient = 0;
            double u_coefficient = 0;
            double v_coefficient = 0;
            double[] x_equation = new double[2];
            double[] y_equation = new double[2];
            a_coefficient = Math.Pow(r1, 2) - Math.Pow(x1, 2) - Math.Pow(y1, 2);
            b_coefficient = Math.Pow(r2, 2) - Math.Pow(x2, 2) - Math.Pow(y2, 2);
            c_coefficient = x2 - x1;
            d_coefficient = y2 - y1;
            if (d_coefficient != 0)
            {
                if (c_coefficient == 0)
                {
                    y_equation[0] = y_equation[1] = (a_coefficient - b_coefficient) / (2 * d_coefficient);
                    e_coefficient = 1;
                    f_coefficient = -2 * x1;
                    g_coefficient = Math.Pow(x1, 2) - Math.Pow(r1, 2) + Math.Pow(y_equation[0] - y1, 2);
                    x_equation = quadraticEquation(e_coefficient, f_coefficient, g_coefficient);
                    result.Add("x1", x_equation[0]);
                    result.Add("x2", x_equation[1]);
                    result.Add("y1", y_equation[0]);
                    result.Add("y2", y_equation[1]);
                }
                else
                {
                    u_coefficient = (a_coefficient - b_coefficient) / (2 * d_coefficient);
                    v_coefficient = c_coefficient / d_coefficient;
                    e_coefficient = Math.Pow(v_coefficient, 2) + 1;
                    f_coefficient = -2 * (x1 + v_coefficient * (u_coefficient - y1));
                    g_coefficient = Math.Pow(x1, 2) + Math.Pow(u_coefficient - y1, 2) - Math.Pow(r1, 2);
                    x_equation = quadraticEquation(e_coefficient, f_coefficient, g_coefficient);
                    y_equation[0] = u_coefficient - v_coefficient * x_equation[0];
                    y_equation[1] = u_coefficient - v_coefficient * x_equation[1];
                }
            }
            else
            {
                if (c_coefficient == 0)
                {
                    MessageBox.Show("Hai đường tròn không giao nhau");
                    result.Add("x1", 0);
                    result.Add("x2", 0);
                    result.Add("y1", 0);
                    result.Add("y2", 0);
                }
                else
                {
                    x_equation[0] = x_equation[1] = (a_coefficient - b_coefficient) / (2 * c_coefficient);
                    e_coefficient = 1;
                    f_coefficient = -2 * y1;
                    g_coefficient = Math.Pow(y1, 2) - Math.Pow(r1, 2) + Math.Pow(x_equation[0] - x1, 2);
                    y_equation = quadraticEquation(e_coefficient, f_coefficient, g_coefficient);
                    result.Add("x1", x_equation[0]);
                    result.Add("x2", x_equation[1]);
                    result.Add("y1", y_equation[0]);
                    result.Add("y2", y_equation[1]);
                }
            }
            return result;

        }
    }
}

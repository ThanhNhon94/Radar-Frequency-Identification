using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;

namespace Reader_Express
{
    class KearestAlgorithm
    {
        /*private List<String> ReadFile(string path)
        {
            List<String> list = new List<string>();
            StreamReader sReader = new StreamReader(path);
            string line;
            while ((line = sReader.ReadLine().Trim()) != null)
            {
                list.Add(line);
            }
            sReader.Close();
            return list;
        }
        private List<Double> AutoArrangeClass(string path, double data)
        {
            List<String> list = new List<string>();
            List<Double> list_result = new List<double>();
            list = ReadFile(path);
            foreach (string item in list)
            {
                double temp = data - Double.Parse(item);
                temp = Math.Abs(temp);
                list_result.Add(temp);
            }
            return list_result;
        }
        private List<Dictionary<Double, Int16>> DataTraining(string path)
        {
            List<Dictionary<Double, Int16>> list = new List<Dictionary<double, Int16>>();
            Dictionary<Double, Int16> dictionary = new Dictionary<double,short>();
            StreamReader sReader = new StreamReader(path);
            string line;
            Int16 i = 0;
            while ((line = sReader.ReadLine().Trim()) != null)
            {
                dictionary.Add(Double.Parse(line), i);
                i++;
            };
            i = 0;
            sReader.Close();
            return list;
        }*/
        /**
         * this function find min value in list
         */
        private static int GTNN(List<double> list)
        {
            double min;
            int box = 0;
            min = list[0];          
           for(int i =0; i<list.Count; i++)
            {
                if (list[i] < min)
                {
                    min = list[i];
                    box = i;
                }
            }
            return box;
        }
        /**
         * this function arrange class for data training 
         * @path: path to file data training training.txt
         * @data: data need arrange class
         * @return: class data is arrange
         */ 
        private Int16 AutoArrangeClass(string path, double data)
        {
            List<String> list = new List<string>();
            List<Double> list_result = new List<double>();
            Dictionary<Double, Int16> dictionary = new Dictionary<double, short>();
            Dictionary<Double, Double> dictionary_1 = new Dictionary<double, double>();
            StreamReader sReader = new StreamReader(path);
            string line;
            Int16 i = 1;
            while ((line = sReader.ReadLine()) != null)
            {
                dictionary.Add(Double.Parse(line), i);
                double temp = data - Double.Parse(line);
                temp = Math.Abs(temp);
                list_result.Add(temp);
                dictionary_1.Add(temp, Double.Parse(line));
                i++;
            };
            i = 1;
            double min = GTNN(list_result);
            double result = dictionary_1[min];
            sReader.Close();
            return dictionary[result];
        }
    }
}

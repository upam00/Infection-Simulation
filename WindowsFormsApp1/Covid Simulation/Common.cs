using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Business
{
    class Common
    {
        public static List<int> getMinMax(List<int> l)
        {
            int min, max;
            min = 0;
            max = 0;
            int i;
            for (i = 1; i < l.Count; i++)
            {
                if (l[i] < l[min])
                {
                    min = i;
                }
                if (l[i] > l[max])
                {
                    max = i;
                }

            }

            List<int> rv = new List<int>();
            rv.Add(min);
            rv.Add(max);
            return rv;
        }

        public static List<List<int>> convertToListOfListOfNos(List<List<String>> x)
        {
            List<List<int>> rv = new List<List<int>>();
            List<int> t = null;
            int i, j;
            for (i = 0; i < x.Count; i++)
            {
                t = new List<int>();
                for (j = 0; j < x[i].Count; j++)
                {
                    int ti = Convert.ToInt32(x[i][j]);
                    t.Add(ti);
                }
                rv.Add(t);
            }
            return rv;
        }

        public static List<string> splitString(string given, char delemiter)
        {
            int i;
            List<string> temp = new List<string>();
            int count = given.Length;
            //int flag = 0;
            string var = "";
            string var2;
            char comp;
            for (i = 0; i < count; i++)
            {
                comp = given[i];
                if (comp != delemiter)
                {
                    var2 = given[i].ToString();
                    var = var + var2;
                }
                else
                {
                    temp.Add(var);
                    var = "";

                }

            }

            temp.Add(var);


            return temp;
        }

        public static List<double> splitStringToNOS(string given, char delemiter)
        {
            int i;
            List<string> temp = new List<string>();
            List<Double> x = new List<Double>();
            int count = given.Length;
            //int flag = 0;
            string var = "";
            string var2;
            char comp;
            for (i = 0; i < count; i++)
            {
                comp = given[i];
                if (comp != delemiter)
                {
                    var2 = given[i].ToString();
                    var = var + var2;
                }
                else
                {
                    temp.Add(var);
                    var = "";

                }

            }

            temp.Add(var);
            for(i=0; i<temp.Count; i++)
            {
                x.Add(Convert.ToDouble(temp[i]));
            }

            return x;
        }


    }




}

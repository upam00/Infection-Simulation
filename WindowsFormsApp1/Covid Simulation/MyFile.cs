using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1.Business
{
    class MyFile
    {
        public static List<List<string>> readListOfString(string filename, char delemiter)
        {
            List<List<string>> rv = new List<List<string>>();
            System.IO.StreamReader sr = new System.IO.StreamReader(filename);
            string x = "";
            while (true)
            {
                x = sr.ReadLine();
                if (x == null)
                {
                    break;
                }
                else
                {
                    if (x != "")
                    {
                        rv.Add(Common.splitString(x, delemiter));
                    }

                }
            }

            sr.Close();
            return rv;

        }

        public static void writeListOfList(string filename, List<List<int>> l2, string del)
        {
            string fn = filename;
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(fn);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fn, append: true);
            String g = "";
            int i, j;
            for (i = 0; i < l2.Count; i++)
            {
                for (j = 0; j < l2[i].Count; j++)
                {
                    if (j != l2[i].Count - 1)
                    {
                        g = g + l2[i][j];
                        g = g + del;

                    }
                    else
                    {
                        g = g + l2[i][j];
                    }
                }
                
                sw.WriteLine(g);
                g = "";
            }


            sw.Close();
        }

        public static List<string> readListOfItems(string filename)
        {
            List<string> rv = new List<string>();
            System.IO.StreamReader sr = new System.IO.StreamReader(filename);
            //string y = "SELECT ONE";
            //rv.Add(y);
            string x = "";
            while (true)
            {
                x = sr.ReadLine();
                if (x == null)
                {
                    break;
                }
                else
                {
                    if (x != null)
                    {
                        rv.Add(x);
                        x = "";
                    }

                }
            }

            sr.Close();
            return rv;

        }

        public static void writeListOfListString(String filename, List<List<string>> l2, String del)
        {
            String fileName = filename;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, append:true);
            String g = "";
            int j, i;
            for (i = 0; i < l2.Count; i++)
            {
                for (j = 0; j < l2[i].Count; j++)
                {
                    if (j != l2[i].Count - 1)
                    {
                        g = g + l2[i][j];
                        g = g + del;

                    }
                    else
                    {
                        g = g + l2[i][j];
                    }
                }
                sw.WriteLine(g);
                g = "";
            }

            sw.Close();
        }

        public static void findStringMatch(List<string> toCheck, string filename)
        {
            List<string> database = readListOfItems(filename);
            int k = toCheck.Count;
            int l = database.Count;
            int i, j;
            int count=0;
            string result="";

            for(i=0; i<k; i++)
            {
                for(j=0; j<l; j++)
                {
                    if(String.Compare(toCheck[i].ToLower(),database[j].ToLower())==0)
                    {
                        count = count + 1;
                        result = result + database[j] + "\n";
                    }
                }
            }

            MessageBox.Show(count+" matche(s) found"+"\n"+ result);
           
           
        }

        public static string getSingleString(List<string> l)
        {
            string temp = "";
            int i = 0;
            for(i=0; i<l.Count; i++)
            {
                temp = temp + l[i] + "\n";
            }

            return temp;
        }

        public static string getSingleString(List<string> l, string delemeter)
        {
            string temp = "";
            int i = 0;
            for (i = 0; i < l.Count; i++)
            {
                temp = temp + l[i] + delemeter;
            }
            return temp;
        }

        public static List<string> getUniqueList(List<string> l)
        {
            int i, n;
            n = l.Count;
            List<String> rv = new List<string>();
            if (n > 0)
            {
                string prev = l[0];
                rv.Add(l[0]);
                for (i = 1; i < n; i++)
                {
                    if (string.Compare(l[i], prev) != 0)
                    {
                        prev = l[i];
                        rv.Add(l[i]);
                    }
                }
                return rv;
            }
            else
                return null;
        }
        public static string getSingleString(List<double> l, string delemeter)
        {
            string temp = "";
            int i = 0;
            for (i = 0; i < l.Count; i++)
            {
                temp = temp + l[i] + delemeter;
            }
            return temp;
        }

        public static Color[][] GetBitMapColorMatrix(string filename)
        {
            string bitmapFilePath = filename;
            Bitmap b1 = new Bitmap(bitmapFilePath);

            int hight = b1.Height;
            int width = b1.Width;

            Color[][] colorMatrix = new Color[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new Color[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = b1.GetPixel(i, j);
                    //int val=b1.GetPixel(i, j).R+b1.GetPixel(i,j).G+b1.Get
                }
            }
            return colorMatrix;
        }



    }
}

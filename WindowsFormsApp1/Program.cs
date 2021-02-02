using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Monte_Carlo;



namespace WindowsFormsApp1
{
    //using Hello;
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            @try temp = new @try();
            temp.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(temp);
           
        }
    }
}

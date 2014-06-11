using Et075.View;
using System;
using System.Windows.Forms;

namespace Et075.Controller
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }//end:Main

    }//end:class Program
}

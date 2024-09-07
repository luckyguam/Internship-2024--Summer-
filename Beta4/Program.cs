using Beta4;
using System;
using System.Windows.Forms;

namespace Beta4
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Start the application with the FPathForm
            Application.Run(new FPathForm());
        }
    }
}

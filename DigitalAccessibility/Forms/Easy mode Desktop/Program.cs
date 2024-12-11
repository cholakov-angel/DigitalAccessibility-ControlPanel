using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Easy_mode_Desktop
{
    static class Program
    {
        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Loading());
        } // Main
    } // Program
}

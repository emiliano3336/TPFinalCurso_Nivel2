using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPFinalNivel2_Marchese.UIL.Welcome;

namespace TPFinalNivel2_Marchese
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmConsultaAlrticulo());
            Application.Run(new frmWelcome());
        }
    }
}

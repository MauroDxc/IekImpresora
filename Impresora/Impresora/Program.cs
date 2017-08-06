using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Diagnostics;

namespace Impresora
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new actual());
                
            }
            catch (Exception e)
            {
                //MessageBox.Show("Este equipo se apagara no tiene licencia e="+e.Message);
                //MessageBox.Show("Este equipo se apagará");
                Process proceso = new Process();
                proceso.StartInfo.UseShellExecute = false;
                proceso.StartInfo.RedirectStandardOutput = true;
                proceso.StartInfo.FileName = "shutdown.exe";
                proceso.StartInfo.Arguments = "/p";
                proceso.Start();
            }
        }
    }
}

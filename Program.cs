using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CalculatorForm());
            } catch (Exception e) {
                TextWriter stderr = Console.Error;
                stderr.WriteLine(e.Message);
            } finally {
                Application.Exit();
            }
            
        }
    }
}

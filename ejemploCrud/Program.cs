using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ejemploCrud.vista; // Agrega la referencia al espacio de nombres donde está el formulario Login

namespace ejemploCrud
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Login()); // Cambia Form1() por Login()
            Application.Run(new alumno());
        }
    }
}

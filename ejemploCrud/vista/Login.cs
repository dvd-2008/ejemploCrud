using ejemploCrud.Controlador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ejemploCrud.vista
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnIngresar_MouseClick(object sender, MouseEventArgs e)
        {
            string usuario = txtUsu.Text;
            int contraseña;

            // Intenta convertir la contraseña a un entero
            if (!int.TryParse(txtpass.Text, out contraseña))
            {
                MessageBox.Show("Por favor, ingrese una contraseña válida.");
                return;
            }

            loginController controlador = new loginController();
            bool autenticado = controlador.AutenticarUsuario(usuario, contraseña);

            if (autenticado)
            {
                // Usuario autenticado, redirigir al formulario principal (index)
                indexForm indexForm = new indexForm();
                indexForm.Show();
                this.Hide(); // Ocultar el formulario de inicio de sesión
            }
            else
            {
                // Mostrar un mensaje de error al usuario
                MessageBox.Show("Usuario o contraseña incorrectos. Por favor, inténtelo de nuevo.");
            }
        }

        private void txtUsu_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

        }
    }
}

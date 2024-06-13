using ejemploCrud.Controlador;
using System;
using System.Windows.Forms;

namespace ejemploCrud.vista
{
    public partial class alumno : Form
    {
        // Crear una instancia del controlador de alumnos
        private AlumnoController alumnoController = new AlumnoController();

        public alumno()
        {
            InitializeComponent();
            CargarAlumnos();
        }

        // Método para cargar los alumnos en el DataGridView
        private void CargarAlumnos()
        {
            // Obtener la lista de alumnos del controlador
            var listaAlumnos = alumnoController.ObtenerAlumnos();

            // Asignar la lista de alumnos al DataSource del DataGridView
            dataGridView1.DataSource = listaAlumnos;
        }

        private void btnAgregar_MouseClick(object sender, MouseEventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string dni = txtDNI.Text;

            // Validar los datos antes de agregarlos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(dni))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            // Llamar al método AgregarAlumno del controlador
            alumnoController.AgregarAlumno(nombre, apellido, dni);

            // Mostrar un mensaje de éxito y limpiar los TextBox
            MessageBox.Show("Alumno agregado exitosamente.");
            txtNombre.Clear();
            txtApellido.Clear();
            txtDNI.Clear();
            LimpiarCampos();
            CargarAlumnos();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                txtId.Text = selectedRow.Cells["Id"].Value.ToString();
                txtNombre.Text = selectedRow.Cells["Nombre"].Value.ToString();
                txtApellido.Text = selectedRow.Cells["Apellido"].Value.ToString();
                txtDNI.Text = selectedRow.Cells["DNI"].Value.ToString();
            }
        }

        private void btnEditar_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Verificar si se seleccionó una fila
            {
                // Obtener el ID del alumno seleccionado
                int idAlumno = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                // Obtener los nuevos valores del TextBox
                string nuevoNombre = txtNombre.Text;
                string nuevoApellido = txtApellido.Text;
                string nuevoDNI = txtDNI.Text;

                // Llamar al método EditarAlumno del controlador
                alumnoController.EditarAlumno(idAlumno, nuevoNombre, nuevoApellido, nuevoDNI);

                // Recargar los alumnos en el DataGridView
                LimpiarCampos();
                CargarAlumnos();

                // Mostrar un mensaje de éxito
                MessageBox.Show("Información del alumno editada exitosamente.");
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un alumno para editar.");
            }
        }

        private void alumno_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtDNI.Clear();
        }

    }
}

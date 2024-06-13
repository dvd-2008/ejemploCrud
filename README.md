CONTROLADOR / alumnoController :

using ejemploCrud.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ejemploCrud.Controlador
{
    internal class AlumnoController
    {
        // Método para obtener la lista de alumnos
        public List<Alumno> ObtenerAlumnos()
        {
            List<Alumno> listaAlumnos = new List<Alumno>();

            try
            {
                // Crear una instancia del contexto de datos (DataClasses1DataContext)
                using (var db = new DataClasses1DataContext())
                {
                    // Consultar la base de datos para obtener todos los alumnos
                    var query = from alumno in db.ALUMNO
                                select alumno;

                    // Iterar sobre los resultados y convertirlos en objetos Alumno
                    foreach (var item in query)
                    {
                        // Crear una instancia de Alumno y pasar los argumentos al constructor
                        Alumno alumno = new Alumno(item.Id_Alumno, item.Nombre, item.Apellido, item.Dni);
                        listaAlumnos.Add(alumno);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la consulta
                Console.WriteLine("Error al obtener la lista de alumnos: " + ex.Message);
            }

            return listaAlumnos;
        }


        public void AgregarAlumno(string nombre, string apellido, string dni)
        {
            try
            {
                // Crear una instancia del contexto de datos (DataClasses1DataContext)
                using (var db = new DataClasses1DataContext())
                {
                    // Crear una nueva instancia de ALUMNO
                    var nuevoAlumno = new ALUMNO
                    {
                        Nombre = nombre,
                        Apellido = apellido,
                        Dni = dni
                    };

                    // Agregar la nueva instancia al contexto
                    db.ALUMNO.InsertOnSubmit(nuevoAlumno);

                    // Guardar los cambios en la base de datos
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la inserción
                Console.WriteLine("Error al agregar un nuevo alumno: " + ex.Message);
            }
        }
     


        public void EditarAlumno(int idAlumno, string nuevoNombre, string nuevoApellido, string nuevoDNI)
        {
            try
            {
                // Crear una instancia del contexto de datos (DataClasses1DataContext)
                using (var db = new DataClasses1DataContext())
                {
                    // Buscar el alumno por Id_Alumno
                    var alumnoExistente = db.ALUMNO.SingleOrDefault(a => a.Id_Alumno == idAlumno);

                    if (alumnoExistente != null)
                    {
                        // Actualizar los datos del alumno
                        alumnoExistente.Nombre = nuevoNombre;
                        alumnoExistente.Apellido = nuevoApellido;
                        alumnoExistente.Dni = nuevoDNI;

                        // Guardar los cambios en la base de datos
                        db.SubmitChanges();
                    }
                    else
                    {
                        Console.WriteLine("Alumno no encontrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la actualización
                Console.WriteLine("Error al editar los datos del alumno: " + ex.Message);
            }
        }

    }
}


/////////////////////////////////////////////////////////////

modelo / Alumno :


namespace ejemploCrud.Modelo
{
    internal class Alumno
    {
        public int Id { get; set; } // Propiedad para el Id del alumno
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }

        // Constructor que acepta cuatro argumentos (incluyendo el Id)
        public Alumno(int id, string nombre, string apellido, string dni)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            DNI = dni;
        }
    }
}




vista/ alumno




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

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }
    }
}



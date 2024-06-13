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

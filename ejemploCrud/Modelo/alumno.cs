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

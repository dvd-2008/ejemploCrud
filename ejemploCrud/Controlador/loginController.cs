using ejemploCrud.Modelo;
using System.Linq;

namespace ejemploCrud.Controlador
{
    internal class loginController
    {
        public bool AutenticarUsuario(string usuario, int contraseña)
        {
            // Crear una instancia de tu contexto de datos LINQ to SQL
            using (var contexto = new DataClasses1DataContext())
            {
                // Verificar si existe un usuario con el usuario y contraseña proporcionados
                var usuarioExistente = contexto.LOGIN.FirstOrDefault(u => u.Usuario == usuario && u.Password == contraseña);

                // Si se encuentra un usuario válido, retornar verdadero
                return usuarioExistente != null;
            }
        }
    }
}

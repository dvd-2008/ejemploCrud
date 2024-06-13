using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ejemploCrud.Modelo
{
    public class Login
    {
        [Key]
        public int ID_Usuario { get; set; }

        [Required]
        [StringLength(10)]
        public string Usuario { get; set; }

        [Required]
        public int Password { get; set; }
    }
}


using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Direccion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Ciudad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Pais { get; set; }
        

    }
}

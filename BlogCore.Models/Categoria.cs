using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Categoria
    {
        [Key]
        public int Id{ get; set; }

        [Required(ErrorMessage ="Este campo es requerido")]
        [Display(Name="nombre categoria")]

        public string Nombre { get; set; }

        [Display(Name ="Orden de visualizacion")]
        public int? Orden { get; set; }

        //luego de crear los modelos hay que agregarlos en el APPLICATION DBCONTEXT porque es la que se encarga de crear las migraciones

    }
}

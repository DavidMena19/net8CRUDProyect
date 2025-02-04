using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nombre del Articulo")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Descripcion del Articulo")]
        public string Descripcion { get; set; }
      
        [Display(Name = "Fecha de creacion")]
        public string? FechaCreacion { get; set; }

        [Display(Name = "Imagen")]
        [DataType(DataType.ImageUrl)]
        public string? UrlImagen { get; set; }

        [Required(ErrorMessage ="La categoria es obligatoria")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }


    }
}

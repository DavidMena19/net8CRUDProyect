using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Models
{
    public class Slider
    {
        [Key]
        public int id {  get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nombre Slider")]
        public string Nombre {  get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Estado Slider")]
        public bool Estado {  get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string? UrlImagen {  get; set; }
    }
}

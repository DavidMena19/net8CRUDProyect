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
        
        public string Nombre {  get; set; }

        public bool Estado {  get; set; }
        public string UrlImagen {  get; set; }
    }
}

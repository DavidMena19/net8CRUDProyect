using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Models
{
    public class Carrito
    {
        [Key]
        public int Id { get; set; }
        //cliente id en string porque el id de los usuarios en identiti es en string no int
        public string ClienteId { get; set; }      
        public int? ArticuloId { get; set; }
        [ForeignKey("ArticuloId")]
        public Articulo Articulo { get; set; }
        public List<CarritoDetalle> Detalles { get; set; } = new List<CarritoDetalle>();
        public Decimal Total => Detalles.Sum(d => d.Subtotal);

    }
}

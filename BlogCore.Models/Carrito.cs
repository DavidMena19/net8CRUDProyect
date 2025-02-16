using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Models
{
    public class Carrito
    {
        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public List<CarritoDetalle> Detalles { get; set; } = new List<CarritoDetalle>();
        public Decimal Total => Detalles.Sum(d => d.Subtotal);

    }
}

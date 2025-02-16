using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Models
{
    public class CarritoDetalle
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public Decimal Subtotal => PrecioUnitario * Cantidad;
    }
}

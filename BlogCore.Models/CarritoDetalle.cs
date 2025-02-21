using Microsoft.AspNet.Identity.EntityFramework;
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

        //relacion con el articulo
        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }

        //Relacion Con el usuario
        public string UsuarioId { get; set; }
        public ApplicationUser User { get; set; }

        //Relacion con el carrito
        public int CarritoId { get; set; }
        public Carrito Carrito { get; set; }
        public string Nombre { get; set; }
        public Decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public Decimal Subtotal => PrecioUnitario * Cantidad;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Models.ViewModels
{
    public class HomeVM
    {
        //IEnumerable porque la vista principal llamara una lista tanto de Slider como de Articulos
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Articulo> ListaArticulos { get; set; }
        public IEnumerable<Carrito> Carrito { get; set; }
        public IEnumerable<CarritoDetalle> Detalle { get; set; }
    }
}

using net8Proyect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Data.Data.Repository.IRepository
{
    public interface ICarritoRepository : IRepository<Carrito>
    {
        void Update(Carrito carrito);
        int Count(string usuarioId);
        //void ObtenerCarritoActual(Carrito carrito);
    }
}

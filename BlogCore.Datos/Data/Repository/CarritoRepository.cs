
using net8Proyect.Models;
using net8Proyect.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Data.Data.Repository
{
    public class CarritoRepository : Repository<Carrito>, ICarritoRepository
    {
        private readonly ApplicationDbContext _context;
        public CarritoRepository(ApplicationDbContext contex): base(contex)
        {
            _context = contex;
        }


        //public void ObtenerCarritoActual(Carrito carrito)
        //{
        //    var objDesdeBd = _context.Carrito.FirstOrDefault(u=>u.Id == carrito.Id);

        //}

        public void Update(Carrito carrito)
        {
            var objDesdeDb = _context.Carrito.FirstOrDefault(s => s.Id == carrito.Id);

            objDesdeDb.ClienteId = carrito.ClienteId;
            objDesdeDb.Detalles = carrito.Detalles;
           
            //_context.SaveChanges();

        }
    }
}

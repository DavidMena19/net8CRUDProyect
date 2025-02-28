
using net8Proyect.Models;
using net8Proyect.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Data.Data.Repository
{
    public class CarritoDetalleRepository : Repository<CarritoDetalle>, ICarritoDetalleRepository
    {
        private readonly ApplicationDbContext _context;
        public CarritoDetalleRepository(ApplicationDbContext contex): base(contex)
        {
            _context = contex;
        }


        public void Update(CarritoDetalle detalle)
        {
            var objDesdeDb = _context.CarritoDetalle.FirstOrDefault(s => s.Id == detalle.Id);

            objDesdeDb.PrecioUnitario = detalle.PrecioUnitario;
            objDesdeDb.Cantidad = detalle.Cantidad;
   
            //_context.SaveChanges();

        }
    }
}

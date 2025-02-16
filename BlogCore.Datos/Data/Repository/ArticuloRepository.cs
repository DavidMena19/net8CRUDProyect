
using net8Proyect.Models;
using net8Proyect.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Data.Data.Repository
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticuloRepository(ApplicationDbContext contex): base(contex)
        {
            _context = contex;
        }

        public IQueryable<Articulo> AsQueryable()
        {
            return _context.Set<Articulo>().AsQueryable();
        }

        public void Update(Articulo articulo)
        {
            var objDesdeDb = _context.Articulo.FirstOrDefault(s => s.Id == articulo.Id);
            objDesdeDb.Nombre = articulo.Nombre;
            objDesdeDb.Descripcion = articulo.Descripcion;
            objDesdeDb.UrlImagen = articulo.UrlImagen;
            objDesdeDb.CategoriaId = articulo.CategoriaId;

            //_context.SaveChanges();

        }
    }
}

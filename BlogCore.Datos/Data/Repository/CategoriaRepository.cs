
using BlogCore.Models;
using net8Proyect.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Data.Data.Repository
{
    internal class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoriaRepository(ApplicationDbContext contex): base(contex)
        {
            _context = contex;
        }
        public void Update(Categoria categoria)
        {
            var objDesdeDb = _context.Categoria.FirstOrDefault(s => s.Id == categoria.Id);
            objDesdeDb.Nombre = categoria.Nombre;
            objDesdeDb.Orden = categoria.Orden;

            _context.SaveChanges();

        }
    }
}

using Microsoft.EntityFrameworkCore;
using net8Proyect.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Data.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _context;

        public ContenedorTrabajo(ApplicationDbContext context)
        {
            _context = context;
            Categoria = new CategoriaRepository(_context);
            //Articulo = new ArticuloRepository(_context);
        }

        public ICategoriaRepository Categoria {  get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}


using net8Proyect.Models;
using net8Proyect.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace net8Proyect.Data.Data.Repository
{
    internal class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoriaRepository(ApplicationDbContext contex): base(contex)
        {
            _context = contex;
        }

        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _context.Categoria.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            });
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


using net8Proyect.Models;
using net8Proyect.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Data.Data.Repository
{
    internal class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _context;
        public SliderRepository(ApplicationDbContext contex): base(contex)
        {
            _context = contex;
        }

        public void Update(Slider slider)
        {
            var objetoBd = _context.Slider.FirstOrDefault(i => i.id == slider.id);

            objetoBd.Nombre = slider.Nombre;
            objetoBd.UrlImagen = slider.UrlImagen;
            objetoBd.Estado = slider.Estado;


        }
        //public void Update(Articulo articulo)
        //{
        //    var objDesdeDb = _context.Articulo.FirstOrDefault(s => s.Id == articulo.Id);
        //    objDesdeDb.Nombre = articulo.Nombre;
        //    objDesdeDb.Descripcion = articulo.Descripcion;
        //    objDesdeDb.UrlImagen = articulo.UrlImagen;
        //    objDesdeDb.CategoriaId = articulo.CategoriaId;

        //    _context.SaveChanges();

        //}
    }
}

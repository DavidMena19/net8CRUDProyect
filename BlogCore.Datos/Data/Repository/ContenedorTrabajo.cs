﻿using Microsoft.EntityFrameworkCore;
using net8Proyect.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Articulo = new ArticuloRepository(_context);
            Slider = new SliderRepository(_context);
            Usuario = new UsuarioRepository(_context);
            Carrito = new CarritoRepository(_context);
            CarritoDetalle = new CarritoDetalleRepository(_context);
        }

        public ICategoriaRepository Categoria {  get; private set; }
        public IArticuloRepository Articulo { get; private set; }
        public ISliderRepository Slider { get; private set; }
        public IUsuarioRepository Usuario { get; private set; }
        public ICarritoRepository Carrito { get; private set; }
        public ICarritoDetalleRepository CarritoDetalle {  get; private set; }

       
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net8Proyect.Data.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        //aqui van los repositorios

        ICategoriaRepository Categoria { get; }

        void save();
    }
}

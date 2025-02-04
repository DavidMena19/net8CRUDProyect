﻿using Microsoft.AspNetCore.Mvc.Rendering;
using net8Proyect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace net8Proyect.Data.Data.Repository.IRepository
{
    public interface ICategoriaRepository: IRepository<Categoria>
    {
        void Update(Categoria categoria);
        IEnumerable<SelectListItem> GetListaCategorias();
    }
}

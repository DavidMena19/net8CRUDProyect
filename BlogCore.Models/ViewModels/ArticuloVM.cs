﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace net8Proyect.Models.ViewModels
{
    public class ArticuloVM
    {
        public Articulo Articulo { get; set; }
        public IEnumerable<SelectListItem>? ListaCategoria { get; set; }
    }
}

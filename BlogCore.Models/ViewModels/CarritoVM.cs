﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace net8Proyect.Models.ViewModels
{
    public class CarritoVM
    {
        public IEnumerable<CarritoDetalle> Detalles { get; set; }
        public IEnumerable<Carrito> Carritos { get; set; }
    }
}

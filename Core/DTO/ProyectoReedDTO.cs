﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class ProyectoReedDto
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; } // Cambio de enum a string
    }
}
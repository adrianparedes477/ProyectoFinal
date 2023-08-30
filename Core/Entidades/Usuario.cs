﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Usuario
    {
        public int CodUsuario { get; set; }

        public string Nombre { get; set; }

        public int  Dni { get; set; }

        public TipoUsuario Tipo { get; set; }

        public string Contrasenia { get; set; }

        public enum TipoUsuario
        {
            Admistrador = 1,
            Consultor = 2
        }
    }
}

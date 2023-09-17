﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public int Dni { get; set; }

        [Required(ErrorMessage = "La contraseña  es Requerida")]
        public string Contrasenia { get; set; }
        public string Tipo { get; set; }
    }
}

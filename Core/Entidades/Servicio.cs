﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Servicio
    {
        [Column("codServicio")]
        public int Id { get; set; }

        public string Descr { get; set; }

        public bool Estado { get; set; }

        public decimal ValorHora { get; set; }
    }
}

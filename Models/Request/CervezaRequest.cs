﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceBlazorCrud.Models.Request
{
    public class CervezaRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
    }
}

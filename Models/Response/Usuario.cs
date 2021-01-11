using System;
using System.Collections.Generic;

namespace WebServiceBlazorCrud.Models.Response
{
    public partial class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombres { get; set; }
        public string Apelidos { get; set; }
        public string Correo { get; set; }
        public DateTime Nacimiento { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
    }
}

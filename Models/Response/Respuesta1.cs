using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceBlazorCrud.Models.Response
{
    public class Respuesta1
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public List<Cerveza> Data { get; set; }


        public Respuesta1()
        {
            this.Exito = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPICase.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nome { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
    }
}
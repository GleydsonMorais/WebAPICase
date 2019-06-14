using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPICase.Models
{
    public class Historico
    {
        public int idHistorico { get; set; }
        public string regiao { get; set; }
        public string estado { get; set; }
        public string municipio { get; set; }
        public string revenda { get; set; }
        public string instalacaoCodigo { get; set; }
        public string produto { get; set; }
        public DateTime dataColeta { get; set; }
        public decimal valorCompra { get; set; }
        public decimal valorVenda { get; set; }
        public string undMedida { get; set; }
        public string bandeira { get; set; }
    }
}
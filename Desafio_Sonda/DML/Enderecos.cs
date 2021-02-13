using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Desafio_Sonda.DML
{
    public class Enderecos
    {
        public long id { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public long clienteId { get; set; }
    }
}
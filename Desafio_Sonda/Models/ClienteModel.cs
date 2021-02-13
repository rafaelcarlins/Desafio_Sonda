using Desafio_Sonda.DML;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Desafio_Sonda.Models
{
    public class ClienteModel
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        [Display(Name = "Data Nascimento")]
        public string DtNasc { get; set; }

        public List<Enderecos> ListaEnderecos { get; set; }
    }    
}
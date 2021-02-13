using Desafio_Sonda.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Sonda.Interface
{
    public interface IEndereco
    {
        void Exclui(long IDCliente);
        long Incluir(List<Enderecos> endereco, long IDCliente);
        List<DML.Enderecos> Pesquisa(long ID);
        DML.Enderecos Consultar(string CPF);
    }
}

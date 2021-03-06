﻿using Desafio_Sonda.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Sonda.Interface
{
    public interface IClientes
    {
        int Incluir(Cliente cliente);
        void Alterar(Cliente cliente);
        List<Cliente> PesquisaTodos();
        Cliente Consultar(long id);
        void Excluir(int ID);
    }
}

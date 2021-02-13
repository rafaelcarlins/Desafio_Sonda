using Desafio_Sonda.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Desafio_Sonda.DAL
{
    public class DaoEndereco :IEndereco
    {
        public long Incluir(List<DML.Enderecos> endereco, long clienteId)
        {
            DataSet ds = new DataSet();
            for (int i = 0; i < endereco.Count; i++)
            {
                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
                parametros.Add(new System.Data.SqlClient.SqlParameter("LOGRADOURO", endereco[i].logradouro));
                parametros.Add(new System.Data.SqlClient.SqlParameter("BAIRRO", endereco[i].bairro));
                parametros.Add(new System.Data.SqlClient.SqlParameter("CIDADE", endereco[i].cidade));
                parametros.Add(new System.Data.SqlClient.SqlParameter("ESTADO", endereco[i].estado));
                parametros.Add(new System.Data.SqlClient.SqlParameter("CLIENTE_ID", clienteId));

                ds =  Consultar("SP_IncEndereco", parametros);

            }

            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
            
        }
        public DML.Enderecos Consultar(string CPF)
        {
            DML.Enderecos enderecos = new DML.Enderecos();

            return enderecos;
        }

        public List<DML.Enderecos> Pesquisa(long ID)
        {
            List<DML.Enderecos> list = new List<DML.Enderecos>();
            return list;
        }

        public void Exclui(long IDCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", IDCliente));
            Executar("SP_DeleteEndereco", parametros);

        }


        private string stringDeConexao
        {
            get
            {
                ConnectionStringSettings conn = System.Configuration.ConfigurationManager.ConnectionStrings["Banco"];
                if (conn != null)
                    return conn.ConnectionString;
                else
                    return string.Empty;
            }
        }

        internal void Executar(string NomeProcedure, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(stringDeConexao);
            comando.Connection = conexao;
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = NomeProcedure;
            foreach (var item in parametros)
                comando.Parameters.Add(item);

            conexao.Open();
            try
            {
                comando.ExecuteNonQuery();
            }
            finally
            {
                conexao.Close();
            }
        }

        internal DataSet Consultar(string NomeProcedure, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(stringDeConexao);

            comando.Connection = conexao;
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = NomeProcedure;
            foreach (var item in parametros)
                comando.Parameters.Add(item);

            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            DataSet ds = new DataSet();
            conexao.Open();

            try
            {
                adapter.Fill(ds);
            }
            finally
            {
                conexao.Close();
            }

            return ds;
        }
    }
}
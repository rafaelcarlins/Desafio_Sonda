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
    public class DaoCliente : IClientes 
    {
        public int Incluir(DML.Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("NOME", cliente.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", cliente.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("DATA_NASC", cliente.Data_Nasc));

            DataSet ds = Consultar("SP_IncCliente", parametros);
            int ret = 0;

            if (ds.Tables[0].Rows.Count > 0)
                int.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        public List<DML.Cliente> PesquisaTodos() 
        {
            
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            DataSet ds = Consultar("SP_ConsultarTodosClientes", parametros);
            List<DML.Cliente> cli = Converter(ds);

            return cli;

        }
        public void Excluir(int ID) 
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", ID));

            Executar("SP_DelCliente", parametros);
        }

        public DML.Cliente Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            DataSet ds = Consultar("SP_ConsCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            return cli.FirstOrDefault();
        }

        public void Alterar(DML.Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", cliente.Nome));

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", cliente.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("DATA_NASC", cliente.Data_Nasc));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", cliente.id));

            Executar("SP_AltCliente", parametros);
        }

        private List<DML.Cliente> Converter(DataSet ds)
        {
            List<DML.Cliente> lista = new List<DML.Cliente>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Cliente cli = new DML.Cliente();
                    cli.id = row.Field<int>("ID");
                    cli.Nome = row.Field<string>("NOME");
                    cli.CPF = row.Field<string>("CPF");
                    cli.Data_Nasc = row.Field<string>("Data_Nasc");
                    lista.Add(cli);
                }
            }

            return lista;
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
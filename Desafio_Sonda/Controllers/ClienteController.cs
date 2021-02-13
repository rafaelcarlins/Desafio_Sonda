using Desafio_Sonda.BLL;
using Desafio_Sonda.DML;
using Desafio_Sonda.Interface;
using Desafio_Sonda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desafio_Sonda.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClientes _cliente;
        private readonly IEndereco _endereco;

        public ClienteController(IClientes cliente, IEndereco endereco)
        {
            _cliente = cliente;
            _endereco = endereco;
        }
        public JsonResult Index()
        {
            try
            {
                int qtd = 0;
                

                List<Cliente> clientes = _cliente.Pesquisa();

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                Cliente cliente = new Cliente();

                //bool CPFExistente = bo.VerificarExistencia(model.CPF);
                bool CPFExistente = false;
                if (CPFExistente == false)
                {
                    model.Id = _cliente.Incluir(new Cliente()
                    {
                        Nome = model.Nome,
                        CPF = model.CPF,
                        Data_Nasc = model.DtNasc
                    });
                }
                else
                {
                    return Json("CPF já cadastrado!");
                }

                if (model.ListaEnderecos != null)
                {
                    IncluirEndereco(model.ListaEnderecos, model.Id);
                }

                return Json("Cadastro efetuado com sucesso");
            }
        }
        public void IncluirEndereco(List<Enderecos> endereco, long IDCliente)
        {

            _endereco.Exclui(IDCliente);

            if (endereco[0] != null)
            {
                _endereco.Incluir(endereco, IDCliente);
            }

        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                _cliente.Alterar(new Cliente()
                {
                    id = model.Id,
                    Nome = model.Nome,
                    CPF = model.CPF,
                    Data_Nasc = model.DtNasc
                });

                if (model.ListaEnderecos != null)
                {
                    IncluirEndereco(model.ListaEnderecos, model.Id);
                }

                return Json("Cadastro alterado com sucesso");
            }
        }
    }
}
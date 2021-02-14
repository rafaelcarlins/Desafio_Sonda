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
        public ActionResult Index()
        {
            List<Cliente> clientes = new List<Cliente>();
            ClienteModel clienteModels = new ClienteModel();
            List<ClienteModel> ListClienteModels = new List<ClienteModel>();

            clientes = _cliente.PesquisaTodos();
            for (int i = 0; i < clientes.Count; i++)
            {
                clienteModels.Id = clientes[i].id;
                clienteModels.Nome = clientes[i].Nome;
                clienteModels.CPF = clientes[i].CPF;
                clienteModels.DtNasc = clientes[i].Data_Nasc;
                ListClienteModels.Add(clienteModels);
                clienteModels = new ClienteModel();
            }
            return View(ListClienteModels);

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

        public ActionResult Alterar(long id)
        {
            
            Cliente cliente = _cliente.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.id,
                    Nome = cliente.Nome,
                    CPF = cliente.CPF,
                    DtNasc = cliente.Data_Nasc
                };
            }

            return View(model);
        }

        public ActionResult AlterarCliente(ClienteModel model)
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

                
                return View(model);
            }
        }

        public ActionResult Excluir(int ID)
        {
            _cliente.Excluir(ID);
            return RedirectToAction("Index");
        }
    }
}
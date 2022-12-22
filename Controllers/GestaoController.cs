using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using market.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace market.Controllers
{
    [Route("[controller]/{action=Index}")]
    public class GestaoController : Controller
    {
        private readonly ILogger<GestaoController> _logger;
        private readonly ApplicationDbContext _repo; 
        public GestaoController(
            ILogger<GestaoController> logger,
            ApplicationDbContext context
            )
        {
            _logger = logger;
            _repo = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Categorias(){
             var categorias = _repo.Categorias
                .Where( cat => cat.Status )
                .Select( cat => new Models.Categoria(){Id = cat.Id, Nome = cat.Nome, Status = cat.Status })
                .ToList();
                
            return View(model: categorias);
        }
        
        public IActionResult Fornecedores(){
            var fornecedores = _repo.Fornecedores
                .Where( f => f.Status)
                .Select( f => new Models.Fornecedor(){
                    Id = f.Id,
                    Email = f.Email,
                    Nome = f.Nome,
                    Telefone = f.Telefone,
                    Status = f.Status
                })
                .ToList();
            return View(model:fornecedores);
        }
        public IActionResult Produtos(){
            return View();
        }

        public IActionResult NovaCategoria(){
            return View();
        }
        public IActionResult NovoFornecedor(){
            return View();
        }
        public IActionResult NovoProduto(){
            try 
            {
                SelectListItem opcaoPadrao = new SelectListItem(){Text = "Selecione uma opção", Value=""};
                
                ViewBag.Categorias = _repo.Categorias
                    .Select( cat => new SelectListItem(){ Text = cat.Nome , Value = cat.Id.ToString()}).ToList();
                ViewBag.Categorias.Add(opcaoPadrao);

                ViewBag.Fornecedores = _repo.Fornecedores
                    .Select( f => new SelectListItem() { Text = f.Nome, Value = f.Id.ToString()}).ToList();
                ViewBag.Fornecedores.Add(opcaoPadrao);
                
                return View();
            }
            catch{
                return RedirectToAction("Produtos");
            }

        }
        [HttpGet]
        [Route("[action]/{id?}")]
        public IActionResult EditarCategoria(int id){
            Models.Categoria categoria = _repo.Categorias
                .Where(c => c.Id == id)
                .Select(c => new Models.Categoria() {Id = c.Id , Nome = c.Nome, Status = c.Status})
                .Single();

            return View(model: categoria);
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}
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
    [Route("[controller]")]
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
        
        [Route("categorias")]
        public IActionResult Categorias(){
            return View();
        }
        
        [Route("fornecedores")]
        public IActionResult Fornecedores(){
            return View();
        }
        [Route("produtos")]
        public IActionResult Produtos(){
            return View();
        }

        [Route("novacategoria")]
        public IActionResult NovaCategoria(){
            return View();
        }
        [Route("novofornecedor")]
        public IActionResult NovoFornecedor(){
            return View();
        }
        [Route("novoproduto")]
        public IActionResult NovoProduto(){
            try 
            {
                ViewBag.Categorias = _repo.Categorias
                    .Select( cat => new SelectListItem(){ Text = cat.Nome , Value = cat.Id.ToString()}).ToList();
                ViewBag.Fornecedores = _repo.Fornecedores
                    .Select( f => new SelectListItem() { Text = f.Nome, Value = f.Id.ToString()}).ToList();
                return View();
            }
            catch{
                return RedirectToAction("Produtos");
            }

        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}
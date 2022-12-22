using AutoMapper;
using market.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace market.Controllers
{
    [Route("[controller]/{action=Index}")]
    public class GestaoController : Controller
    {
        private readonly ILogger<GestaoController> _logger;
        private readonly ApplicationDbContext _repo;

        private readonly IMapper _mapper; 
        public GestaoController(
            ILogger<GestaoController> logger,
            ApplicationDbContext context,
            IMapper mapper
            )
        {
            _logger = logger;
            _repo = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Categorias(){
             var categorias = _repo.Categorias
                .Where( cat => cat.Status )
                .Select( cat => _mapper.Map<Models.Categoria>(cat))
                .ToList();
                
            return View(model: categorias);
        }
        
        public IActionResult Fornecedores(){
            var fornecedores = _repo.Fornecedores
                .Where( f => f.Status)
                .Select( f => _mapper.Map<Models.Fornecedor>(f))
                .ToList();
            return View(model:fornecedores);
        }
        public IActionResult Produtos(){
            var produtos = _repo.Produtos
                .Include( p => p.Categoria)
                .Include( p => p.Fornecedor)
                .Where( p => p.Status)
                .Select( p => _mapper.Map<Models.Produto>(p)).ToList();
            return View(model: produtos);
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
        [HttpGet]
        [Route("[action]/{id?}")]
        public IActionResult EditarFornecedor(int id){
            Models.Fornecedor fornecedor = _repo.Fornecedores
                .Where(f => f.Id == id)
                .Select(f => _mapper.Map<Models.Fornecedor>(f))
                .Single();

            return View(model: fornecedor);
        }
        [HttpGet]
        [Route("[action]/{id?}")]
        public IActionResult EditarProduto(int id){
            SelectListItem opcaoPadrao = new SelectListItem(){Text = "Selecione uma opção", Value=""};
                
                ViewBag.Categorias = _repo.Categorias
                    .Select( cat => new SelectListItem(){ Text = cat.Nome , Value = cat.Id.ToString()}).ToList();
                ViewBag.Categorias.Add(opcaoPadrao);

                ViewBag.Fornecedores = _repo.Fornecedores
                    .Select( f => new SelectListItem() { Text = f.Nome, Value = f.Id.ToString()}).ToList();
                ViewBag.Fornecedores.Add(opcaoPadrao);
                
            Models.Produto produtos = _repo.Produtos
                .Include( p => p.Fornecedor)
                .Include( p => p.Categoria)
                .Where(p => p.Id == id)
                .Select(p => _mapper.Map<Models.Produto>(p))
                .Single();

            return View(model: produtos);
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}
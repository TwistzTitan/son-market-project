using AutoMapper;
using market.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using market.Domain.Services;
using market.Domain.Services.Models;
using market.Domain.Services.Common;

namespace market.Controllers
{
    [Route("[controller]/{action=Index}")]
    public class GestaoController : Controller
    {
        private readonly ILogger<GestaoController> _logger;
        private readonly ApplicationDbContext _repo;

        private readonly IServicoEstoque _servicoEstoque;

        private readonly IMapper _mapper; 
        public GestaoController(
            ILogger<GestaoController> logger,
            ApplicationDbContext context,
            IServicoEstoque estoqueService,
            IMapper mapper
            )
        {
            _logger = logger;
            _repo = context;
            _mapper = mapper;
            _servicoEstoque = estoqueService;
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
        public IActionResult Promocoes(){
            var promocoes = _repo.Promocoes
                .Include( p => p.Produto)
                .Where( p => p.Status)
                .Select( p => _mapper.Map<Models.Promocao>(p)).ToList();
            return View(model: promocoes);
        }
        public async Task<IActionResult> Estoques(){
            var serviceResp = await _servicoEstoque.ProdutosDisponiveis() as RespProdutosDisponiveis;
            
            var estoques = serviceResp.Estoques
                .Select( e => _mapper.Map<Models.Estoque>(e)).ToList();
            
            return View(model: estoques);
            
        }

        public IActionResult NovaCategoria(){
            return View();
        }
        public IActionResult NovoFornecedor(){
            return View();
        }
        public IActionResult NovaPromocao(){
            
           ViewBag.Produtos = _repo.Produtos
                .Where(p => p.Status)
                .Select(p => new SelectListItem(){Text = p.Nome, Value = p.Id.ToString()})
                .ToList();
                
            return View();
        }
        public IActionResult NovoEstoque(){
            
           ViewBag.Produtos = _repo.Produtos
                .Where(p => p.Status)
                .Select(p => new SelectListItem(){Text = p.Nome, Value = p.Id.ToString()})
                .ToList();
                
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
            Models.Produto prod = _repo.Produtos
                .Include( p => p.Fornecedor)
                .Include( p => p.Categoria)
                .Where(p => p.Id == id)
                .Select(p => _mapper.Map<Models.Produto>(p))
                .Single();
            SelectListItem opcaoPadrao = new SelectListItem(){Text = "Selecione uma opção", Value=""};
                
                ViewBag.Categorias = _repo.Categorias
                    .Select( cat => new SelectListItem()
                        {   Text = cat.Nome , 
                            Value = cat.Id.ToString(), 
                            Selected = cat.Id == prod.CategoriaID
                        })
                    .ToList();
                ViewBag.Categorias.Add(opcaoPadrao);

                ViewBag.Fornecedores = _repo.Fornecedores
                    .Select( f => new SelectListItem() 
                    {   Text = f.Nome, 
                        Value = f.Id.ToString(), 
                        Selected = f.Id == prod.FornecedorID 
                    })
                    .ToList();
                ViewBag.Fornecedores.Add(opcaoPadrao);
                

            return View(model: prod);
        }
        [HttpGet]
        [Route("[action]/{id?}")]
        public IActionResult EditarPromocao(int id){
            Models.Promocao promocao = _repo.Promocoes
                .Include( p => p.Produto)
                .Where(p => p.Id == id)
                .Select(p => _mapper.Map<Models.Promocao>(p))
                .Single();

           
            SelectListItem opcaoPadrao = new SelectListItem(){Text = "Selecione uma opção", Value=""};
                
            ViewBag.Produtos = _repo.Produtos
                .Select( p => new SelectListItem()
                { 
                    Text = p.Nome , 
                    Value = p.Id.ToString(), 
                    Selected = p.Id == promocao.ProdutoID 
                })
                .ToList();
            ViewBag.Produtos.Add(opcaoPadrao);


            return View(model: promocao);
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}
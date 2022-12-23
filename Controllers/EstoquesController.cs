using AutoMapper;
using market.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entity = market.Domain.Entity.Estoque;
using EstoqueModel = market.Models.Estoque;
namespace market.Controllers
{
    [Route("[controller]/[action]")]
    public class EstoquesController : Controller
    {
        private readonly ApplicationDbContext _repo; 
        private readonly IMapper _mapper;
        private readonly ILogger<EstoquesController> _logger;
        public EstoquesController(
                ILogger<EstoquesController> logger,
                ApplicationDbContext context,
                IMapper mapper
                ){
                _logger = logger;
                _repo = context;
                _mapper = mapper;
        }
            
        [HttpPost]
        public IActionResult Salvar(EstoqueModel model){

                if(!ModelState.IsValid){

                    
                    SelectListItem opcaoPadrao = new SelectListItem(){Text = "Selecione uma opção", Value="", Selected = true, Disabled = true};

                    ViewBag.Produtos = _repo.Produtos
                        .Where( p => p.Status)
                        .Select( p => new SelectListItem(){ Text = p.Nome , Value = p.Id.ToString()}).ToList();
                    ViewBag.Produtos.Add(opcaoPadrao);

                    return View("../Gestao/NovoEstoque");    
                }

                var produto = _repo.Produtos.Find(model.ProdutoID);
                
                if(produto == null)
                    return RedirectToAction("NovoEstoque","Gestao");
                
                Entity estoque = new Entity()
                    {
                        
                        Produto = produto,
                        Quantidade = model.Quantidade,
                        
                    };
                
                _repo.Estoques.Add(estoque);
                _repo.SaveChanges();

                return RedirectToAction("Estoques","Gestao");
        }
        
            
        [HttpPost]
        public IActionResult Editar(EstoqueModel model){

            if(!ModelState.IsValid)
                return View($"../Gestao/EditarEstoque/{model.Id}");
            
            
            var produto = _repo.Produtos.Find(model.ProdutoID);
            
            if(produto == null)
                return RedirectToAction("NovoEstoque","Gestao");
            
            Entity estoque = new Entity(){
                Produto = produto,
                Quantidade = model.Quantidade,
            };
            
            try{
                _repo.Estoques.Update(estoque);
                _repo.SaveChanges();
                return RedirectToAction("Estoques","Gestao");
            }
            catch{
                ViewBag.Message = "Não foi possível atualizar a categoria";

                return RedirectToAction("Estoques","Gestao");   
            }
            
        }
        
    }
}
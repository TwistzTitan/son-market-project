using AutoMapper;
using market.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entity = market.Domain.Entity.Produto;
using ProdutoModel = market.Models.Produto;
namespace market.Controllers
{
    [Route("[controller]/[action]")]
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _repo; 
        private readonly IMapper _mapper;
        private readonly ILogger<ProdutosController> _logger;
        public ProdutosController(
                ILogger<ProdutosController> logger,
                ApplicationDbContext context,
                IMapper mapper
                ){
                _logger = logger;
                _repo = context;
                _mapper = mapper;
        }
            
        [HttpPost]
        public IActionResult Salvar(ProdutoModel model){

                if(!ModelState.IsValid){

                    
                    SelectListItem opcaoPadrao = new SelectListItem(){Text = "Selecione uma opção", Value="", Selected = true, Disabled = true};

                    ViewBag.Categorias = _repo.Categorias
                        .Select( cat => new SelectListItem(){ Text = cat.Nome , Value = cat.Id.ToString()}).ToList();
                    ViewBag.Categorias.Add(opcaoPadrao);

                    ViewBag.Fornecedores = _repo.Fornecedores
                        .Select( f => new SelectListItem() { Text = f.Nome, Value = f.Id.ToString()}).ToList();
                    ViewBag.Fornecedores.Add(opcaoPadrao);
                    

                    return View("../Gestao/NovoProduto");    
                }

                Domain.Entity.Categoria? categoria = _repo.Categorias.Find(model.CategoriaID);
                Domain.Entity.Fornecedor? fornecedor = _repo.Fornecedores.Find(model.FornecedorID);
                
                if(categoria == null || fornecedor == null)
                    return RedirectToAction("NovoProduto","Gestao");
                
                Entity prod = new Entity()
                    {
                        Nome = model.Nome,
                        Categoria = categoria,
                        Fornecedor = fornecedor,
                        PrecoCusto = model.PrecoCusto,
                        PrecoVenda = model.PrecoVenda,
                        Medicao = model.Medicao,
                        Status = true
                    };
                
                _repo.Produtos.Add(prod);
                _repo.SaveChanges();

                return RedirectToAction("Produtos","Gestao");
        }
        
            
        [HttpPost]
        public IActionResult Editar(ProdutoModel model){

            if(!ModelState.IsValid)
                return View($"../Gestao/EditarProduto/{model.Id}");
            
            
            var categoria = _repo.Categorias.Find(model.CategoriaID);
            var fornecedor = _repo.Fornecedores.Find(model.FornecedorID);

            if(categoria == null || fornecedor == null)
                return View($"../Gestao/EditarProduto/{model.Id}");
            

            Entity produto = new Entity(){
                Id = model.Id,
                Nome = model.Nome,
                Categoria = categoria,
                Fornecedor = fornecedor,
                PrecoCusto = model.PrecoCusto,
                PrecoVenda = model.PrecoVenda,
                Medicao = model.Medicao,
                //TODO: Model should get status info
                Status = true,
            };
            
            try{
                _repo.Produtos.Update(produto);
                _repo.SaveChanges();
                return RedirectToAction("Produtos","Gestao");
            }
            catch{
                ViewBag.Message = "Não foi possível atualizar a categoria";

                return RedirectToAction("Produtos","Gestao");   
            }
            
        }
        [HttpPost]
        public IActionResult Deletar(int id){
            
            Entity? produto = _repo.Produtos.Find(id);
            
            if(produto == null)
                return RedirectToAction("Produtos","Gestao");

            produto.Status = false;
            _repo.Produtos.Update(produto);
            _repo.SaveChanges();

            return RedirectToAction("Produtos","Gestao");
        }
    }
}
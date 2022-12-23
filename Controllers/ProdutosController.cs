using AutoMapper;
using market.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entity = market.Domain.Entity.Produto;
using ProdutoModel = market.Models.Produto;
using System.Linq;
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
        public IActionResult Buscar(int id){
            
           
            if(id > 0){
              
              //Verifica se o produto existe em estoque com ou sem promoções
            var query =      from p in _repo.Produtos
                             where p.Id == id && p.Status
                             join e in _repo.Estoques on p.Id equals e.Produto.Id into pestq
                             from p2 in pestq
                             join pr in _repo.Promocoes on p2.Produto.Id equals pr.Produto.Id into promopro
                             from prowithpromo in promopro.DefaultIfEmpty()
                             select new {
                                Produto = p2.Produto,
                                Categoria= p2.Produto.Categoria,
                                Fornecedor = p2.Produto.Fornecedor,
                                PorcentagemPromocao = prowithpromo.Porcentagem,
                                PromoStatus = prowithpromo.Status
                             };
                             
                             
             var produtoQuery =  query.FirstOrDefault();
             

              if(produtoQuery == null){
                Response.StatusCode = 404;
                return Json("Produto indisponível");
              }

              if(produtoQuery.PromoStatus)
                produtoQuery.Produto.PrecoVenda *= (produtoQuery.PorcentagemPromocao); 
            
              produtoQuery.Produto.Categoria = produtoQuery.Categoria;
              produtoQuery.Produto.Fornecedor = produtoQuery.Fornecedor;
              
              ProdutoModel model = _mapper.Map<ProdutoModel>(produtoQuery.Produto);
              Response.StatusCode = 200;
              return Json(model);
            }
            
            Response.StatusCode = 400;
            return Json("Erro na requisição");
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
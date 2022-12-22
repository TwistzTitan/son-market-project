using AutoMapper;
using market.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entity = market.Domain.Entity.Promocao;
using PromocaoModel = market.Models.Promocao;
namespace market.Controllers
{
    [Route("[controller]/[action]")]
    public class PromocoesController : Controller
    {
        private readonly ApplicationDbContext _repo; 
        private readonly IMapper _mapper;
        private readonly ILogger<PromocoesController> _logger;
        public PromocoesController(
                ILogger<PromocoesController> logger,
                ApplicationDbContext context,
                IMapper mapper
                ){
                _logger = logger;
                _repo = context;
                _mapper = mapper;
        }
            
        [HttpPost]
        public IActionResult Salvar(PromocaoModel model){

                if(!ModelState.IsValid){

                    
                    SelectListItem opcaoPadrao = new SelectListItem(){Text = "Selecione uma opção", Value="", Selected = true, Disabled = true};

                    ViewBag.Produtos = _repo.Produtos
                        .Select( p => new SelectListItem(){ Text = p.Nome , Value = p.Id.ToString()}).ToList();
                    ViewBag.Produtos.Add(opcaoPadrao);

                    return View("../Gestao/NovaPromocao");    
                }

                var produto = _repo.Produtos.Find(model.ProdutoID);
                
                if(produto == null)
                    return RedirectToAction("NovaPromocao","Gestao");
                
                Entity promo = new Entity()
                    {
                        Nome = model.Nome,
                        Produto = produto,
                        Porcentagem = model.Porcentagem,
                        Status = true
                    };
                
                _repo.Promocoes.Add(promo);
                _repo.SaveChanges();

                return RedirectToAction("Promocoes","Gestao");
        }
        
            
        [HttpPost]
        public IActionResult Editar(PromocaoModel model){

            if(!ModelState.IsValid)
                return View($"../Gestao/EditarPromocao/{model.Id}");
            
            
            var produto = _repo.Produtos.Find(model.ProdutoID);
            
            if(produto == null)
                return RedirectToAction("NovaPromocao","Gestao");
            
            Entity promo = new Entity(){
                Id = model.Id,
                Produto = produto,
                Nome = model.Nome,
                Porcentagem = model.Porcentagem,
                Status = true,
            };
            
            try{
                _repo.Promocoes.Update(promo);
                _repo.SaveChanges();
                return RedirectToAction("Promocoes","Gestao");
            }
            catch{
                ViewBag.Message = "Não foi possível atualizar a categoria";

                return RedirectToAction("Promocoes","Gestao");   
            }
            
        }
        [HttpPost]
        public IActionResult Deletar(int id){
            
            Entity? promo = _repo.Promocoes.Find(id);
            
            if(promo == null)
                return RedirectToAction("Promocoes","Gestao");

            promo.Status = false;
            _repo.Promocoes.Update(promo);
            _repo.SaveChanges();

            return RedirectToAction("Promocoes","Gestao");
        }
    }
}
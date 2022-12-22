using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CategoriaModel =  market.Models.Categoria;
using Entity = market.Domain.Entity.Categoria;
using market.Data;
namespace market.Controllers
{
    [Route("[controller]/[action]")]
    public class CategoriasController : Controller
    {
        private readonly ILogger<CategoriasController> _logger;
        private readonly ApplicationDbContext _repo;
        public CategoriasController(
            ILogger<CategoriasController> logger,
            ApplicationDbContext context
            )
        {
            _logger = logger;
            _repo = context;
        }

        [HttpPost]
        public IActionResult Salvar(CategoriaModel categoria)
        {
            if(!ModelState.IsValid){
                Debug.Print("Model validation failed: {0} - {1} - {2}", categoria.Nome, categoria.Status, categoria.Id);
                return View("../Gestao/NovaCategoria");
            }
            
            Entity db_categoria = new Entity()
                {   
                    Nome = categoria.Nome,
                    Status = true
                } ;
            
            _repo.Categorias.Add(db_categoria);

            _repo.SaveChanges();

            return RedirectToAction("Categorias","Gestao");
        }

        [HttpPost]
        public IActionResult Editar(CategoriaModel categoriaEdit){

            if(!ModelState.IsValid){
                return View($"../Gestao/EditarCategoria/{categoriaEdit.Id}");
            }
            Entity categoria = new Entity(){
                Id = categoriaEdit.Id,
                Nome = categoriaEdit.Nome,
                Status = categoriaEdit.Status
            };

            try{
                _repo.Categorias.Update(categoria);
                _repo.SaveChanges();
                return RedirectToAction("Categorias","Gestao");
            }
            catch{
                ViewBag.Message = "Não foi possível atualizar a categoria";

                return RedirectToAction("Categorias","Gestao");   
            }
            
        }
    
        [HttpPost]
        public IActionResult Deletar(CategoriaModel categoriaDel){
            
            if(!ModelState.IsValid){

                return RedirectToAction("Categorias","Gestao");
            }

            Entity categoria = new Entity()
            {
                Id = categoriaDel.Id,
                Nome = categoriaDel.Nome,
                Status = false
            };

            _repo.Categorias.Update(categoria);
            _repo.SaveChanges();

            return RedirectToAction("Categorias","Gestao");
        }
    
    }
}
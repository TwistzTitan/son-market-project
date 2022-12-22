using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using market.Models;
using market.Data;
using market.Domain.Entity;
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
        public IActionResult Salvar(Models.Categoria categoria)
        {
            if(!ModelState.IsValid){
                Debug.Print("Model validation failed: {0} - {1} - {2}", categoria.Nome, categoria.Status, categoria.Id);
                return View("../Gestao/NovaCategoria");
            }
            
            Domain.Entity.Categoria db_categoria = new Domain.Entity.Categoria()
                {   
                    Nome = categoria.Nome,
                    Status = true
                } ;
            
            _repo.Categorias.Add(db_categoria);

            _repo.SaveChanges();

            return RedirectToAction("Categorias","Gestao");
        }

        [HttpPost]
        public IActionResult Editar(Models.Categoria categoriaEdit){

            if(!ModelState.IsValid){
                return View($"../Gestao/EditarCategoria/{categoriaEdit.Id}");
            }
            Domain.Entity.Categoria categoria = new Domain.Entity.Categoria(){
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
        public IActionResult Deletar(Models.Categoria categoriaDel){
            
            if(!ModelState.IsValid){

                return RedirectToAction("Categorias","Gestao");
            }

            Domain.Entity.Categoria categoria = new Domain.Entity.Categoria()
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
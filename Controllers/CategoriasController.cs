using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using market.Models;
using market.Data;
using market.Domain.Entity;
namespace market.Controllers
{
    [Route("[controller]")]
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

    }
}
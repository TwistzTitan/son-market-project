using AutoMapper;
using market.Data;
using Microsoft.AspNetCore.Mvc;
using Models = market.Models;
using Entity = market.Domain.Entity;
namespace market.Controllers
{
    [Route("[controller]/[action]")]
    public class FornecedoresController : Controller
    {
        private readonly ILogger<FornecedoresController> _logger;
        private readonly ApplicationDbContext _repo;

        private readonly IMapper _mapper;
        public FornecedoresController(
            ILogger<FornecedoresController> logger,
            ApplicationDbContext context,
            IMapper mapper
            )
        {
            _logger = logger;
            _repo = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Salvar (Models.Fornecedor model){
            if(!ModelState.IsValid){
                return View("../Gestao/NovoFornecedor");
            }

            Entity.Fornecedor fornecedor = _mapper.Map<Entity.Fornecedor>(model);
            fornecedor.Status = true;

            _repo.Fornecedores.Add(fornecedor);
            _repo.SaveChanges();

            return RedirectToAction("Fornecedores","Gestao");
        }

        [HttpPost]
        public IActionResult Editar(Models.Fornecedor model){

            if(!ModelState.IsValid){
                return View($"../Gestao/EditarFornecedor/{model.Id}");
            }
            Entity.Fornecedor fornecedor = _mapper.Map<Entity.Fornecedor>(model);

            try{
                _repo.Fornecedores.Update(fornecedor);
                _repo.SaveChanges();
                return RedirectToAction("Fornecedores","Gestao");
            }
            catch{
                ViewBag.Message = "Não foi possível atualizar a categoria";

                return RedirectToAction("Fornecedores","Gestao");   
            }
            
        }


        public IActionResult Deletar(Models.Fornecedor model){
            
            if(!ModelState.IsValid){

                return RedirectToAction("Fornecedores","Gestao");
            }

            Entity.Fornecedor fornecedor = _mapper.Map<Entity.Fornecedor>(model);
            fornecedor.Status = false;

            _repo.Fornecedores.Update(fornecedor);
            _repo.SaveChanges();

            return RedirectToAction("Fornecedores","Gestao");
        }
    }
}
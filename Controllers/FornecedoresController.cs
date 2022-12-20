using market.Data;
using Microsoft.AspNetCore.Mvc;

namespace market.Controllers
{
    [Route("[controller]")]
    public class FornecedoresController : Controller
    {
        private readonly ILogger<FornecedoresController> _logger;
        private readonly ApplicationDbContext _repo;
        public FornecedoresController(
            ILogger<FornecedoresController> logger,
            ApplicationDbContext context
            )
        {
            _logger = logger;
            _repo = context;
        }

        [HttpPost]
        public IActionResult Salvar (Models.Fornecedor fornecedor){
            if(!ModelState.IsValid){
                return View("../Gestao/NovoFornecedor");
            }

            Domain.Entity.Fornecedor forn = new Domain.Entity.Fornecedor(){
                Nome = fornecedor.Nome,
                Email = fornecedor.Email,
                Telefone = fornecedor.Telefone,
                Status = true
            };

            _repo.Fornecedores.Add(forn);
            _repo.SaveChanges();

            return RedirectToAction("Fornecedores","Gestao");
        }
    }
}
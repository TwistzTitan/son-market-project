using market.Data;
using Microsoft.AspNetCore.Mvc;

namespace market.Controllers
{
    [Route("[controller]")]
    public class ProdutosController : Controller
    {
       private readonly ApplicationDbContext _repo; 
       private readonly ILogger<ProdutosController> _logger;
       public ProdutosController(
            ILogger<ProdutosController> logger,
            ApplicationDbContext context
            ){
            _logger = logger;
            _repo = context;
       }
       
       [HttpPost]
       public IActionResult Salvar(Models.Produto produto){

            if(!ModelState.IsValid){

                return RedirectToAction("NovoProduto","Gestao");
            }

            Domain.Entity.Categoria? categoria = _repo.Categorias.Find(produto.CategoriaID);
            Domain.Entity.Fornecedor? fornecedor = _repo.Fornecedores.Find(produto.FornecedorID);
            
            if(categoria == null || fornecedor == null)
                return RedirectToAction("NovoProduto","Gestao");
            
            Domain.Entity.Produto prod = new Domain.Entity.Produto()
                {
                    Nome = produto.Nome,
                    Categoria = categoria,
                    Fornecedor = fornecedor,
                    PrecoCusto = produto.PrecoCusto,
                    PrecoVenda = produto.PrecoVenda,
                    Medicao = produto.Medicao,
                    Status = true
                };
            
            _repo.Produtos.Add(prod);
            _repo.SaveChanges();

            return RedirectToAction("Produtos","Gestao");
       }
    }
}
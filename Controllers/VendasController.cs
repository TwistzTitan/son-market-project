using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VendasModel = market.Models.Venda;
using SaidaModel = market.Models.Saida;
using ProdutoModel = market.Models.Produto;
using Produto = market.Domain.Entity.Produto;
using Venda = market.Domain.Entity.Venda;
using Saida = market.Domain.Entity.Saida;
using market.Domain.Repository;
using market.Data;
using AutoMapper;

namespace market.Controllers {

    [Route("[controller]/api/v1/[action]")]
    public class VendasController : Controller {

        private readonly IMapper _mapper; 
        private readonly ApplicationDbContext _repo;
        private readonly ILogger _logger;

        public VendasController(
                ILogger<VendasController> logger,
                ApplicationDbContext context,
                IMapper mapper
                ){
                _mapper = mapper;
                _repo = context;
                _logger = logger;

        }

        
        [HttpPost]
        public IActionResult BuscarProdutoVenda(int id) { 
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
                produtoQuery.Produto.PrecoVenda -= produtoQuery.Produto.PrecoVenda * (produtoQuery.PorcentagemPromocao); 
            
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
        public IActionResult FinalizarCompra([FromBody] VendasModel model){
           
          // Processo de finalização de compra
          // Verificar produtos no estoque
          // Gerar saida caso todos os produtos estejam em estoque
          // Gerar venda caso todos produtos tenham a saida realizada
           
           
           
            //TODO: Validação de vendas
            Venda venda = new Venda()
              {
                Total = model.Total,
                Troco = model.Troco,
                ValorPago = model.ValorPago,
                Data = DateTime.Today,

              };
             _repo.Vendas.Add(venda);
             _repo.SaveChanges();

             List<Saida> saidas = new List<Saida>();
             
             foreach(var i in model.Itens){
               
               var s = new Saida(){
                Produto = _repo.Produtos.Find(i.ProdutoId),
                Quantidade = i.Quantidade,
                Valor = i.Preco,
                Venda = venda,
                Data = DateTime.Today
               };

               if(s.Produto == null){
                Response.StatusCode = 404;
                return Json("Produto inválido");
               }
              
              saidas.Add(s);

             }

             _repo.Saidas.AddRange(saidas);
             _repo.SaveChanges();

             //TODO: Remover produtos do estoque

            return Json(new {venda, saidas});

        }

        [HttpPost]
        public IActionResult RelatorioVendas(){
            return Json(_repo.Vendas.ToList());
        }

    }
}
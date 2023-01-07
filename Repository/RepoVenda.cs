using System.Threading.Tasks;
using System.Threading;
using market.Domain.Entity;
using market.Domain.Repository;
using market.Data;
using System.Linq;
using System.Collections.Generic;
using market.Domain.Repository.Models;
namespace market.Repository {

    public class RepoVenda : IRepoVenda
    {
        private readonly ApplicationDbContext _context;
        public RepoVenda(ApplicationDbContext context){
            
            this._context = context;
        }
        
        public override RespSalvarVenda Salvar(Venda data) {
            try{
                _context.Vendas.Add(data);
                _context.SaveChanges();
                return RespSalvarVenda.Sucesso();
            }
            catch{
                return RespSalvarVenda.Erro();
            }
        }

        public override RespObterVenda Obter(int id)
        {
           Venda venda = _context.Vendas.Single(v => v.Id == id);
           
           return new RespObterVenda(venda);
        }

    }


}
using System;
using market.Domain.Services.Models;
using System.Threading.Tasks;

namespace market.Domain.Services {
    public interface IProdutoService {
        Task<RespProdutoBase> Indisponivel(ReqProdutoBase req);
        Task<RespProdutoBase> Criar(ReqProdutoBase req);
        
    }

}
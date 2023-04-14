using System.Threading.Tasks;
using System;
using market.Domain.Services.Models;
namespace market.Domain.Services {
    public interface IServicoEstoque {
        Task<RespEstoqueBase> GerarSaida (ReqEstoqueBase req);
        Task<RespEstoqueBase> EstoquesDisponiveis();
    }

}
using market.Domain.Entity;
using market.Domain.Repository.Common;
using market.Domain.Repository.Models;
using System.Collections.Generic;
namespace market.Domain.Repository{
    

    public interface IGestorRepoEstoque : IRepositoryBase<Estoque> {
        RespDadosBase<Estoque> ObterEstoquesDisponiveis();
    }
}
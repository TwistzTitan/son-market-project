using market.Domain.Entity;
using market.Domain.Repository.Common;
using market.Domain.Repository.Models;
using System.Collections.Generic;
namespace market.Domain.Repository{
    public abstract class IRepoEstoque : IRepositoryBase<Estoque>
    {
         public abstract RespRepoBase Salvar(Estoque estoque);
         public abstract RespRepoBase Obter(int id);
        
    }

    public interface IGestorRepoEstoque{
        IList<Estoque> ObterEstoquesDisponiveis();
    }
}
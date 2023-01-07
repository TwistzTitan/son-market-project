using market.Domain.Entity;
using System.Collections.Generic;
using System;
using market.Domain.Repository.Common;

namespace market.Domain.Repository{

    public abstract class IRepoVenda : IRepositoryBase<Venda>
    {
        public abstract RespRepoBase Salvar(Venda data);
        public abstract RespRepoBase Obter(int id);
     
    }

    // public interface IRepoVendasHandler {
    //     RespRepoBase<T> ObterVendas();
    //     RespRepoBase<T> ObterVendasPorDia();
    // }
}
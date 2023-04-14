using market.Domain.Entity;
using System.Collections.Generic;
using System;
using market.Domain.Repository.Common;

namespace market.Domain.Repository{

    public abstract class IRepoVenda : IRepositoryBase<Venda>
    {
        
        RespSimplesBase<Venda> IRepositoryBase<Venda>.Salvar(Venda data)
        {
            throw new NotImplementedException();
        }

        RespDadosBase<Venda> IRepositoryBase<Venda>.Obter(int id)
        {
            throw new NotImplementedException();
        }
    }

    // public interface IRepoVendasHandler {
    //     RespRepoBase<T> ObterVendas();
    //     RespRepoBase<T> ObterVendasPorDia();
    // }
}
using System.Collections;
using System;
using System.Threading.Tasks;

namespace market.Domain.Repository.Common{
    public interface IRepositoryBase<T>{

        RespSimplesBase<T> Salvar(T data);
        RespDadosBase<T> Obter(int id);

    }

}
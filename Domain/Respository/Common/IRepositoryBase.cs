using System.Collections;
using System;
using System.Threading.Tasks;

namespace market.Domain.Repository.Common{
    public interface IRepositoryBase<T>{

        RespRepoBase Salvar(T data);
        RespRepoBase Obter(int id);

    }

}
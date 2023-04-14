using System.Collections;
using System;
using market.Domain.Entity;
namespace market.Domain.Repository.Common{
    public abstract class RespBase{
        public RepoStatus Status;
    }
    public abstract class RespDadosBase<T> : RespBase {

        public IEnumerable<T> Dados {get; protected set;}
    }

    public abstract class RespSimplesBase<T> : RespBase {
        public T Valor {get; protected set;}
    }
}


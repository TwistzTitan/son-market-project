using market.Domain.Repository.Common;
using market.Domain.Entity;

namespace market.Domain.Repository.Models{
    public class RespRepoEstoque : RespDadosBase<Estoque>
{   
    public IList<Estoque> Dados {get; private set;} = new List<Estoque>(); 

    public RespRepoEstoque(){}
    public RespRepoEstoque(Estoque data, RepoStatus status = RepoStatus.Sucesso)
    {  
        Dados.Add(data);
        Status = status;
    }

    public RespRepoEstoque(IList<Estoque> estoques, RepoStatus status = RepoStatus.Sucesso){
        Dados = estoques;
        Status = status;
    }

}

public class RespSalvarEstoque : RespSimplesBase<Estoque>{

    private RespSalvarEstoque(RepoStatus status, Estoque estoque){
        this.Status = status;
        this.Valor = estoque;
    }

    private RespSalvarEstoque(RepoStatus status)
    {
            this.Status = status;
            this.Valor = new Estoque();
    }

    public static RespSalvarEstoque Erro() => new (RepoStatus.Erro);

    public static RespSalvarEstoque Sucesso(Estoque estoque) => new (RepoStatus.Sucesso,estoque);
}

}
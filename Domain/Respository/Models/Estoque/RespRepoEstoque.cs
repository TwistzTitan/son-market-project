using market.Domain.Repository.Common;
using market.Domain.Entity;

namespace market.Domain.Repository.Models{
    public class RespObterEstoque : RespRepoBase
{   
    public IList<Estoque> Dados {get; private set;} = new List<Estoque>(); 

    public RespObterEstoque(Estoque data, RepoStatus status = RepoStatus.Sucesso)
    {  
        this.Dados.Add(data);
        this.Status = status;
    }

}

public class RespSalvarEstoque : RespRepoBase{

    private RespSalvarEstoque(RepoStatus status){
        this.Status = status;
    }

    public static RespSalvarEstoque Erro() => new RespSalvarEstoque(RepoStatus.Erro);

    public static RespSalvarEstoque Sucesso() => new RespSalvarEstoque(RepoStatus.Sucesso);
}

}
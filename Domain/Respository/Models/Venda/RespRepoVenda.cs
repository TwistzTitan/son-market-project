using market.Domain.Repository.Common;
using market.Domain.Entity;
namespace market.Domain.Repository.Models {

    public class RespObterVenda : RespRepoBase
{   
    public IList<Venda> Dados {get; private set;} = new List<Venda>(); 

    public RespObterVenda(Venda data, RepoStatus status = RepoStatus.Sucesso)
    {  
        this.Dados.Add(data);
        this.Status = status;
    }

}

public class RespSalvarVenda : RespRepoBase{

    private RespSalvarVenda(RepoStatus status){
        this.Status = status;
    }

    public static RespSalvarVenda Erro() => new RespSalvarVenda(RepoStatus.Erro);

    public static RespSalvarVenda Sucesso() => new RespSalvarVenda(RepoStatus.Sucesso);
}


}
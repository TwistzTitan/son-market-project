using market.Domain.Services;
using market.Domain.Repository;
using market.Domain.Repository.Common;
using market.Domain.Services.Models;
using market.Domain.Services.Common;
using System.Threading.Tasks;
using System;
using System.Linq;
public class ServicoEstoque : IServicoEstoque
{   
    private readonly IGestorRepoEstoque _repo;
    public ServicoEstoque(IGestorRepoEstoque repo)
    {   
        this._repo = repo;
    }

    public Task<RespEstoqueBase> GerarSaida(ReqEstoqueBase req){
        throw new NotImplementedException();
    }

    public async Task<RespEstoqueBase> EstoquesDisponiveis(){

       var repoResp = _repo.ObterEstoquesDisponiveis();

      switch(repoResp.Status){
        case RepoStatus.Sucesso:
            return new RespProdutosDisponiveis(repoResp.Dados.ToList());
        case RepoStatus.Erro:
            return RespProdutosDisponiveis.SemEstoque();
        default:
            return RespProdutosDisponiveis.SemEstoque();
      }

    }
}
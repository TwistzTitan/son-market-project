using market.Domain.Services;
using market.Domain.Repository;
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

    public async Task<RespEstoqueBase> ProdutosDisponiveis(){

       var estoques = _repo.ObterEstoquesDisponiveis();
       
       var response = new RespProdutosDisponiveis(estoques);

       response.Status = ServicoStatus.Concluido;
 
       return response;
    }
}
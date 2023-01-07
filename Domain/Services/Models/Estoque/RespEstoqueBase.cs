using System;
using System.Collections;
using market.Domain.Services.Common;
using market.Domain.Entity;
namespace market.Domain.Services.Models {
    public abstract class RespEstoqueBase {
        public ServicoStatus Status {get;set;}

    }

    public class RespGerarSaida : RespEstoqueBase{
        public IList<int> Saidas {get; set;} = new List<int>();
        
    }

    public class RespProdutosDisponiveis : RespEstoqueBase {
        public readonly IList<Estoque> Estoques; 
        public RespProdutosDisponiveis(IList<Estoque> estoques){

            Estoques = estoques;
        }   
    }

}
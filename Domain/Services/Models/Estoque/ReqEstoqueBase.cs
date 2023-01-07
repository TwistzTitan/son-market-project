using System.Collections.Generic;
using System;

namespace market.Domain.Services.Models{
    
    public abstract class ReqEstoqueBase {

    }

    public class ReqGerarSaida : ReqEstoqueBase {

        private IDictionary<int,int> ProdutosQuantidade {get; set;} = new Dictionary<int,int>();

        public ReqGerarSaida(){}

        public void IncluirProdQuantidade(int produtoId, int quant = 0)
        {

            bool argsNotNull = (produtoId != null) && (quant != null);

            if (argsNotNull)
                ProdutosQuantidade[produtoId] += quant;
            else
                throw new ArgumentNullException();

        }
    }

}
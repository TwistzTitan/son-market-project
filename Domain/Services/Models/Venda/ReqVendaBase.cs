using System.Collections.Generic;
using System;
namespace market.Domain.Services.Models
{
    public abstract class ReqVendaBase {
        public IList<int> Saidas;
    }

    public class ReqGerarVenda : ReqVendaBase {

        public ReqGerarVenda(){
            Saidas = new List<int>();
        }
        public void IncluirSaida(int saidaId){
            
            Saidas.Add(saidaId);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Venda = market.Models.Venda;

namespace market.Models
{
    public class Saida
    {
        public int Id {get; set;}
        public int ProdutoId {get; set;}
        public float Preco {get; set;}
        public float Quantidade {get; set;}
        public float Total {get; set;}
        public Venda Venda {get; set;} = new Venda();
        public DateTime Data {get; set;} 

        private Saida ComVenda(Venda venda){
            Venda = Venda;
            Data = DateTime.Now;
            return this;
        }

        private Saida ComItem(Item item){
            Preco = item.Preco;
            ProdutoId = item.ProdutoId;
            Quantidade = item.Quantidade;
            Total = item.Total;
            return this;
        }

        public void Gerar(Venda venda, Item item)=>
            ComItem(item).ComVenda(Venda);
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace market.Models
{
    public class Venda
    {
        public int Id {get; set;}
        public DateTime Data{get; set;}
        public Item [] Itens {get ; set;} = new Item[]{};
        public float Total {get; set;}
        public float ValorPago {get; set;}
        public float Troco{get; set;}
    }

    public class Item {
        public int ProdutoId {get ; set;} 
        public float Preco {get ; set;}
        public int Quantidade {get ; set;}
        public float Total {get => Preco * Quantidade;} 

    }
}
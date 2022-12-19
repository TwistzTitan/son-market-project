using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace market.Models
{
    public class Saida
    {
        public int Id {get; set;}
        public Produto Produto {get; set;}
        public float ValorVenda {get; set;}
        public DateTime Data {get; set;} 
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace market.Models
{
    public class Produto
    {
       public int Id {get; set;}
       public string Nome {get; set;} = String.Empty;
       public Categoria Categoria {get; set;}
       public Fornecedor Fornecedor {get; set;}
       public float PrecoCusto {get; set;}
       public float PrecoVenda {get; set;}
       public int Medicao {get; set;}
       public bool Status{get; set;}
    }

    // public enum MedidaProduto {
    //     Unidade = 0,
    //     KG = 1, 
    //     Litro = 2
    // }
}
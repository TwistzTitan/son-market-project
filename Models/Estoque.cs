using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace market.Models
{
    public class Estoque
    {
        [Required]
        public int Id {get; set;}

        [Required]
        public string Produto {get; set;} = String.Empty;

        public int ProdutoID {get => int.Parse(Produto);}
        public string ProdutoNome {get ; set;} = String.Empty;
        public float Quantidade {get; set;}
    }
}
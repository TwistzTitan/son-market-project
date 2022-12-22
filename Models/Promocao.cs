using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace market.Models
{
    public class Promocao
    {
        [Required]
        public int Id {get; set;}
        [Required]
        [StringLength(100,ErrorMessage = "Nome da promoção inválida, máximo 100 caracteres")]
        [MinLength(2, ErrorMessage = "Nome da promoção inválida, mínimo 2 caracteres")]
        public string Nome {get; set;} = String.Empty;
        [Required]
        [Display(Name = "Produtos")]
        public string Produto {get ; set;} = String.Empty;
        
        public int ProdutoID {get => Produto != null ? int.Parse(Produto) : default ;}
        
        public string ProdutoNome {get; set;} = String.Empty;
        
        [Required]
        [Display(Name = "Porcentagem")]
        [Range(1,50)]
        public float Porcentagem {get; set;}
        public bool Status {get; set;}
    }
}
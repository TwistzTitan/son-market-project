using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace market.Models
{
    public class Produto
    {
       [Required]
       public int Id {get; set;}
       [Required]
       [StringLength(100,ErrorMessage = "Nome do produto inválido, máximo 100 caracteres")]
       [MinLength(2, ErrorMessage = "Nome do produto inválido, mínimo 2 caracteres")]
       public string Nome {get; set;} = String.Empty;
       [Required(ErrorMessage = "Categoria é obrigatória")]
       [Display(Name = "Categorias")]
       public string Categoria {get ; set;} = String.Empty;
       public int CategoriaID {get => Categoria == null ? default : int.Parse(Categoria);}

       [Required(ErrorMessage = "Fornecedor é obrigatório")]
       [Display(Name = "Fornecedores")]
       public string Fornecedor {get; set;} = String.Empty;
       public int FornecedorID {get => Fornecedor == null ? default : int.Parse(Fornecedor);}
       
       [Required(ErrorMessage = "Preco de custo é obrigatório")]
       [Display(Name = "Preço de Custo")]
       public float PrecoCusto {get; set;}
       [Required(ErrorMessage = "Preco de venda é obrigatório")]
       [Display(Name = "Preço de Venda")]
       public float PrecoVenda {get; set;}

       [Required(ErrorMessage = "Medição inválida")]
       [Range(0,2)]
       [Display(Name = "Unidade de medida")]
       public int Medicao {get; set;}
    }

    // public enum MedidaProduto {
    //     Unidade = 0,
    //     KG = 1, 
    //     Litro = 2
    // }
}
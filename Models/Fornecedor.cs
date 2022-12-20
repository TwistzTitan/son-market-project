using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace market.Models
{
    public class Fornecedor
    {
        [Required]
        public int Id {get; set;}
        [Required]
        [StringLength(100,ErrorMessage = "Nome inválido, limite máximo de caracteres 100.")]
        [MinLength(2, ErrorMessage = "Nome inválido, limite mínimo de caracteres 2.")]
        [Display(Name = "Nome do Fornecedor")]
        public string Nome {get; set;} = String.Empty;
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email {get; set;} = String.Empty;
        [Required]
        [RegularExpression(@"^[0-9]{2}.((9[0-9]{4}.[0-9]{4})|(9[0-9]{8}))",ErrorMessage = "Telefone inválido")]
        [StringLength(13,ErrorMessage = "Número inválido")]
        public string Telefone {get; set;} = String.Empty;
        public bool Status {get; set;}
    }
}
using System.ComponentModel.DataAnnotations;

namespace market.Models
{
    public class Categoria
    {
        [Required]
        public int Id {get; set;}
        [Required]
        [Display(Name = "Nome da Categoria")]
        [StringLength(100,ErrorMessage = "Tamanho máximo 100 caracteres.")]
        [MinLength(2, ErrorMessage ="Tamanho mínimo 2 caracteres")]
        public string Nome {get; set;} = String.Empty;
        public bool Status {get; set;} = false;
    }
}
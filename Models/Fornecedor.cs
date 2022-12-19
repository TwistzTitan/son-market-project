using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace market.Models
{
    public class Fornecedor
    {
        public int Id {get; set;}
        public string Nome {get; set;} = String.Empty;
        public string Email {get; set;} = String.Empty;
        public string Telefone {get; set;} = String.Empty;
    }
}
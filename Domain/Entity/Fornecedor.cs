namespace market.Domain.Entity
{
    public class Fornecedor
    {
        public int Id {get; set;}
        public string Nome {get; set;} = String.Empty;
        public string Email {get; set;} = String.Empty;
        public string Telefone {get; set;} = String.Empty;
        public bool Status {get; set;}
    }
}
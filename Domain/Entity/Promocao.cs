namespace market.Domain.Entity
{
    public class Promocao
    {
        public int Id {get; set;}
        public string Nome {get; set;} = String.Empty;
        public Produto Produto {get; set;}
        public float Porcentagem {get; set;}
        public bool Status {get; set;}
    }
}
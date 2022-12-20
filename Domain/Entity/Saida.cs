namespace market.Domain.Entity
{
    public class Saida
    {
        public int Id {get; set;}
        public Produto Produto {get; set;}
        public float ValorVenda {get; set;}
        public DateTime Data {get; set;} 
    }
}
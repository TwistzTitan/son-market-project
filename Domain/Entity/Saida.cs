namespace market.Domain.Entity
{
    public class Saida
    {
        public int Id {get; set;}
        public Produto Produto {get; set;}
        public float Valor {get; set;}
        public float Quantidade {get; set;}
        public Venda Venda {get; set;}
        public DateTime Data {get; set;} 
    }
}
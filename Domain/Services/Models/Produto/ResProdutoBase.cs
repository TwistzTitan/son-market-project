using market.Domain.Services.Common;
namespace market.Domain.Services.Models {
    public abstract class RespProdutoBase {

        public int Id;
        public ServicoStatus Status;
    }

    public class RespProdutoIndisponivel : RespProdutoBase {
        
        RespProdutoIndisponivel(int prodId){
            this.Id = prodId;
            this.Status = ServicoStatus.ProdutoDesativado;
        }
    }

}
using System.Threading.Tasks;
using market.Domain.Services.Models;
 namespace market.Domain.Services {

    public interface IServicoVenda {
       Task<RespVendaBase> GerarVenda(ReqVendaBase req);
    }


}
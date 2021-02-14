using System.Collections.Generic;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_application.Ports.Database
{
    public interface IBuscaModeloPort
    {
        Task<List<T>> BuscaAsync<T>(int id_marca);
    }
}

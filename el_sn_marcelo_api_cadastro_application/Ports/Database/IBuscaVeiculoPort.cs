using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_cadastro_application.Ports.Database
{
    public interface IBuscaVeiculoPort
    {
        Task<List<object>> BuscaPorMarcaAsync(int id_marca);
        Task<T> BuscaAsync<T>(int id);
    }
}

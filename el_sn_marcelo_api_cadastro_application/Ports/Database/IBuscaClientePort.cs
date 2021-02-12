using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_cadastro_application.Ports.Database
{
   public interface IBuscaClientePort
    {
        Task<object> BuscaAsync(string cpf, string senha);
    }
}

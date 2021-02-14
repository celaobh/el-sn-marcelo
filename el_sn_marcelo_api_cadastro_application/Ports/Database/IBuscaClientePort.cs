using System.Threading.Tasks;

namespace el_sn_marcelo_api_application.Ports.Database
{
    public interface IBuscaClientePort
    {
        Task<object> BuscaAsync(string cpf, string senha);
    }
}

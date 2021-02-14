using System.Threading.Tasks;

namespace el_sn_marcelo_api_application.Ports.Database
{
    public interface IBuscaOperadorPort
    {
        Task<object> BuscaAsync(string matricula, string senha);
    }
}

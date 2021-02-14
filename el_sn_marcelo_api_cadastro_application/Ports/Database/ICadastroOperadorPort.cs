using System.Threading.Tasks;

namespace el_sn_marcelo_api_application.Ports.Database
{
    public interface ICadastroOperadorPort
    {
        Task<string> CadastraAsync(string nome, string senha);
    }
}

using System.Threading.Tasks;

namespace el_sn_marcelo_api_application.Ports.Database
{
    public interface ICadastroMarcaPort
    {
        Task CadastraAsync(string nome, string logotipo);
    }
}

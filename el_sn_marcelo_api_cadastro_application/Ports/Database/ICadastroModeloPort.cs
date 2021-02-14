using System.Threading.Tasks;

namespace el_sn_marcelo_api_application.Ports.Database
{
    public interface ICadastroModeloPort
    {
        Task CadastraAsync(string nome, int id_marca);
    }
}

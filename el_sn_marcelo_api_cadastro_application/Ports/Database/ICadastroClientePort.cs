using System.Threading.Tasks;

namespace el_sn_marcelo_api_application.Ports.Database
{
    public interface ICadastroClientePort
    {

        Task<string> CadastraAsync(string nome, string cpf, string aniversario, string senha, string cep, string logradouro, int numero, string complemento, string cidade, string estado);
    }
}

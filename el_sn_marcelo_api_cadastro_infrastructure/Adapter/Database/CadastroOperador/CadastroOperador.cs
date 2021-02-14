using el_sn_marcelo_api_application.Ports.Database;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_infrastructure.Adapter.Database.CadastroOperador
{
    public class CadastroOperador : ICadastroOperadorPort
    {
        private IDatabasePort _database;
        public CadastroOperador(IDatabasePort database)
        {
            _database = database;
        }

        public async Task<string> CadastraAsync(string nome, string senha)
        {
            var retorno = await _database.ExecuteAsync($"INSERT INTO usuario (nome,senha) VALUES('{nome}','{senha}');SELECT CAST(SCOPE_IDENTITY() as int)");
            return retorno.ToString();
        }
    }
}

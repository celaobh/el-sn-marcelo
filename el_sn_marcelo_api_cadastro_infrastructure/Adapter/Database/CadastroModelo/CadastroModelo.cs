using el_sn_marcelo_api_application.Ports.Database;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_infrastructure.Adapter.Database.CadastroOperador
{
    public class CadastroModelo : ICadastroModeloPort
    {
        private IDatabasePort _database;
        public CadastroModelo(IDatabasePort database)
        {
            _database = database;
        }

        public async Task CadastraAsync(string nome, int id_marca)
        {
            await _database.ExecuteAsync($"INSERT INTO modelo (nome,id_marca) VALUES('{nome}','{id_marca}');");
        }
    }
}

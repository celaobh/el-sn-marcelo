using Dapper;
using el_sn_marcelo_api_cadastro_application.Ports.Database;
using el_sn_marcelo_api_cadastro_infrastructure.Models;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_cadastro_infrastructure.Adapter.Database.CadastroOperador
{
    public class CadastroMarca:ICadastroMarcaPort
    {
        private IDatabasePort _database;
        public CadastroMarca(IDatabasePort database)
        {
            _database = database;
        }

        public async Task CadastraAsync(string nome, string logotipo)
        {
            await _database.ExecuteAsync($"INSERT INTO marca (nome,logotipo) VALUES('{nome}','{logotipo}');");
        }
    }
}

using el_sn_marcelo_api_application.Ports.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_infrastructure.Adapter.Database.CadastroOperador
{
    public class BuscaMarca : IBuscaMarcaPort
    {
        private IDatabasePort _database;
        public BuscaMarca(IDatabasePort database)
        {
            _database = database;
        }

        public async Task<List<T>> BuscaAsync<T>()
        {
            List<T> lista = await _database.QueryAsync<T>($"SELECT * FROM marca;");
            return lista;
        }
    }
}

using Dapper;
using el_sn_marcelo_api_cadastro_application.Ports.Database;
using el_sn_marcelo_api_cadastro_infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_cadastro_infrastructure.Adapter.Database.CadastroOperador
{
    public class BuscaModelo:IBuscaModeloPort
    {
        private IDatabasePort _database;
        public BuscaModelo(IDatabasePort database)
        {
            _database = database;
        }

        public async Task<List<T>> BuscaAsync<T>(int id_marca)
        {
            List<T> lista =  await _database.QueryAsync<T>($"SELECT * FROM modelo WHERE id_marca = {id_marca};");
            return lista;
        }
    }
}

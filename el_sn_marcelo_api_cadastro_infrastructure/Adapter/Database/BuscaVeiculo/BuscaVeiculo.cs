using el_sn_marcelo_api_cadastro_application.Ports.Database;
using el_sn_marcelo_api_cadastro_infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace el_sn_marcelo_api_cadastro_infrastructure.Adapter.Database.BuscaOperador
{
    public class BuscaVeiculo : IBuscaVeiculoPort
    {
        IDatabasePort _dapper;
        public BuscaVeiculo(IDatabasePort dapper)
        {
            _dapper = dapper;
        }

        public async Task<List<T>> BuscaPorMarcaAsync<T>(int id_marca)
        {
            return await _dapper.QueryAsync<T>($@"SELECT *  from veiculo WHERE id_marca = '{id_marca}'");
        }

        public async Task<T> BuscaAsync<T>(int id)
        {
            return await _dapper.QueryAsync<T>($@"SELECT *  from veiculo WHERE id = '{id}'");
        }

    }
}

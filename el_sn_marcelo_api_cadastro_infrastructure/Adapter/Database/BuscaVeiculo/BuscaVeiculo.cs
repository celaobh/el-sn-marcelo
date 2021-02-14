using el_sn_marcelo_api_cadastro_application.Ports.Database;
using el_sn_marcelo_api_cadastro_infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Concurrent;
using static Dapper.SqlMapper;

namespace el_sn_marcelo_api_cadastro_infrastructure.Adapter.Database.BuscaOperador
{
    public class BuscaVeiculo : IBuscaVeiculoPort
    {
        IDatabasePort _dapper;
        public BuscaVeiculo(IDatabasePort dapper)
        {
            _dapper = dapper;
        }

        public async Task<List<object>> BuscaPorMarcaAsync(int id_marca)
        {
            var retorno = await _dapper.QueryAsync<Veiculo>($@"SELECT *  from veiculo WHERE id_marca = '{id_marca}'");
            ConcurrentBag<Veiculo> veiculo = new ConcurrentBag<Veiculo>(retorno);
            foreach(var item in veiculo) 
            {
                var queryTwo = $"SELECT * FROM  marca WHERE id = {item.id_marca}";
                var queryThree = $"SELECT * FROM  modelo WHERE id = {item.id_modelo}";
                GridReader gridReader = await _dapper.QueryMultipleAsync(queryTwo + " " + queryThree);
                var marca = gridReader.Read<Marca>().FirstOrDefault();
                var modelo = gridReader.Read<Modelo>().FirstOrDefault();
                item.marca = marca;
                item.modelo = modelo;
            }
         
           

            return veiculo.AsList<object>();
        }

        public async Task<T> BuscaAsync<T>(int id)
        {
            return await _dapper.QueryAsync<T>($@"SELECT *  from veiculo WHERE id = '{id}'");
        }

    }
}

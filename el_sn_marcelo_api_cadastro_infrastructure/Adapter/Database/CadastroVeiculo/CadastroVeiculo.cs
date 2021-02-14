using Dapper;
using el_sn_marcelo_api_cadastro_application.Ports.Database;
using el_sn_marcelo_api_cadastro_infrastructure.Models;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_cadastro_infrastructure.Adapter.Database.CadastroOperador
{
    public class CadastroVeiculo:ICadastroVeiculoPort
    {
        private IDatabasePort _database;
        public CadastroVeiculo(IDatabasePort database)
        {
            _database = database;
        }

        public async Task CadastraAsync(string placa, int id_marca, int id_modelo, int ano, decimal valor_hora, string combustivel, int limite_porta_malas, string categoria, string foto1, string foto2, string foto3)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            await _database.ExecuteAsync($"INSERT INTO veiculo ( placa,  id_marca,  id_modelo, ano, valor_hora, combustivel, limite_porta_malas, categoria, foto1, foto2, foto3) VALUES('{placa}',  {id_marca},  {id_modelo}, {ano}, {valor_hora}, '{combustivel}', {limite_porta_malas}, '{categoria}','{foto1}', '{foto2}', '{foto3}');");
        }
    }
}

using Dapper;
using el_sn_marcelo_api_cadastro_application.Ports.Database;
using el_sn_marcelo_api_cadastro_infrastructure.Models;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_cadastro_infrastructure.Adapter.Database.CadastroCliente
{
    public class CadastroCliente : ICadastroClientePort
    {
        private IDatabasePort _database;
        public CadastroCliente(IDatabasePort database)
        {
            _database = database;
        }

        public async Task<string> CadastraAsync(string nome,string cpf,string aniversario,string senha,string cep,string logradouro,int numero,string complemento,string cidade,string estado)
        {
            var retorno = await _database.ExecuteAsync($"INSERT INTO usuario (nome,cpf,aniversario,senha,cep,logradouro,numero,complemento,cidade,estado) VALUES('{nome}','{cpf}','{aniversario}','{senha}','{cep}','{logradouro}',{numero},'{complemento}','{cidade}','{estado}');SELECT CAST(SCOPE_IDENTITY() as int)");
            return retorno.ToString();
        }

    }
}

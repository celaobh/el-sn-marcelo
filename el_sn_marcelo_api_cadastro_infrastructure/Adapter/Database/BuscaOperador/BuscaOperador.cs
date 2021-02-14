using el_sn_marcelo_api_application.Ports.Database;
using el_sn_marcelo_api_infrastructure.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_infrastructure.Adapter.Database.BuscaOperador
{
    public class BuscaOperador : IBuscaOperadorPort
    {
        IDatabasePort _dapper;
        public BuscaOperador(IDatabasePort dapper)
        {
            _dapper = dapper;
        }

        public async Task<object> BuscaAsync(string matricula, string senha)
        {
            Usuario operador = null;
            //return await _dapper.QueryAsync<T>(@"SELECT id, nome, cpf, matricula, aniversario,cep,logradouro,numero,complemento,cidade,estado from usuario");
            List<Usuario> retorno = await _dapper.QueryAsync<Usuario>($@"SELECT id, nome, matricula  from usuario WHERE id = '{matricula}' AND senha = '{senha}'");
            if (retorno.Any())
            {
                operador = retorno.FirstOrDefault();
                operador.role = "OPERADOR";
            }
            return operador;
        }

    }
}

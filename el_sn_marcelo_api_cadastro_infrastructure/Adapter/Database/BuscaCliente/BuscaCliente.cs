using el_sn_marcelo_api_application.Ports.Database;
using el_sn_marcelo_api_infrastructure.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_infrastructure.Adapter.Database.BuscaCliente
{
    public class BuscaCliente : IBuscaClientePort
    {
        IDatabasePort _dapper;
        public BuscaCliente(IDatabasePort dapper)
        {
            _dapper = dapper;
        }

        public async Task<object> BuscaAsync(string cpf, string senha)
        {

            Usuario cliente = null;
            //return await _dapper.QueryAsync<T>(@"SELECT id, nome, cpf, matricula, aniversario,cep,logradouro,numero,complemento,cidade,estado from usuario");
            List<Usuario> retorno = await _dapper.QueryAsync<Usuario, Endereco, Usuario>($@"SELECT id, nome, cpf, matricula, aniversario,cep,logradouro,numero,complemento,cidade,estado from usuario WHERE cpf = '{cpf}' AND senha = '{senha}'", (p, c) => { p.endereco = c; return p; }, splitOn: "cep");
            if (retorno.Any())
            {
                cliente = retorno.FirstOrDefault();
                cliente.role = "CLIENTE";
            }
            return cliente;
        }

    }
}

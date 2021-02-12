using el_sn_marcelo_api_cadastro_application.Ports.Database;
using el_sn_marcelo_api_cadastro_infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace el_sn_marcelo_api_cadastro_infrastructure.Adapter.Database.BuscaCliente
{
    public class BuscaCliente :IBuscaClientePort
    {
        IDatabasePort _dapper;
        public BuscaCliente(IDatabasePort dapper)
        {
            _dapper = dapper;
        }

        public async Task<object> BuscaAsync(string cpf,string senha)
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

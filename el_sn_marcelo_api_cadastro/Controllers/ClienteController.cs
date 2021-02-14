using el_sn_marcelo_api_application.Ports.Database;
using el_sn_marcelo_api_infrastructure.Models;
using el_sn_marcelo_api_infrastructure.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace el_sn_marcelo_api.Controllers
{
    [Route("[controller]")]
    public class ClienteController : Controller
    {

        private ICadastroClientePort _cadastroCliente;


        public ClienteController(ICadastroClientePort cadastroCliente, IBuscaClientePort buscaCliente, IBuscaOperadorPort buscaOperador)
        {
            _cadastroCliente = cadastroCliente;
        }

        /// <summary>
        /// Cria um usuario
        /// </summary>
        /// <param name="value"></param>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Cliente value)
        {
            Usuario usuario = new Usuario
            {
                aniversario = value.aniversario,
                cpf = value.cpf,
                endereco = new Endereco
                {
                    cep = value.cep,
                    cidade = value.cidade,
                    complemento = value.complemento,
                    estado = value.estado,
                    logradouro = value.logradouro,
                    numero = value.numero
                },
                nome = value.nome,
                senha = value.senha
            };

            Usuario novoUsuario = usuario.CriarHashPassword(usuario.senha);
            return Ok(await _cadastroCliente.CadastraAsync(novoUsuario.nome, novoUsuario.cpf, novoUsuario.aniversario, novoUsuario.senha, novoUsuario.endereco.cep, novoUsuario.endereco.logradouro, novoUsuario.endereco.numero.Value, novoUsuario.endereco.complemento, novoUsuario.endereco.cidade, novoUsuario.endereco.estado));
        }

    }
}

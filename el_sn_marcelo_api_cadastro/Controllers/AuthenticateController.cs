using el_sn_marcelo_api_cadastro_application.Ports.Database;
using el_sn_marcelo_api_cadastro_infrastructure.Models;
using el_sn_marcelo_api_cadastro_infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace el_sn_marcelo_api_cadastro.Controllers
{
    [Route("[controller]")]
    public class AuthenticateController : Controller
    {

        private IBuscaClientePort _buscaCliente;
        private IBuscaOperadorPort _buscaOperador;

        public AuthenticateController(IBuscaClientePort buscaCliente, IBuscaOperadorPort buscaOperador)
        {
            _buscaCliente = buscaCliente;
            _buscaOperador = buscaOperador;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(string cpf, string matricula, string senha)
        {

            Usuario retorno = string.IsNullOrEmpty(matricula) ?
                await _buscaCliente.BuscaAsync(cpf, new Usuario().CriarHashPassword(senha).senha) as Usuario :
                await _buscaOperador.BuscaAsync(matricula, new Usuario().CriarHashPassword(senha).senha) as Usuario;

            if (retorno != null)
            {
                var token = TokenService.GenerateToken(retorno);
                retorno.token = token as string;
                return Ok(new { id = retorno.id, nome = retorno.nome, role = retorno.role, token = token });
            }
            else
            {
                return NotFound();
            }
        }
    }
}

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
    public class OperadorController : Controller
    {

        private ICadastroOperadorPort _cadastroOperador;

        public OperadorController(ICadastroOperadorPort cadastroOperador)
        {
            _cadastroOperador = cadastroOperador;
        }

        /// <summary>
        /// Cria um usuario
        /// </summary>
        /// <param name="value"></param>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Operador value)
        {
            Usuario usuario = new Usuario
            {
                nome = value.nome,
                senha = value.senha
            };

            Usuario novoUsuario = usuario.CriarHashPassword(usuario.senha);
            return Ok(await _cadastroOperador.CadastraAsync(novoUsuario.nome, novoUsuario.senha));
        }

    }
}

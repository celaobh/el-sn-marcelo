using el_sn_marcelo_api_application.Ports.Database;
using el_sn_marcelo_api_infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace el_sn_marcelo_api.Controllers
{
    [Route("[controller]")]
    public class MarcaController : Controller
    {

        private ICadastroMarcaPort _cadastroMarca;
        private IBuscaMarcaPort _buscaMarca;


        public MarcaController(ICadastroMarcaPort cadastroMarca, IBuscaMarcaPort buscaMarca)
        {
            _cadastroMarca = cadastroMarca;
            _buscaMarca = buscaMarca;
        }


        [Authorize(Roles = "OPERADOR")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Marca value)
        {
            await _cadastroMarca.CadastraAsync(value.nome, value.logotipo);
            return Ok();
        }

        [Authorize(Roles = "OPERADOR")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _buscaMarca.BuscaAsync<Marca>());
        }

    }
}

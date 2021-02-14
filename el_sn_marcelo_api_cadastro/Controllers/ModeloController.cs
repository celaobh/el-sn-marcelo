using el_sn_marcelo_api_application.Ports.Database;
using el_sn_marcelo_api_infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace el_sn_marcelo_api.Controllers
{
    [Route("[controller]")]
    public class ModeloController : Controller
    {

        private ICadastroModeloPort _cadastroModelo;
        private IBuscaModeloPort _buscaModelo;


        public ModeloController(ICadastroModeloPort cadastroModelo, IBuscaModeloPort buscaModelo)
        {
            _cadastroModelo = cadastroModelo;
            _buscaModelo = buscaModelo;
        }


        [Authorize(Roles = "OPERADOR")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Modelo value)
        {
            await _cadastroModelo.CadastraAsync(value.nome, value.id_marca);
            return Ok();
        }

        [Authorize(Roles = "OPERADOR")]
        [HttpGet("{id_marca}")]
        public async Task<IActionResult> Get(int id_marca)
        {
            return Ok(await _buscaModelo.BuscaAsync<Modelo>(id_marca));
        }

    }
}

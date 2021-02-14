using el_sn_marcelo_api_cadastro_application.Ports.Database;
using el_sn_marcelo_api_cadastro_infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace el_sn_marcelo_api_cadastro.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private ICadastroVeiculoPort _cadastroVeiculo;
        private IBuscaVeiculoPort _buscaVeiculo;
        public VeiculoController(ICadastroVeiculoPort cadastroVeiculo, IBuscaVeiculoPort buscaVeiculo)
        {
            _cadastroVeiculo = cadastroVeiculo;
            _buscaVeiculo = buscaVeiculo;
        }

   
        [Authorize(Roles = "OPERADOR")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Veiculo value)
        {
            await _cadastroVeiculo.CadastraAsync(value.placa,value.id_marca,value.id_modelo,value.ano,value.valor_hora,value.combustivel,value.limite_porta_malas,value.categoria,value.foto1,value.foto2,value.foto3);
            return Ok();
        }

        [Authorize(Roles = "OPERADOR")]
        [HttpGet("/marca/{id_marca}")]
        public async Task<IActionResult> GetPorMarca(int id_marca)
        {
            return Ok(await _buscaVeiculo.BuscaPorMarcaAsync<Veiculo>(id_marca));
        }

        [Authorize(Roles = "OPERADOR")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id_marca)
        {
            return Ok(await _buscaVeiculo.BuscaAsync<Veiculo>(id_marca));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace el_sn_marcelo_api_cadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {

        public VeiculoController()
        {

        }

        // GET: api/Veiculo
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Veiculo/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Veiculo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Veiculo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

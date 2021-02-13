using el_sn_marcelo_web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace el_sn_marcelo_web.Controllers
{
    public class ModeloController : Controller
    {
        private APIClient _client;

        public ModeloController(APIClient client)
        {
            _client = client;
        }

        public async Task<dynamic> CadastrarModelo(Modelo obj)
        {
            var retorno = await _client.PostModeloAsync(obj);
            if (retorno != null)
                return new { success = true };
            else
                return new { success = false, msg = "Não foi possível cadastrar o modelo." };
        }

        public async Task<dynamic> ListarModelos(int id_marca)
        {
            var retorno = await _client.GetModeloAsync(id_marca);
            if (retorno != null)
            {
                return new { success = true, obj = await JsonSerializer.DeserializeAsync<List<Marca>>(retorno) };
            }
            else
                return new { success = false, msg = "Não foi possível recuperar os modelos." };
        }
    }
}
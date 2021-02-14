using el_sn_marcelo_web.Models;
using el_sn_marcelo_web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace el_sn_marcelo_web.Controllers
{
    public class ModeloController : Controller
    {
        private ModelAPI _modelAPI;

        public ModeloController(ModelAPI modelAPI)
        {
            _modelAPI = modelAPI;
        }

        public async Task<dynamic> CadastrarModelo(Modelo obj)
        {
            var retorno = await _modelAPI.PostModeloAsync(obj);
            if (retorno != null)
                return new { success = true };
            else
                return new { success = false, msg = "Não foi possível cadastrar o modelo." };
        }

        public async Task<dynamic> ListarModelos(int id_marca)
        {
            var retorno = await _modelAPI.GetModeloAsync(id_marca);
            if (retorno != null)
            {
                return new { success = true, obj = await JsonSerializer.DeserializeAsync<List<Marca>>(retorno) };
            }
            else
                return new { success = false, msg = "Não foi possível recuperar os modelos." };
        }
    }
}
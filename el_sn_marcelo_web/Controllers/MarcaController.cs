using el_sn_marcelo_web.Models;
using el_sn_marcelo_web.Services;
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
    public class MarcaController : Controller
    {
        private BrandAPI _brandAPI;

        public MarcaController(BrandAPI brandAPI)
        {
            _brandAPI = brandAPI;
        }

        public async Task<dynamic> CadastrarMarca(Marca obj)
        {
            var retorno = await _brandAPI.PostMarcaAsync(obj);
            if (retorno != null)
                return new { success = true };
            else
                return new { success = false, msg = "Não foi possível cadastrar a marca." };
        }

        public async Task<dynamic> ListarMarcas()
        {
            var retorno = await _brandAPI.GetMarcasAsync();
            if (retorno != null)
            {
                return new { success = true, obj = await JsonSerializer.DeserializeAsync<List<Marca>>(retorno) };
            }
            else
                return new { success = false, msg = "Não foi possível recuperar marcas." };
        }
    }
}
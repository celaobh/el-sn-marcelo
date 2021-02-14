using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using el_sn_marcelo_web.Models;
using el_sn_marcelo_web.Services;
using Microsoft.AspNetCore.Mvc;

namespace el_sn_marcelo_web.Controllers
{
    public class VeiculoController : Controller
    {
        private VehicleAPI _vehicleAPI;
        public VeiculoController(VehicleAPI vehicleAPI)
        {
            _vehicleAPI = vehicleAPI;
        }

        public async Task<dynamic> CadastrarVeiculo(Veiculo obj)
        {
            var retorno = await _vehicleAPI.PostVeiculoAsync(obj);
            if (retorno != null)
                return new { success = true };
            else
                return new { success = false, msg = "Não foi possível cadastrar o veiculo." };
        }
    }
}
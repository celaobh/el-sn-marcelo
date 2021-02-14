using el_sn_marcelo_web.Controllers;
using el_sn_marcelo_web.Models;
using el_sn_marcelo_web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace el_sn_marcelo_web.Pages
{
    public class IndexModel : PageModel
    {
        public List<Veiculo> veiculosBasicos;
        public List<Veiculo> veiculosCompletos;
        public List<Veiculo> veiculosLuxo;
        private VeiculoController _controller;
        private VehicleAPI _vehicleAPI;

        public IndexModel(VehicleAPI vehicleAPI)
        {
            _vehicleAPI = vehicleAPI;
            _controller = new VeiculoController(_vehicleAPI);
            veiculosBasicos = new List<Veiculo>();
            veiculosCompletos = new List<Veiculo>();
            veiculosLuxo = new List<Veiculo>();
        }

        public async Task<IActionResult> OnGet()
        {
            var lista = await _controller.ListarVeiculosAsync();
            lista.Where(b => b.categoria.ToUpper() == "BÁSICO").ToList().ForEach(b => veiculosBasicos.Add(b));
            lista.Where(b => b.categoria.ToUpper() == "COMPLETO").ToList().ForEach(b => veiculosCompletos.Add(b));
            lista.Where(b => b.categoria.ToUpper() == "LUXO").ToList().ForEach(b => veiculosLuxo.Add(b));
            return Page();
        }
    }
}

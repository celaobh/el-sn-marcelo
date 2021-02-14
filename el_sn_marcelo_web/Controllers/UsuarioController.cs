using el_sn_marcelo_web.Models;
using el_sn_marcelo_web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace el_sn_marcelo_web.Controllers
{
    public class UsuarioController : Controller
    {
        private AuthenticateAPI _authenticateAPI;
        private CustomerAPI _customerAPI;

        public UsuarioController(AuthenticateAPI authenticateAPI, CustomerAPI customerAPI)
        {
            _authenticateAPI = authenticateAPI;
            _customerAPI = customerAPI;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<dynamic> CadastrarCliente(Cliente obj)
        {
            obj.cpf = obj.cpf.Replace(".", "").Replace("-", "");
            await _customerAPI.SignUpAsync(obj);
            var retorno = await Autenticar(obj.senha, "", obj.cpf);
            if (retorno.success)
                return new { success = true, url = "/" };
            else
                return new { success = false, msg = "Não foi possível autenticar seu usuário." };

        }

        public async Task<dynamic> CadastrarOperador(Operador obj)
        {
            var matricula = await _customerAPI.SignUpAsync(obj);
            if (matricula != null)
            {
                var retorno = await Autenticar(obj.senha, await JsonSerializer.DeserializeAsync<string>(matricula));
                if (retorno.success)
                    return new { success = true, url = "/" };
                else
                    return new { success = false, msg = "Não foi possível autenticar seu usuário." };
            }
            else
                return new { success = false, msg = "Não foi possível realizar seu cadastro." };

        }

        [HttpPost]
        public async Task<dynamic> Autenticar(string senha, string matricula = "", string cpf = "")
        {
            var retorno = await _authenticateAPI.AuthenticateAsync(cpf.Replace(".", "").Replace("-", ""), senha, matricula);
            if (retorno != null)
            {
                Usuario usuario = await JsonSerializer.DeserializeAsync<Usuario>(retorno);
                var claims = new List<Claim>
                {
                    new Claim("user", usuario.nome),
                    new Claim("id", usuario.id.ToString()),
                    new Claim("role", usuario.role),
                    new Claim("token",usuario.token)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));

                return new { success = true, url = "/" };
            }
            else
            {
                return new { success = false, msg = "Não foi possível autenticar seu usuário." };
            }
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
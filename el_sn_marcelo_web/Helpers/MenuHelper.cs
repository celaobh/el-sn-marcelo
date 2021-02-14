using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace el_sn_marcelo_web.Helpers
{
    public static class MenuHelper
    {
        public static IHtmlContent MostrarCadastros(this IHtmlHelper html)
        {
            string menu = "";
            if (html.ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (html.ViewContext.HttpContext.User.IsInRole("OPERADOR"))
                {
                    menu = @"<li class='nav - item dropdown'>
                               <a class='nav-link dropdown-toggle' href='#' id='navbarDropdown' role='button' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>
                                Cadastros
                            </a>
                            <div class='dropdown-menu' aria-labelledby='navbarDropdown'>
                                <a class='dropdown-item' href='/Cadastros/Marca'>Marcas</a>
                                <a class='dropdown-item' href='/Cadastros/Modelo'>Modelos</a>
                                <a class='dropdown-item' href='/Cadastros/Veiculo'>Veiculos</a>
                            </div>
                        </li>";
                }
            }
            return new HtmlString(menu);
        }

        public static IHtmlContent MostrarUsuario(this IHtmlHelper html)
        {
            if (html.ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var matricula = html.ViewContext.HttpContext.User.IsInRole("OPERADOR") ? $", Matrícula: {html.ViewContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value}" : "";
                var menu = $@"<li class='nav-item'>
                                    <a class='nav-link' href='#'>Olá, {html.ViewContext.HttpContext.User.Identity.Name}{matricula}</a>
                              </li>";
                return new HtmlString(menu);
            }
            else
                return new HtmlString(string.Empty);
        }

        public static IHtmlContent MostrarLogout(this IHtmlHelper html)
        {
            if (html.ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var menu = $@"<li class='nav-item'>
                                    <a class='nav-link' href='../usuario/logout'>Sair</a>
                                </li>";
                return new HtmlString(menu);
            }
            else
                return new HtmlString(string.Empty);
        }


        public static IHtmlContent MostrarLogin(this IHtmlHelper html)
        {
            string menu = "";
            if (!html.ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                menu = @"<li class='nav-item'>
                              <a class='nav-link' href='/Login'>Login</a>
                            </li>";
            }
            return html.Raw(menu);
        }

        public static IHtmlContent MostrarCadastre(this IHtmlHelper html)
        {
            string menu = "";
            if (!html.ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                menu = @"<li class='nav-item'>
                              <a class='nav-link' href='/Cadastro'>Cadastre-se</a>
                            </li>";
            }
            return html.Raw(menu);
        }
    }
}

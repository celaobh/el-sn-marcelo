using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace el_sn_marcelo_web.Pages.Cadastros
{
    [Authorize(Roles = "OPERADOR")]
    public class MarcaModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}

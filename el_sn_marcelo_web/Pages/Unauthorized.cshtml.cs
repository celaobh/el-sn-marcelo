using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace el_sn_marcelo_web.Pages
{
    public class UnauthorizedModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
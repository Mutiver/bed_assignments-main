using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BED_Assignment_3.Pages
{
    [Authorize("Reception")]
    public class ReceptionModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace exercise_4.Pages.Contacts
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = tru)]
        public Contacts ContactsClass { get; set; }
        public void OnGet()
        {
            ContactsClass = new Contacts()
            {
                Name = ContactsClass.Name,
                Email = ContactsClass.Email,
                Phone = ContactsClass.Phone,

            };

        }

        public IActionResult OnPost() 
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("Index", new {Name = ContactsClass.Name, Email = ContactsClass.Email, Phone = ContactsClass.Phone});
        }

    }

    public class Contacts
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }  


    }
}

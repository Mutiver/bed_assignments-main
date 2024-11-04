using BED_Assignment_3.Areas.Identity.Pages.Account;
using BED_Assignment_3.Data;
using BED_Assignment_3.Data.Models;
using BED_Assignment_3.Hub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Build.Framework;

namespace BED_Assignment_3.Pages
{
    [Authorize("Reception")]
    public class OrderedBreakfastModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<KitchenHub, IKitchenHub> _kitchenHub;

        [BindProperty] public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public DateTime Date { get; set; } = DateTime.Today;
            public int Adults { get; set; } = 0;
            public int Children { get; set; } = 0;
        }

        public OrderedBreakfastModel(ApplicationDbContext context, IHubContext<KitchenHub, IKitchenHub> kitchenHub)
        {
            _context = context;
            _kitchenHub = kitchenHub;
            Input = new InputModel();
        }

        public IActionResult OnPostRedirect()
        {
	        return RedirectToPage("ListOfPeopleAteBreakfast");
        }



        public async Task<IActionResult> OnPostAsync()
        {
	        var guestExpected = new GuestExpected
	        {
		        Adults = Input.Adults,
		        Children = Input.Children,
		        Date = Input.Date,
	        };

            _context.GuestsExpected.Add(guestExpected);
            await _context.SaveChangesAsync();
            await _kitchenHub.Clients.All.KitchenUpdate();
            return Page();
        }


        public void OnGet()
        {
        }
    }
}

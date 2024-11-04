using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BED_Assignment_3.Data;
using BED_Assignment_3.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BED_Assignment_3.Pages
{

	[Authorize("Reception")]
    public class ListOfPeopleAteBreakfastModel : PageModel
    {

	    private readonly ApplicationDbContext _context;
	    public string DateNow { get; set; }

	    public List<GuestCheckIn> CheckedIn { get; set; } = new List<GuestCheckIn>();

	    public DisplayModel Display { get; set; }

	    public class DisplayModel
	    {
		    public int RoomNumber { get; set; } = 0;
		    public int Adults { get; set; } = 0;
		    public int Children { get; set; } = 0;
	    }


	    public ListOfPeopleAteBreakfastModel(ApplicationDbContext context)
	    {
			_context = context;
			Display = new DisplayModel();
			DateNow = DateTime.Now.Day + "/" + DateTime.Now.Month;
	    }


		// for when database is set up

		public async Task OnGetAsync()
		{
			var dbGuestCheckIns = await _context.GuestCheckIns
				.Where(g => g.Date.Day == DateTime.Now.Day && g.Date.Month == DateTime.Now.Month).ToListAsync();


			if (dbGuestCheckIns == null)
			{
				RedirectToPage(("Error"));
				return;
			}


			CheckedIn = dbGuestCheckIns;
		}


	}
}

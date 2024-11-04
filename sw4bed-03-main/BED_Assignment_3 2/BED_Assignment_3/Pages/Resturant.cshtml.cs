using BED_Assignment_3.Data;
using BED_Assignment_3.Data.Models;
using BED_Assignment_3.Hub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace BED_Assignment_3.Pages
{
    [Authorize("Waiter")]
    public class RestaurantModel : PageModel
    {
		private ApplicationDbContext _context;
		private IHubContext<KitchenHub, IKitchenHub> _kitchenHub;

		[BindProperty] public InputModel Input { get; set; }

		public class InputModel
		{
			[Required]
			[DataType(DataType.Date)]
			public DateTime Date { get; set; } = DateTime.Today;

			public int RoomNr { get; set; }
			public int Adults { get; set; } = 0;
			public int Children { get; set; } = 0;
		}

		public RestaurantModel(ApplicationDbContext context, IHubContext<KitchenHub, IKitchenHub> kitchenHub)
		{
			_context = context;
			Input = new InputModel();
			_kitchenHub = kitchenHub;
		}
		public async Task<IActionResult> OnPostAsync()
		{
			var guestCheckIn = new GuestCheckIn
			{
				Adults = Input.Adults,
				Children = Input.Children,
				Date = Input.Date,
				RoomNr = Input.RoomNr
				
			};

			_context.GuestCheckIns.Add(guestCheckIn);
			await _context.SaveChangesAsync();
			await _kitchenHub.Clients.All.KitchenUpdate();
			return Page();
		}
		public void OnGet() {}
	}
}

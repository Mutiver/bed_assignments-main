    using BED_Assignment_3.Data;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using BED_Assignment_3.Data;
    using BED_Assignment_3.Hub;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    namespace BED_Assignment_3.Pages
    {
        [Authorize("Kitchen")]
        public class KitchenModel : PageModel
        {
            private readonly ApplicationDbContext _context;
            private readonly IHubContext<KitchenHub, IKitchenHub> _hubContext;

            public KitchenModel(ApplicationDbContext context, IHubContext<KitchenHub, IKitchenHub> hubContext)
            {
                _context = context;
                _hubContext = hubContext;
            }
            
            public int TotalExpectedGuests { get; set; }
            public int TotalAdultsExpected { get; set; }
            public int TotalChildrenExpected { get; set; }

            public int TotalCheckedInAdults { get; set; }
            public int TotalCheckedInChildren { get; set; }

            public int RemainingAdults { get; set; }
            public int RemainingChildren { get; set; }

            public async Task OnGetAsync()
            {
                TotalAdultsExpected = _context.GuestsExpected.Sum(g => g.Adults);
                TotalChildrenExpected = _context.GuestsExpected.Sum(g => g.Children);
                TotalExpectedGuests = TotalAdultsExpected + TotalChildrenExpected;
                
                TotalCheckedInAdults = _context.GuestCheckIns.Sum(g => g.Adults);
                TotalCheckedInChildren = _context.GuestCheckIns.Sum(g => g.Children);

                RemainingAdults = TotalAdultsExpected - TotalCheckedInAdults;
                RemainingChildren = TotalChildrenExpected - TotalCheckedInChildren;

                return;
            }
        }
    }

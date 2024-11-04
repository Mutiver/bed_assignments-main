using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using BED_Assignment_3.Data.Models;

namespace BED_Assignment_3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
	    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		    : base(options) { }

	    public DbSet<ReceptionUser>? ReceptionUsers { get; set; } 
            public DbSet<KitchenUser>? KitchenUsers { get; set; }

            public DbSet<WaiterUser> WaiterUsers { get; set; }

            public DbSet<GuestCheckIn> GuestCheckIns { get; set; }

            public DbSet<GuestExpected> GuestsExpected { get; set; }
	
    }
}
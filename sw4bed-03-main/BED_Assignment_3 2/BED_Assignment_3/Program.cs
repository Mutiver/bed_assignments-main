using BED_Assignment_3.Data;
using BED_Assignment_3.Hub;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Change to "DefaultConnection" to use localDB
var connectionString = builder.Configuration.GetConnectionString("SqlContainer") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
	{
		options.SignIn.RequireConfirmedAccount = false;
		options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 4;
        options.Password.RequiredUniqueChars = 0;
	})
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Reception",
        policyBuilder => policyBuilder.RequireClaim("ReceptionUser"));
    options.AddPolicy("Waiter",
        policyBuilder => policyBuilder.RequireClaim("WaiterUser"));
    options.AddPolicy("Kitchen",
        policyBuilder => policyBuilder.RequireClaim("KitchenUser"));
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();    

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<KitchenHub>("/KitchenHub");

app.Run();



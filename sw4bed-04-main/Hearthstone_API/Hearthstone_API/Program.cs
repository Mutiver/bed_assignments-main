using Hearthstone_API.Models;
using Hearthstone_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoDbSettings>(
	builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<SeedData>();
builder.Services.AddSingleton<CardService>();

var app = builder.Build();

//seeding data
bool seed = true;
if (seed)
{
	using (var scope = app.Services.CreateScope())
	{
		var services = scope.ServiceProvider;

		var seedDataType = services.GetRequiredService<SeedData>();
		seedDataType.CreateSets();
		seedDataType.CreateClasses();
		seedDataType.CreateRarities();
		seedDataType.CreateCardTypes();
		seedDataType.CreateCards();
	}
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

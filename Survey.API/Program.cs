using Survey.DataAccess.Context;
using Survey.DataAccess.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Send Fake Data

using var scope = builder.Services.BuildServiceProvider().CreateScope();
var provider = scope.ServiceProvider;
var dbcontext = provider.GetRequiredService<AppDbContext>();
await SeedData.Seeding(dbcontext);


var app = builder.Build();

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

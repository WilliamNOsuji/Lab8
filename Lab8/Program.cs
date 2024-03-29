using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lab8.Data;
using Lab8.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Lab8Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Lab8Context") ?? throw new InvalidOperationException("Connection string 'Lab8Context' not found.")));

//builder.Services.AddScoped<AnimalService>();
builder.Services.AddScoped<GenericService<Animal>>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow all", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allow all");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

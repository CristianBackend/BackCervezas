using Backend.Dtos;
using Backend.Models;
using Backend.Services;
using Backend.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddSingleton<IPeopleService,PeopleServices>();

//builder.Services.AddScoped<IPostServices,PostServices>();

//builder.Services.AddHttpClient<IPostServices,PostServices>(
//    x => x.BaseAddress = new Uri(builder.Configuration["BaseUrlPost"]));


builder.Services.AddDbContext<StoredContext>(x => x.UseSqlServer(
    
    builder.Configuration.GetConnectionString("StoredConnection")
    ));


//validadores
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();

builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();



//services 

builder.Services.AddScoped<IBeerServices, BeerServices>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

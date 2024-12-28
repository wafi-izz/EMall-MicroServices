using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderServiceDbContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<IProductService, ProductService.Services.ProductService>(Client =>
{
    Client.BaseAddress = new Uri("https://localhost:7119/");
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

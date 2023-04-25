using Microsoft.EntityFrameworkCore;
using Orders.WebApi.Abstractions;
using Orders.WebApi.DataAccessLayer;
using Orders.WebApi.Domain.Orders.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(
    x => x.UseNpgsql(builder.Configuration.GetConnectionString("OrdersWebApiDbConnection")));

builder.Services.AddControllers();
builder.Services.AddTransient<IDataStore, EfDataStore>();
builder.Services.AddTransient<OrderService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
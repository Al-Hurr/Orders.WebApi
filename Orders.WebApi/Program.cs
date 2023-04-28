using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Orders.WebApi.Abstractions;
using Orders.WebApi.Common;
using Orders.WebApi.DataAccessLayer;
using Orders.WebApi.Domain.Orders.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(
    x => x.UseNpgsql(builder.Configuration.GetConnectionString("OrdersWebApiDbConnection")));

builder.Services.AddControllers();
builder.Services.AddTransient<IDataStore, EfDataStore>();
builder.Services.AddTransient<OrderService>();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidation();
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

var app = builder.Build();

{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
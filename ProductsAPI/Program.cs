using Microsoft.EntityFrameworkCore;
using ProductsAPI;
using ProductsAPI.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("PaymentDetailDB");
builder.Services.AddDbContextPool<PaymentDetailDBContext>(option => option.UseSqlServer(connectionString));

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



app.UseMyMiddleware();
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Hello World! my middleware");
//    await next();
//});

app.UseHttpsRedirection();

app.MapControllers();
app.UseAuthorization();


app.Run();

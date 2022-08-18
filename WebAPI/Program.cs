global using FastEndpoints;
global using FluentValidation;

using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;

var builder = WebApplication.CreateBuilder();
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlServer(builder.Configuration.GetSection("ConnectionString").Value));
builder.Services.AddTransient<ICreator, Creator>();
builder.Services.AddFastEndpoints();

var app = builder.Build();
app.UseAuthorization();
app.UseFastEndpoints();
app.Run();
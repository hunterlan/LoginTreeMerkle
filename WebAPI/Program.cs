global using FastEndpoints;
global using FluentValidation;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;

var builder = WebApplication.CreateBuilder();
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlServer(builder.Configuration.GetSection("ConnectionString").Value));
builder.Services.AddTransient<ICreator, Creator>();
builder.Services.AddFastEndpoints();
builder.Services.AddAuthenticationJWTBearer(builder.Configuration.GetSection("JWTSigninKey").Value);

var app = builder.Build();
app.UseAuthorization();
app.UseFastEndpoints();
app.Run();
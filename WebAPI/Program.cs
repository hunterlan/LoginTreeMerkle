global using FastEndpoints;
global using FluentValidation;

using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder();
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlServer(builder.Configuration.GetSection("ConnectionString").Value));
builder.Services.AddTransient<ICreator, Creator>();
builder.Services.AddFastEndpoints();
builder.Services.AddAuthenticationJWTBearer(builder.Configuration.GetSection("JWTSigninKey").Value);
builder.Services.AddSwaggerDoc(settings =>
{
    settings.Title = "Merkle tree login API";
    settings.Version = "v1";
});

var app = builder.Build();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());
app.Run();
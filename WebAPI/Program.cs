global using FastEndpoints;
global using FluentValidation;

using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using FastEndpoints.Swagger;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());
app.Run();
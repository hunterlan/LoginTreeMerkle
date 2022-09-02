using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RequestsDTO.Users;
using WebAPI.ResponsesDTO.Users;

namespace WebAPI.Endpoints.Users;

public class LoginUserEndpoint : Endpoint<LoginUserRequest>
{
    private readonly ApplicationContext _context;
    private readonly ICreator _creator;

    public LoginUserEndpoint(ApplicationContext context, ICreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public override void Configure()
    {
        Post("/api/user/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginUserRequest req, CancellationToken ct)
    {
        Logger.LogInformation($"User {req.Login} is trying to login");
        User? foundUser = null;
        List<UserData> userDatas = null;

        try
        {
            foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == req.Login, cancellationToken: ct);
            userDatas = _context.UserData.Where(u => u.Key.Equals(req.Key)).ToList();
        }
        catch (Exception ex)
        {
            Logger.LogError($"Exception during login of user {req.Login}: {ex.Message}");
            var exResponse = new ErrorResponse
            {
                Message = ex.Message,
                StatusCode = StatusCodes.Status500InternalServerError
            };
            await SendAsync(exResponse, exResponse.StatusCode, ct);
            return;
        }

        if (foundUser != null && userDatas.Any())
        {
            UserData foundUserData = new();
            bool isAuthSuccess = false;

            foreach (var data in userDatas)
            {
                var currentMerkleHashTree = _creator.CreateMerkleHashTree(data, req.Login, req.Password, Config.GetSection("Salt").Value);
                if (foundUser.MarkleHashTree.Equals(currentMerkleHashTree))
                {
                    foundUserData = data;
                    isAuthSuccess = true;
                    break;
                }
            }

            if (isAuthSuccess)
            {
                Logger.LogInformation("Successful attempt.");
                var response = new LoginUserResponse()
                {
                    Data = foundUserData,
                    Login = foundUser.Login,
                    Token = JWTBearer.CreateToken(
                        signingKey: Config.GetSection("JWTSigninKey").Value,
                        expireAt: DateTime.UtcNow.AddHours(1),
                        claims: new[] { ("Login", foundUser.Login), ("Email", foundUserData.Email) })
                };
                await SendAsync(response, cancellation: ct);
                return;
            }
        }

        Logger.LogInformation("Failed attempt.");
        var errorResponse = new ErrorResponse
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Message = "Invalid credentials sent!"
        };
        await SendAsync(errorResponse, StatusCodes.Status400BadRequest, ct);
    }
}
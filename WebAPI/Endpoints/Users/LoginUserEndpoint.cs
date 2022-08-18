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
        User? foundUser = null;
        List<UserData> userDatas = null;

        try
        {
            foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == req.Login, cancellationToken: ct);
            userDatas = _context.UserData.Where(u => u.HashMD5.Equals(req.DataMD5)).ToList();
        }
        catch (Exception ex)
        {
            var exResponse = new { Message = ex.Message };
            await SendAsync(exResponse, StatusCodes.Status500InternalServerError, ct);
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

        var errorResponse = new { Message = "Invalid credential data" };
        await SendAsync(errorResponse, StatusCodes.Status400BadRequest, ct);
    }
}
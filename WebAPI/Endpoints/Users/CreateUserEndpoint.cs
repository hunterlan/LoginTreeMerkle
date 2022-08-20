using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RequestsDTO.Users;
using WebAPI.ResponsesDTO.Users;

namespace WebAPI.Endpoints.Users;

public class CreateUserEndpoint : Endpoint<CreateUserRequest>
{
    private readonly ApplicationContext _context;
    private readonly ICreator _creator;

    public CreateUserEndpoint(ApplicationContext context, ICreator creator)
    {
        _context = context;
        _creator = creator;

        _context.Database.EnsureCreated();
    }

    public override void Configure()
    {
        Post("api/user/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        Logger.LogInformation($"Attempt to create user {req.Login}");
        // var response = new CreateUserResponse() { IsCreated = true };
        dynamic response;
        bool isError = false;

        try
        {
            response = new CreateUserResponse();
            var mappedUserData = MapUserData(req);
            mappedUserData.Key = _creator.CreateHashOnData(mappedUserData);
            var mappedUser = MapUser(req.Login, req.Password, mappedUserData);

            await _context.Users.AddAsync(mappedUser, ct);
            await _context.UserData.AddAsync(mappedUserData, ct);

            await _context.SaveChangesAsync(ct);

            response.Login = mappedUser.Login;
            response.Data = mappedUserData;
            response.Token = JWTBearer.CreateToken(
                signingKey: Config.GetSection("JWTSigninKey").Value,
                expireAt: DateTime.UtcNow.AddHours(1),
                claims: new[] { ("Login", req.Login), ("Email", req.Email) });
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            response = new ErrorResponse();
            response.Message = ex.Message;
            response.StatusCode = StatusCodes.Status500InternalServerError;
            isError = true;
        }

        await SendAsync(response, statusCode: isError ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK, cancellation: ct);
    }

    private User MapUser(string login, string password, UserData data)
    {
        var hashedMerkleTree =
            _creator.CreateMerkleHashTree(data, login, password, Config.GetSection("Salt").Value);
        var newUser = new User() { Login = login, MarkleHashTree = hashedMerkleTree };

        return newUser;
    }

    private UserData MapUserData(CreateUserRequest request)
    {
        return new UserData()
        {
            Age = request.Age,
            City = request.City,
            Country = request.Country,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Region = request.Region,
            PostalCode = request.PostalCode,
            PhoneNumber = request.PhoneNumber
        };
    }
}
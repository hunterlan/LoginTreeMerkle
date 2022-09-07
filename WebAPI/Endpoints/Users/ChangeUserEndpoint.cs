using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RequestsDTO.Users;
using WebAPI.ResponsesDTO.Users;

namespace WebAPI.Endpoints.Users;

public class ChangeUserEndpoint : Endpoint<ChangeUserRequest>
{
    private readonly ApplicationContext _context;
    private readonly ICreator _creator;

    public ChangeUserEndpoint(ApplicationContext context, ICreator creator)
    {
        _context = context;
        _creator = creator;
    }

    public override void Configure()
    {
        Post("api/user/change");
    }

    public override async Task HandleAsync(ChangeUserRequest req, CancellationToken ct)
    {
        Logger.LogInformation($"Attempt to change user {req.OldLogin}");
        dynamic response;
        bool isError = false;

        var foundUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login.Equals(req.OldLogin), cancellationToken: ct);
        var foundUserData =
            await _context.UserData.AsNoTracking().FirstOrDefaultAsync(ud => ud.Key.Equals(req.Key), cancellationToken: ct);
        if (foundUser != null && foundUserData != null)
        {
            try
            {
                var loginProvide = req.NewLogin ?? req.OldLogin;
                var mappedUserData = MapUserData(req, foundUserData);
                mappedUserData.Key = _creator.CreateHashOnData(mappedUserData);
                var changedUser = MapUser(loginProvide, req.Password, mappedUserData);
                changedUser.Id = foundUser.Id;
                mappedUserData.Id = foundUserData.Id;

                _context.Users.Update(changedUser);
                _context.UserData.Update(mappedUserData);

                await _context.SaveChangesAsync(ct);

                response = new ChangeUserResponse();
                response.NewKey = mappedUserData.Key;
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
        else
        {
            Logger.LogError("Failed attempt.");
            await SendAsync(
                new ErrorResponse { Message = "User not found", StatusCode = StatusCodes.Status404NotFound },
                StatusCodes.Status404NotFound, ct);
        }
    }

    private User MapUser(string login, string password, UserData data)
    {
        var hashedMerkleTree =
            _creator.CreateMerkleHashTree(data, login, password, Config.GetSection("Salt").Value);
        var newUser = new User() { Login = login, MarkleHashTree = hashedMerkleTree };

        return newUser;
    }

    private UserData MapUserData(ChangeUserRequest request, UserData foundUserData)
    {
        return new UserData()
        {
            Age = request.Age ?? foundUserData.Age,
            City = request.City ?? foundUserData.City,
            Country = request.Country ?? foundUserData.Country,
            Email = request.Email ?? foundUserData.Email,
            FirstName = request.FirstName ?? foundUserData.FirstName,
            LastName = request.LastName ?? foundUserData.LastName,
            Region = request.Region ?? foundUserData.Region,
            PostalCode = request.PostalCode ?? foundUserData.PostalCode,
            PhoneNumber = request.PhoneNumber ?? foundUserData.PhoneNumber
        };
    }
}
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.RequestsDTO.Users;

namespace WebAPI.Endpoints.Users;

public class DeleteUserEndpoint : Endpoint<DeleteUserRequest>
{
    private readonly ApplicationContext _context;

    public DeleteUserEndpoint(ApplicationContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("/api/user/delete");
    }

    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        Logger.LogInformation($"Attempt to delete user {req.Login}");
        var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Login.Equals(req.Login), cancellationToken: ct);
        var foundUserData = await _context.UserData.FirstOrDefaultAsync(ud => ud.Email.Equals(req.Email), cancellationToken: ct);

        if (foundUser != null && foundUserData != null)
        {
            _context.Users.Remove(foundUser);
            _context.UserData.Remove(foundUserData);

            try
            { 
                Logger.LogInformation("Successful attempt.");
               await _context.SaveChangesAsync(ct);
               await SendAsync(new EmptyResponse(), StatusCodes.Status200OK, ct);
            }
            catch (Exception e)
            {
                Logger.LogError($"Exception during deleting user {req.Login}: {e.Message}");
                await SendAsync(
                    new ErrorResponse { Message = "Error during deleting user", StatusCode = StatusCodes.Status500InternalServerError },
                    StatusCodes.Status404NotFound, ct);
            }
        }
        else
        {
            Logger.LogError("Failed attempt.");
            await SendAsync(new ErrorResponse { Message = "User not found", StatusCode = StatusCodes.Status404NotFound },
                StatusCodes.Status404NotFound, ct);
        }
    }
}
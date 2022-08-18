using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Configuration;
using WebAPI.Helpers;
using WebAPI.RequestsDTO.Users;
using WebAPI.ResponsesDTO.Users;

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
        var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Login.Equals(req.Login), cancellationToken: ct);
        var foundUserData = await _context.UserData.FirstOrDefaultAsync(ud => ud.Email.Equals(req.Email), cancellationToken: ct);

        if (foundUser != null && foundUserData != null)
        {
            _context.Users.Remove(foundUser);
            _context.UserData.Remove(foundUserData);

            try
            {
               await _context.SaveChangesAsync(ct);
               await SendAsync(new EmptyResponse(), StatusCodes.Status200OK, ct);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                await SendAsync(
                    new ErrorResponse { Message = "Error during deleting user", StatusCode = StatusCodes.Status500InternalServerError },
                    StatusCodes.Status404NotFound, ct);
            }
        }
        else
        {
            await SendAsync(new ErrorResponse { Message = "User not found", StatusCode = StatusCodes.Status404NotFound },
                StatusCodes.Status404NotFound, ct);
        }
    }
}
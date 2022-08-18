using WebAPI.Endpoints.Users;
using WebAPI.RequestsDTO.Users;
using WebAPI.ResponsesDTO.Users;

namespace WebAPI.Helpers.Summuries;

public class LoginUserSummary : Summary<LoginUserEndpoint>
{
    public LoginUserSummary()
    {
        Summary = "Login user request";
        Description = "All fields are required.";
        ExampleRequest = new LoginUserRequest
        {
            Login = "loginUser",
            DataMD5 = "MD5 hash",
            Password = "userPassowrd"
        };
        Response<LoginUserResponse>(200, "OK response with body.");
        Response<ErrorResponse>(400, "Validation failure.");
        Response<ErrorResponse>(500, "See error messages.");
    }
}
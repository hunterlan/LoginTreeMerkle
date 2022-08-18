using WebAPI.Models;

namespace WebAPI.ResponsesDTO.Users;

public class CreateUserResponse
{
    public string Login { get; set; }

    public UserData Data { get; set; }

    public string Token { get; set; }
}
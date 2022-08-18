namespace WebAPI.RequestsDTO.Users;

public class LoginUserRequest
{
    public string Login { get; set; }

    public string Password { get; set; }

    public string DataMD5 { get; set; }
}
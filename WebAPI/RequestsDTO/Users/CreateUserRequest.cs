using Microsoft.AspNetCore.Mvc;

namespace WebAPI.RequestsDTO.Users;

public class CreateUserRequest
{
    public string Login { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string? PhoneNumber { get; set; }

    public uint Age { get; set; }
}
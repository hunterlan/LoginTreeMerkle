using WebAPI.Models;

namespace WebAPI.RequestsDTO.Users;

public class ChangeUserRequest
{
    public string OldLogin { get; set; } = string.Empty;

    public string? NewLogin { get; set; }

    //Can be provided old or new 
    public string Password { get; set; } = string.Empty;

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? Birthday { get; set; }

    public string Key { get; set; } = string.Empty;
}
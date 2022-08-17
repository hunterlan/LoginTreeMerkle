namespace WebAPI.Models;

public class UserData
{
    public int Id { get; set; }

    public string HashMD5 { get; set; }

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
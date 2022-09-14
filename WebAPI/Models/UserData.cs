namespace WebAPI.Models;

public class UserData
{
    public int Id { get; set; }

    public string Key { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime Birthday { get; set; }
}
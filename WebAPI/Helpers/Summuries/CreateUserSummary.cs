using WebAPI.Endpoints.Users;
using WebAPI.RequestsDTO.Users;
using WebAPI.ResponsesDTO.Users;

namespace WebAPI.Helpers.Summuries;

public class CreateUserSummary : Summary<CreateUserEndpoint>
{
    public CreateUserSummary()
    {
        Summary = "Request for creating user";
        Description = "Fields login, password, first and last names, email, country, city and age is required. " +
                      "For login minimal length is 6, for password and email accepted default rules, for phone waiting " +
                      "only numbers or + with numbers, age minimum 18.";
        ExampleRequest = new CreateUserRequest
        {
            Login = "userLogin",
            Password = "1@3Tu456789O",
            FullName = "John",
            Email = "john.smith@mail.com",
            Country = "USA",
            City = "Washington",
            Region = "Optional",
            PostalCode = "Optional",
            PhoneNumber = "+624127",
            Birthday = new DateTime(2001, 1, 1)
        };
        Response<CreateUserResponse>(200, "OK response with body");
        Response<ErrorResponse>(500, "See error message");
        Response<ErrorResponse>(400, "Invalid data was sent. Details see in message.");
    }
}
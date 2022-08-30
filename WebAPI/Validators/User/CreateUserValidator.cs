using System.Text.RegularExpressions;
using WebAPI.RequestsDTO.Users;

namespace WebAPI.Validators.User;

public class CreateUserValidator : Validator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Age)
            .GreaterThan(17)
            .WithMessage("You can't use this service while you're under 18");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Specify email")
            .EmailAddress()
            .WithMessage("Invalid email!");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Specify first name");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Specify last name");

        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Specify country");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("Specify city");

        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Specify login")
            .MinimumLength(6)
            .WithMessage("Minimal length of login is 6");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Specify password")
            .MinimumLength(8)
            .WithMessage("Minimum length is 8 symbols!")
            .Matches(new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$"))
            .WithMessage("Password should contain at least one lower, one upper, one number and special symbol");
            

        RuleFor(x => x.PhoneNumber)
            .Empty()
            .When(x => string.IsNullOrWhiteSpace(x.PhoneNumber))
            .NotEmpty()
            .Matches(new Regex(@"^\+?\d+$"))
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("Invalid phone number!");
    }
}
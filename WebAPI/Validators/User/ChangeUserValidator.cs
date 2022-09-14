using System.Text.RegularExpressions;
using WebAPI.RequestsDTO.Users;

namespace WebAPI.Validators.User;

public class ChangeUserValidator : Validator<ChangeUserRequest>
{
    public ChangeUserValidator()
    {
        // TO-DO: change validation for age
        RuleFor(x => x.Birthday)
            .Empty()
            .When(x => x.Birthday is null)
            .NotEmpty()
            .Must(birthday =>
            {
                DateTime today = new(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day);

                if (birthday.Value.CompareTo(today) <= 0)
                {
                    return true;
                }


                return false;
            })
            .When(x => x.Birthday is not null)
            .WithMessage("You can't use this service while you're under 18");

        RuleFor(x => x.Email)
            .Empty()
            .When(x => string.IsNullOrWhiteSpace(x.Email))
            .NotEmpty()
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Invalid email!");

        RuleFor(x => x.OldLogin)
            .NotEmpty()
            .WithMessage("Specify login");

        RuleFor(x => x.NewLogin)
            .Empty()
            .When(x => string.IsNullOrWhiteSpace(x.NewLogin))
            .NotEmpty()
            .MinimumLength(6)
            .When(x => !string.IsNullOrWhiteSpace(x.NewLogin))
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

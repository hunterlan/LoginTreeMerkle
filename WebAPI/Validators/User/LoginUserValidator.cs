using WebAPI.RequestsDTO.Users;

namespace WebAPI.Validators.User;

public class LoginUserValidator : Validator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        RuleFor(u => u.Login)
            .NotEmpty()
            .WithMessage("Login shouldn't be empty");

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Password shouldn't be empty");

        RuleFor(u => u.Key)
            .NotEmpty()
            .WithMessage("Key MD5 shouldn't be empty");
    }
}
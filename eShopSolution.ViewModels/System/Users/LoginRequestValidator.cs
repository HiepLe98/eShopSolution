using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.System.Users
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MaximumLength(12).WithMessage("Password is not larger 12 charaters")
                .MinimumLength(6).WithMessage("Password is at least 6 charaters");
        }
    }
}

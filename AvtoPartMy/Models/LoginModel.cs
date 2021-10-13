using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class MyValidatiorLogin : AbstractValidator<LoginModel>
    {
        public MyValidatiorLogin()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Поле не може бути пустим!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Поле не може бути пустим!");
        }
    }
}

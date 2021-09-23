using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Models
{
    public class RegisterViewModels

    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class UserValidator : AbstractValidator<RegisterViewModels>
    {

        public UserValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Поле не може бути пустим");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("Поле не може бути пустим");
            RuleFor(x => x.SecondName).NotNull().NotEmpty().WithMessage("Поле не може бути пустим");
            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage("Поле не може бути пустим");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Поле не може бути пустим");
            RuleFor(x => x.Password).Length(5, 100).WithMessage("Пароль не може бути менше 5 символів");
            RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().WithMessage("Поле не може бути пустим");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Пароль і підтверджений пароль не співпадають");



        }




    }
}

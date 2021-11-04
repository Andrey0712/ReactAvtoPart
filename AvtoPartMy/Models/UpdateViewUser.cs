using Data;
using Data.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Models
{
    public class UpdateViewUser
    {
        public string Email { get; set; }

        public string Name { get; set; }
        public IFormFile Photo { get; set; }
    }

    public class UserUpdateValidator : AbstractValidator<UpdateViewUser>
    {
        private readonly AppEFContext _appEFContext;
        private readonly UserManager<AppUser> _userManager;

        public UserUpdateValidator(AppEFContext appEFContext, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _appEFContext = appEFContext;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(" Поле не може бути пустим!")
                .EmailAddress().WithMessage(" Пароль має містити '@'");
                //.DependentRules(() =>
                //{
                //    RuleFor(x => x.Email).Must(BeUniqueEmail)

                //     .WithMessage("Дана пошта уже зареєстрована!");
                //});
                //.Must(BeValidEmail).WithName("Email").WithMessage("Такий користувач вже існує!");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Поле не може бути пустим ");

            //RuleFor(x => x.Password)
            //    .NotEmpty().WithMessage("Поле не може бути пустим  ")
            //    .Length(5, 100).WithMessage(" Пароль не може бути менше 5 символів")
            //    .Matches(@"\d").WithName("Password").WithMessage(" Пароль повинен містити хоча б одну цифру ");

            //RuleFor(x => x.ConfirmPassword)
            //    .NotEmpty().WithMessage(" Поле не може бути пустим   ")
            //    .Equal(x => x.Password).WithMessage(" Пароль і підтверджений пароль не співпадають");



        }

        


    }
}

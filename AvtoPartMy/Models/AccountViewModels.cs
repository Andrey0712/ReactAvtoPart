using Data;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Models
{
    public class RegistrateViewModels
    {
        public string Email { get; set; }

    public string Name { get; set; }
        public IFormFile Photo { get; set; }
        public string Password { get; set; }

    public string ConfirmPassword { get; set; }
}

public class UserValidator : AbstractValidator<RegistrateViewModels>
{
    private readonly AppEFContext _appEFContext;


    public UserValidator(AppEFContext appEFContext)
    {
        _appEFContext = appEFContext;

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(" Поле не може бути пустим!")
            .EmailAddress().WithMessage(" Пароль має містити '@'")
            .Must(BeValidEmail).WithName("Email").WithMessage("Такий користувач вже існує!");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Поле не може бути пустим ");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Поле не може бути пустим  ")
            .Length(3, 100).WithMessage(" Пароль не може бути менше 3 символів")
            .Matches(@"\d").WithName("Password").WithMessage(" Пароль повинен містити хоча б одну цифру ");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage(" Поле не може бути пустим   ")
            .Equal(x => x.Password).WithMessage(" Пароль і підтверджений пароль не співпадають");



    }

    private bool BeValidEmail(string email)
    {
        //var user = _userManager.FindByEmailAsync(email).Result;
        var user = _appEFContext.Users.FirstOrDefault(x => x.Email == email);
        if (user == null)
        {
            return true;
        }
        return false;
    }
}
}

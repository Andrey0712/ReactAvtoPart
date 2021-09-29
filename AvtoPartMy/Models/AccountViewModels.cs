using Data;
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

        private readonly AppEFContext _appEFContext;
        public UserValidator(AppEFContext appEFContext)
        {
            _appEFContext = appEFContext;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Поле Email не може бути порожнім")
                .MinimumLength(6)
                .EmailAddress()
                .WithMessage("Помилка заповнення поля Email")
                .Must(IsValidEmail)
                .WithName("Email")
                .WithMessage("Такий користувач вже існує");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Поле Password не може бути порожнім")
                .MinimumLength(5)
                .WithMessage("Пароль не може бути коротший, ніж 5 символів")
                .Matches(@"\d")
                .WithName("Password")
                .WithMessage("Пароль повинен містити хоча б одну цифру");
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Поле Phone не може бути порожнім")
                .MinimumLength(10)
                .MaximumLength(11)
                .WithMessage("Має бути не менше 10 і не більше 11 цифр");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Поле ConfirmPassword не може бути порожнім")
                .Equal(x => x.Password)
                .WithMessage("Введене підтвердження не співпадає з паролем");
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Поле ім'я не може бути порожнім");
            RuleFor(x => x.SecondName)
                .NotEmpty()
                .WithMessage("Поле прізвище не може бути порожнім");

        }
        private bool IsValidEmail(string email)
        {
            var user = _appEFContext.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return true;
            }
            return false;
        }
    }
}

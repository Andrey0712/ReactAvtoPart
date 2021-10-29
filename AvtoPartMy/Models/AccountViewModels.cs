using AutoMapper;
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
    public class RegistrateViewModels
    {
        public string Email { get; set; }

    public string Name { get; set; }
        public IFormFile Photo { get; set; }
        public string Password { get; set; }

    public string ConfirmPassword { get; set; }
     }

    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<RegistrateViewModels, AppUser>()
                .ForMember(x => x.FotoUser, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Name));
            //.ForMember(x => x.Image, opt => opt.MapFrom(x => "images/"
            //    + (string.IsNullOrEmpty(x.Photo) ? "noimage.jpg" : x.Photo)));
        }
    }

    public class UserValidator : AbstractValidator<RegistrateViewModels>
{
    private readonly AppEFContext _appEFContext;
        private readonly UserManager<AppUser> _userManager;

        public UserValidator(AppEFContext appEFContext, UserManager<AppUser> userManager)
    {
            _userManager = userManager;
            _appEFContext = appEFContext;

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(" Поле не може бути пустим!")
            .EmailAddress().WithMessage(" Пароль має містити '@'")
            //.DependentRules(() =>
            //{
            //    RuleFor(x => x.Email).Must(BeUniqueEmail)

            //     .WithMessage("Дана пошта уже зареєстрована!");
            //});
            .Must(BeValidEmail).WithName("Email").WithMessage("Такий користувач вже існує!");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Поле не може бути пустим ");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Поле не може бути пустим  ")
            .Length(5, 100).WithMessage(" Пароль не може бути менше 5 символів")
            .Matches(@"\d").WithName("Password").WithMessage(" Пароль повинен містити хоча б одну цифру ");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage(" Поле не може бути пустим   ")
            .Equal(x => x.Password).WithMessage(" Пароль і підтверджений пароль не співпадають");



    }

        private bool BeUniqueEmail(string email)
        {
            return _userManager.FindByEmailAsync(email).Result == null;
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

    public class AccountError
    {
        public AccountError()
        {
            Errors = new AccountErrorItem();
        }
        public AccountError(string message)
        {
            Errors = new AccountErrorItem();
            Errors.Invalid.Add(message);
        }
        public AccountErrorItem Errors { get; set; }
    }

    public class AccountErrorItem
    {
        public AccountErrorItem()
        {
            Invalid = new List<string>();
        }
        public List<string> Invalid { get; set; }
    }
}

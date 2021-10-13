using AvtoPartMy.Constans;
using AvtoPartMy.Models;
using AvtoPartMy.Services;
using Data;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
       

        public AccountController(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                RoleManager<AppRole> roleManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = jwtTokenService;
        }

        


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrateViewModels model)
        {
            ///Зберігаємо фото
            try
            {

                var user = new AppUser
                {
                    Email = model.Email,
                    UserName = model.Name

                };

                var role = new AppRole
                {
                    Name = Roles.User
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                    return BadRequest(new { message = result.Errors });

                await _userManager.AddToRoleAsync(user, role.Name);

                await _signInManager.SignInAsync(user, isPersistent: false);

                return Ok(new
                {
                    token = _tokenService.Authentificate(user)
                });
            }
            catch
            {
                return BadRequest(new { message = "Щось пішло не так - помилка з БД" });

            }
        }

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            var result = await _signInManager
                .PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Incorrect data!" });
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            return Ok(new
            {
                token = _tokenService.Authentificate(user)
            });
        }

        //[HttpPost]
        //[Route("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterViewModels model)
        //{
        //    return await Task.Run(() =>
        //    {
        //        IActionResult res;

        //            var user = new AppUser
        //            {
        //                Email = model.Email,
        //                UserName = model.FirstName

        //            };

        //            var createResult = _userManager.CreateAsync(user, model.Password).Result;
        //            if (createResult.Succeeded)
        //            {
        //                res = Ok(new
        //                {
        //                    message = "Успіно зареєстровано!",
        //                    token = _jwtTokenService.CreateToken(user)
        //                });
        //                _signInManager.PasswordSignInAsync(user, model.Password, false, false).Wait();
        //            }
        //            else
        //            {
        //                res = BadRequest(new
        //                {
        //                    error = createResult.Errors
        //                });
        //            }


        //        //else
        //        //{
        //        //    var result = new UserValidator().ValidateAsync(model).Result;
        //        //    res = BadRequest(new
        //        //    {
        //        //        error = result.Errors
        //        //    });
        //        //}
        //        return res;
        //    });
        //}

        //[HttpPost]
        //[Route("logout")]
        //public async Task<IActionResult> Logout([FromBody] string email)
        //{
        //    return await Task.Run(() => {
        //        var userClaim = _signInManager.IsSignedIn(_signInManager.CreateUserPrincipalAsync(
        //            _userManager.FindByEmailAsync(email).Result).Result);
        //        if (userClaim)
        //        {
        //            _signInManager.SignOutAsync().Wait();
        //        }
        //        return Ok();
        //    });
        //}

        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login([FromBody] LoginModel model)
        //{
        //    return await Task.Run(async () => {
        //        IActionResult res;
        //        if (ModelState.IsValid)
        //        {
        //            var user = await _userManager.FindByEmailAsync(model.Email);
        //            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        //            {
        //                res = Ok(new
        //                {
        //                    message = "Вхід схвалено!",
        //                    token = _jwtTokenService.CreateToken(user),
        //                    UserName = user.NormalizedUserName,
        //                    email = user.Email,

        //                });
        //                _signInManager.PasswordSignInAsync(user, model.Password, false, false).Wait();
        //            }
        //            else
        //            {
        //                res = BadRequest(new
        //                {
        //                    message = "Не правильний логін або пароль!"
        //                });
        //            }
        //        }
        //        else
        //        {
        //            var result = new MyValidatiorLogin().ValidateAsync(model).Result;
        //            res = BadRequest(new
        //            {
        //                error = result.Errors
        //            });
        //        }
        //        return res;
        //    });
        //}


    }
}

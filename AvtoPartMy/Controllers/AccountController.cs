using AutoMapper;
using AvtoPartMy.Abastract;
using AvtoPartMy.Constans;
using AvtoPartMy.Exceptions;
using AvtoPartMy.Models;
using AvtoPartMy.Services;
using Data;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
          private readonly IUserService _userService;

        public AccountController(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                  IJwtTokenService jwtTokenService,
                                         IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = jwtTokenService;
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] RegistrateViewModels model)
        {
            try
            {
                string token = await _userService.CreateUser(model);
                return Ok(
                    new { token }
                    );
            }
            catch (AccountException aex)
            {
                return BadRequest(aex.AccountError);
            }
            catch
            {
                return BadRequest(new AccountError("Щось пішло не так!"));
            }
        }


        //[HttpPost]
        //[Route("register")]
        //public async Task<IActionResult> Register([FromForm] RegistrateViewModels model)
        //{


        //        string foto = string.Empty;
        //    string fileName = String.Empty;

        //    if (model.Photo != null)
        //        {                    
        //            var ext = Path.GetExtension(model.Photo.FileName);
        //            foto = Path.GetRandomFileName() + ext;
        //            var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
        //            fileName = Path.Combine(dir, foto);

        //            using (var stream = System.IO.File.Create(fileName))
        //            {
        //                await model.Photo.CopyToAsync(stream);
        //            }
        //        }

        //        try
        //    {

        //        var user = new AppUser
        //        {
        //            Email = model.Email,
        //            UserName = model.Name,
        //            FotoUser= foto
        //        };

        //        // var role = new AppRole
        //        // {
        //        //     Name = Roles.User
        //        // };
        //        var result = await _userManager.CreateAsync(user, model.Password);

        //        if (!result.Succeeded)
        //        {
        //            if (!string.IsNullOrEmpty(fileName))
        //                System.IO.File.Delete(fileName);
        //            AccountError accountError = new AccountError();
        //            foreach (var item in result.Errors)
        //            {
        //                accountError.Errors.Invalid.Add(item.Description);
        //            }
        //            return BadRequest(accountError);
        //        }
        //        // //return BadRequest(new { message = result.Errors });

        //        await _userManager.AddToRoleAsync(user, Roles.User);
        //        if (!result.Succeeded)
        //        {
        //            if (!string.IsNullOrEmpty(fileName))
        //                System.IO.File.Delete(fileName);
        //            result = await _userManager.DeleteAsync(user);
        //            AccountError accountError = new AccountError();
        //            foreach (var item in result.Errors)
        //            {
        //                accountError.Errors.Invalid.Add(item.Description);
        //            }
        //            return BadRequest(accountError);
        //        }

        //        // await _signInManager.SignInAsync(user, isPersistent: false);

        //        return Ok(new
        //        {
        //            token = _tokenService.Authentificate(user)
        //        });
        //    }
        //    catch
        //    {
        //        return BadRequest(new { message = "Щось пішло не так - помилка з БД" });

        //    }
        //}

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            //var result = await _signInManager
            //    .PasswordSignInAsync(model.Email, model.Password, false, false);
            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Incorrect data!" });
            }
            

            return Ok(new
            {
                token = _tokenService.Authentificate(user)
            });
        }

        
    }
}

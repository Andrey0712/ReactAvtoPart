using AvtoPartMy.Constans;
using AvtoPartMy.Models;
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
        //private readonly AppEFContext _context;
        //public AccountController(UserManager<AppUser> userManager, AppEFContext context)
        //{
        //    _userManager = userManager;
        //    _context = context;
        //}
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("register")]
        //public IActionResult Register([FromBody] RegisterViewModels model)
        //{


        //    //return BadRequest(new {
        //    //    message="Такий користувач уже є!"
        //    //});
        //    return Ok();
        //}
        //public async Task<IActionResult> Register([FromBody] RegisterViewModels model)//сохранить юзера
        //{
        //    var user = new AppUser
        //    {
        //        Email = model.Email,
        //        UserName = model.FirstName + " " + model.SecondName,
        //        PhoneNumber = model.Phone,
        //        PasswordHash = model.Password
        //    };
        //    var result = await _userManager.CreateAsync(user, model.Password);
        //    _context.Users.Add(user);
        //    _context.SaveChanges();
        //    if (!result.Succeeded)
        //        return BadRequest(new { message = result.Errors });

        //    return Ok();
        //}

        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModels model)
        {
            var user = new AppUser
            {
                Email = model.Email,
                UserName = model.FirstName

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


            return Ok();
        }


    }
}

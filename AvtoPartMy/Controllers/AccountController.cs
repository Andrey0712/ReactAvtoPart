using AutoMapper;
using AvtoPartMy.Constans;
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
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly AppEFContext _context;

        public AccountController(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                RoleManager<AppRole> roleManager, 
                                IJwtTokenService jwtTokenService,
                                IMapper mapper,
                                AppEFContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = jwtTokenService;
            _context = context;
            _mapper = mapper;

        }




        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] RegistrateViewModels model)
        {
            
                           
                string foto = string.Empty;

               
                if (model.Photo != null)
                {                    
                    var ext = Path.GetExtension(model.Photo.FileName);
                    foto = Path.GetRandomFileName() + ext;
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
                    var fotoPath = Path.Combine(dir, foto);

                    using (var stream = System.IO.File.Create(fotoPath))
                    {
                        await model.Photo.CopyToAsync(stream);
                    }
                }
                
                try
            {

                var user = new AppUser
                {
                    Email = model.Email,
                    UserName = model.Name,
                    FotoUser= foto
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

        //[HttpGet]
        ////[HttpPost]
        //[Route("getusers")]
        //[Authorize(Roles = Roles.User)]

        //public async Task<IActionResult> GetUsersList()
        //{
        //    //return await Task.Run(() => {
        //    //    return Ok(_userManager.Users
        //    //        .Select(x => x).ToList());
        //    //});

        //    var userlist = await _context.Users
        //        .Select(res => _mapper.Map<UserViewModel>(res))
        //        .ToListAsync();

        //    return Ok(userlist);
        //}




    }
}

using AutoMapper;
using AvtoPartMy.Exceptions;
using AvtoPartMy.Models;
using AvtoPartMy.Services;
using Data;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppEFContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private IHostEnvironment _host;
        public UsersController(AppEFContext context,
            IMapper mapper, UserManager<AppUser> userManager,
            IJwtTokenService jwtTokenService, IHostEnvironment host)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _host = host;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _context.Users
                .Select(x => _mapper.Map<UserViewModel>(x))
                .ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        [Route("delete")]
        
        public IActionResult Delete([FromBody] string id)
        {

            var res = _context.Users.FirstOrDefault(x => x.Email == id);
            if (res == null)
            {
                return BadRequest(new { message = "Check id!" });
            }

            _context.Users.Remove(res);
            _context.SaveChanges();
            return Ok(new { message = "User deleted" });
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditUser([FromForm] UpdateViewUser model)
        {
            var res = _context.Users.FirstOrDefault(x => x.Email == model.Email);

            if (model == null)
            {
                return BadRequest(new { message = "Не зашла инфа" });
            }

            //res.Email = model.Email;
            res.UserName = model.Name;
            

            string fileName = string.Empty;

            if (model.Photo != null)
            {
                var ext = Path.GetExtension(model.Photo.FileName);
                fileName = Path.GetRandomFileName() + ext;
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");

                var filePath = Path.Combine(dir, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.Photo.CopyToAsync(stream);
                }
                var oldImage = res.FotoUser;
                string fol = "\\images\\";
                string contentRootPath = _host.ContentRootPath + fol + oldImage;

                if (System.IO.File.Exists(contentRootPath))
                {
                    System.IO.File.Delete(contentRootPath);
                }
                res.FotoUser = fileName;
            }
            _context.SaveChanges();

            return Ok(new { message = "ok" });
        }







    }
}

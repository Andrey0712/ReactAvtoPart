using AvtoPartMy.Models;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AvtoPartMy.Services
{
    public interface IJwtTokenService
    {
        public string Authentificate(AppUser user);
    }

    public class JwtTokenServise : IJwtTokenService
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<AppUser> _userManager;
        public JwtTokenServise(IOptions<AppSettings> appsettings, UserManager<AppUser> userManager)
        {
            _appSettings = appsettings.Value;
            _userManager = userManager;
        }

        public string Authentificate(AppUser appUser)
        {

            var roles = _userManager.GetRolesAsync(appUser).Result;
            var roleClaims = new List<Claim>()
            {
                 new Claim("id",appUser.Id.ToString()),
                 new Claim("name",appUser.UserName)
            };

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var signKey = new SymmetricSecurityKey(key);
            var singCredentials = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
               signingCredentials: singCredentials,
               expires: DateTime.Now.AddDays(100),
               claims: roleClaims
               );
            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }
    }
}

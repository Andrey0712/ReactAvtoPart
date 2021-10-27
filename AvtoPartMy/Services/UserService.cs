using AutoMapper;
using AvtoPartMy.Abastract;
using AvtoPartMy.Constans;
using AvtoPartMy.Exceptions;
using AvtoPartMy.Models;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        public UserService(
            UserManager<AppUser> userManager,
            IJwtTokenService jwtTokenService,
            IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }
        

        public async  Task<string> CreateUser(RegistrateViewModels model)
        {
            string fileName = String.Empty;
            var user = _mapper.Map<AppUser>(model);
            try
            {
                //throw new Exception("asdfa");
                if (model.Photo != null)
                {
                    string randomFilename = Path.GetRandomFileName() +
                        Path.GetExtension(model.Photo.FileName);

                    string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "images");
                    fileName = Path.Combine(dirPath, randomFilename);
                    using (var file = File.Create(fileName))
                    {
                        model.Photo.CopyTo(file);
                    }
                    user.FotoUser = randomFilename;
                }

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(fileName))
                        File.Delete(fileName);
                    AccountError accountError = new AccountError();
                    foreach (var item in result.Errors)
                    {
                        accountError.Errors.Invalid.Add(item.Description);
                    }

                    throw new AccountException(accountError);
                }

                result = await _userManager.AddToRoleAsync(user, Roles.User);
                if (!result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(fileName))
                        File.Delete(fileName);
                    result = await _userManager.DeleteAsync(user);
                    AccountError accountError = new AccountError();
                    foreach (var item in result.Errors)
                    {
                        accountError.Errors.Invalid.Add(item.Description);
                    }
                    throw new AccountException(accountError);
                }
                return _jwtTokenService.Authentificate(user);
            }
            catch (AccountException aex)
            {
                throw new AccountException(aex.AccountError);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(fileName))
                    File.Delete(fileName);
                if (user.Id != 0)
                {
                    var result = await _userManager.DeleteAsync(user);
                }
                AccountError accountError = new AccountError();
                accountError.Errors.Invalid.Add(ex.Message);
                throw new AccountException(accountError);
            }
        }
    }
}

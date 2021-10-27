using AvtoPartMy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Abastract
{
    public interface IUserService
    {
        public Task<string> CreateUser(RegistrateViewModels model);
    }
}

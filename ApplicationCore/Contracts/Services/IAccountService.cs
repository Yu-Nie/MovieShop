using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IAccountService
    {
        Task<UserLoginSuccessModel> ValidateUser(UserLoginModel model);
        
        // return newly registered user id
        Task<int> RegisterUser(UserRegisterModel model);
    }
}

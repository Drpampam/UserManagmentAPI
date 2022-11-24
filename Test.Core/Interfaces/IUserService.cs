using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Dto;
using Test.Data;

namespace Test.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> AddUser(UserDto userDto);
        Task<List<UserDto>> GetUser();
        Task<UserDto> FindByEmail(string email);
        Task<List<EmailDto>> GetUserEmail();
        Task<AppUser> Login(LoginDto loginDto);
        Task<bool> Logout();
    }
}

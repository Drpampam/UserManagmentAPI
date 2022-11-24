using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Dto;
using Test.Core.Interfaces;
using Test.Data;

namespace Test.Core.Services
{
    public class UserService : IUserService
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManger;

        public UserService(UserManager<AppUser> usermanager, SignInManager<AppUser> signinManager)
        {
            _userManager = usermanager;
            _signInManger = signinManager;
        }

        public static List<UserDto> Users { get; set; } = new List<UserDto>();

        public async Task<UserDto> AddUser(UserDto userDto)
        {
            var user = new AppUser()
            {
                UserName = userDto.Email,
                Email = userDto.Email

            };

            var result = await _userManager.CreateAsync(user, "P@w0rd$");

            if (result.Succeeded)
            {
                return userDto;
            }
            return null;
        }

        public async Task<List<UserDto>> GetUser()
        {
            return Users;
        }

        public async Task<UserDto> FindByEmail(string email)
        {
            return Users.Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()).FirstOrDefault();
        }

        public async Task<List<EmailDto>> GetUserEmail()
        {



            return Users.Select(x => new EmailDto()
            {
                Email = x.Email
            }).ToList();
        }

        public async Task<AppUser> Login(LoginDto loginDto)
        {
            //"check@gmail.com", "P@w0rd$"
            var result = await _signInManger.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
            if (result.Succeeded)
            {
                return await _userManager.FindByEmailAsync(loginDto.Email);
             
            }
            return null;
        }

        public async Task<bool> Logout()
        {
           await _signInManger.SignOutAsync();
            return true;
        }

     
    }
}

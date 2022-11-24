using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Test.Core.Dto;
using Test.Core.Interfaces;
using Test.Data;
using WebApplication14.Model;

namespace WebApplication14.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //critical 
        // Error 
        // Warning 
        // information 
        // Debug 
        // Trace

        private IUserService _userservice;
        private IJwtService _jwtService;
        private ILogger<UserController> _logger;
        private IHttpClientFactory _httpClientFactory;
        private UserManager<AppUser> _userManager;

        public UserController(IUserService userService, IJwtService jwtService, ILogger<UserController> logger, IHttpClientFactory httpClientFactory)
        {

            _userservice = userService;
            _jwtService = jwtService;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
          

        }
      
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]User user)
        {
           
            var newUser = new UserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

             var result = await _userservice.AddUser(newUser);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Error! User Cannot be Created ");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
           var result = await _userservice.Login(loginDto);

            if (result == null)
            {
                return BadRequest("Invalid User");
            }
             var token =  _jwtService.Authenticate(result);
            return StatusCode(200, new { Token = token.Token });
        }


        [HttpPost("loginOut")]
        public async Task<IActionResult> LoginOut()
        {
            _logger.LogError("can not retrieve");
            var result = await _userservice.Logout();  
            return Ok("Logut Sucessfull");
        }


      
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await  _userservice.GetUser();
            return Ok(users);
        }


        [HttpGet("email")]
        public async Task<IActionResult> GetUserEmail()
        {
            var users = await _userservice.GetUserEmail();

            if (!users.Any())
            {
                return NotFound();
            }
            return Ok(users);
        }



        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userservice.FindByEmail(email);
            if (user == null)
            {
                return NotFound("User Not Found");   
            }
            return Ok(user);
            
        }



        [HttpGet("api")]
        public async Task<IActionResult> GetAPI()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                using (var response = await httpClient.GetAsync("https://open.er-api.com/v6/latest/USD", HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    var Rates = await JsonSerializer.DeserializeAsync<RateDto>(stream);
                    return Ok(Rates);
                }
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        
           

        }
    }
}

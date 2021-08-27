using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var user = await _userService.GetAllUsers();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestModel model)
        {
            var user = await _userService.RegisterUser(model);
            return Ok(user);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var user = await _userService.Login(model);

            if (user == null)
            {
                throw new Exception("Invalid Login");
            }

            return Ok(
                new
                {
                    token = GenerateJWT(user)
                });

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> LoginAsync(int id)
        {
            var user = await _userService.GetUserInfo(id);
            return Ok(user);
        }

        private string GenerateJWT(UserLoginResponseModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            // Create JWT

            // get the secret key from appsettings for Azure Key/Vault

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecretKey"]));
            // select the hashing algo

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // get the expiration time
            var expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("ExpirationHours"));

            //create the JWT token with above claims and credentials and expiration time

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Issuer"],
                Audience = _configuration["Audience"],
                Subject = identityClaims,
                Expires = expires,
                SigningCredentials = credentials
            };

            var encodedJWT = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(encodedJWT);
            // Store Application Secrets in Azure Key/Vault
        }
    }
}

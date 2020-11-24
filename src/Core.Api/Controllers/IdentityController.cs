//using Core.Api.Commons;
using Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.DTOs;
using Model.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Core.Api.Controllers
{
    [ApiController]
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser>  _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager; // servicio para el logeo....
        private readonly IConfiguration _configuration;
        public IdentityController(UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signManager,
                                  IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signManager;
            _configuration = configuration;
        }
        public string Index()
        {
            return "Estoy en IdentityController";
        }
        //identity/register
        [HttpPost("register")]
        public async Task<IActionResult> Create(ApplicationUserRegisterDto model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            await _userManager.AddToRoleAsync(user, RoleHelper.Seller);

            if (!result.Succeeded)
            {
                return BadRequest("No se pudo crear el usuario.");
            }
            return Ok();

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(ApplicationUserRegisterDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user != null)
            {
                var check = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (check.Succeeded)
                {
                    return Ok(
                      await GenerateToken(user)
                    );
                }
            }
            return BadRequest("Acceso no valido al sistema !!!"); 
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, role)
                );
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(createdToken);
        }

    }
}

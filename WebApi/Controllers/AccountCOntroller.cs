using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.ViewModel;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountCOntroller : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AccountCOntroller(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
            {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }


            // TODO: this should be refactored to an authController

            [AllowAnonymous]
            [HttpPost("authenticate")]
            public async Task<IActionResult> Authenticate([FromBody] LoginViewModel model)
            {
            //   var user = _userRepository.Authenticate(userDto.Username, userDto.Password);
            var user = await userManager.FindByNameAsync(model.Email);


            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                    return Unauthorized();

                // generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
                new Claim (ClaimTypes.Name, user.Email)
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);


                //return the basic user info and token to store on the client
                return Ok(new { tokenString, user });

            }

        //[AllowAnonymous]
        //[HttpPost("register")]
        //public IActionResult Register([FromBody] UserDto userDto)
        //{
        //    map the dto to the entity
        //        var user = new User
        //        {
        //            Id = userDto.Id,
        //            FirstName = userDto.FirstName,
        //            Username = userDto.Username
        //        };

        //    try
        //    {
        //        _userRepository.Create(user, userDto.Password);
        //        return Ok();
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        }
    }

    


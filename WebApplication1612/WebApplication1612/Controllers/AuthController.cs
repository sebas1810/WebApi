using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BLL.Helpers;
using DAL.Models;
using WebApplication1612.Models;
using Microsoft.AspNetCore.Cors;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace WebApplication1612.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<WebAppUser> userManager;
        private readonly SignInManager<WebAppUser> signInManager;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<WebAppUser> userManager, SignInManager<WebAppUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new WebAppUser { UserName = model.UserName, Email = model.Email };
                    
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return BuildToken(model);
                }
                else
                {
                    return BadRequest("Username or password invalid");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await this.signInManager.PasswordSignInAsync(user.UserName, user.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return BuildToken(user);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private IActionResult BuildToken(UserModel userDetails)
        {
            var secretKey = "10secret_keyParacreaciondetokenJWT$$!1";
            JwtSecurityToken token = Authorization.GetToken(secretKey, userDetails.UserName);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = DateTime.UtcNow.AddHours(1)
            });
        }
    }
}
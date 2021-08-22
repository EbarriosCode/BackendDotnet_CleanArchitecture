using Application.Interfaces;
using Countries.Common.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IJWTService _jwtService;

        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IJWTService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<ActionResult<UserToken>> Create([FromBody] UserInfo model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                // All new users are created with the Admin role
                await _userManager.AddToRoleAsync(user, "Admin");
                
                return Ok("Usuario Creado");
            }
            else
            {
                StringBuilder errors = new();
                foreach (var item in result.Errors)
                    errors.AppendLine($"{item.Description}");

                return BadRequest(errors.ToString());
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await this._jwtService.BuildToken(userInfo);
            }
            else
            {
                //ModelState.AddModelError("Error", "Intento de inicio de sesión no válido.");
                return BadRequest("Intento de inicio de sesión no válido.");
            }
        }
    }
}

using ProductRegistrationService.Domain.Account;
using ProductRegistrationService.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using ProductRegistrationService.Infra.Data.Identity;

namespace ProductRegistrationService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController: ControllerBase
    {
        private readonly IAuthenticate _authentication;
        private readonly ISeedUserRoleInitial _seedUserRoleInitial;

        public TokenController(IAuthenticate authentication,
                               ISeedUserRoleInitial seedUserRoleInitial)
        {
            _authentication = authentication ??
                throw new ArgumentNullException(nameof(authentication));
            _seedUserRoleInitial = seedUserRoleInitial ??
                throw new ArgumentNullException(nameof(seedUserRoleInitial));

            //It will create new roles and users if they do not exists yet.
            //No duplicity will be generated.
            _seedUserRoleInitial.SeedRoles();
            _seedUserRoleInitial.SeedUsers();
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            var result = await _authentication.Authenticate(userInfo.Email, userInfo.Password);

            if(result)
            {
                //return GenerateToken(userInfo);
                return Ok($"User {userInfo.Email} login successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }
    }
}
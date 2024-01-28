using ProductRegistrationService.Domain.Account;
using ProductRegistrationService.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductRegistrationService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController: ControllerBase
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenController> _logger;

        public TokenController(IAuthenticate authentication,
                               IConfiguration configuration,
                               ILogger<TokenController> logger)
        {
            _authentication = authentication ??
                throw new ArgumentNullException(nameof(authentication));
            
            _configuration = configuration;
            _logger = logger;
        }

        // [ApiExplorerSettings(IgnoreApi = true)]
        // [Authorize(Roles = "Admin")]
        // [HttpPost("CreateUser")]
        // public async Task<ActionResult> CreateUser([FromBody] LoginModel userInfo)
        // {

        //     //Unavailable method because this feature does not belongs the goal of this Web API.
        //     //Just register user but does not define the user role.

        //     var result = await _authentication.RegisterUser(userInfo.Email, userInfo.Password);

        //     if(result)
        //     {
        //         return Ok($"User {userInfo.Email} was created successfully");
        //     }
        //     else
        //     {
        //         ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
        //         return BadRequest(ModelState);
        //     }
        // }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            dynamic _return;

            try
            {

                var result = await _authentication.Authenticate(userInfo.Email, userInfo.Password);

                if(result)
                {
                    _return = GenerateToken(userInfo);
                    //_return = Ok($"User {userInfo.Email} login successfully");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                    _return = BadRequest(ModelState);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"TokenController.LoginUser -> Error: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                _return = StatusCode(500, "Internal Server Error");
            }

            return _return;
        }

        private UserToken GenerateToken(LoginModel userInfo)
        {
            //Token Content   
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("anyValue", "fshfkjh"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Generate private key for sign the token
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //Generate digital sign
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //Define the expiration time
            var expiration = DateTime.UtcNow.AddMinutes(10);

            //Create the token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
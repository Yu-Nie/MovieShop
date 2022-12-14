using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var user = await _accountService.RegisterUser(model);
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _accountService.ValidateUser(model);
            if(user != null)
            {
                // create token
                var jwtToken = CreatedJwtToken(user);
                return Ok(new {token = jwtToken});
            }
            // IOS, Android, Angular, React, Java
            // token , JWT (JSON  web token)

            // client will send email/ password to API, POST
            // API will validate the email/ password and if valid then API will create the JWT token and sent to client
            // Angular, React (localstorage or sessionstorage in browser)
            // IOS/ Android, Device
            // when client needs some secure information or needs to perfom any operation that requires users to be
            // authenticated then client needs to send the token to the API in the HTTP Header
            // once the API recieves the token from client it will validate the JWT token and if valid it will send the 
            // data back to the client
            // if JWT token is invalid or token is expired then API will send 401 unauthorized
            throw new UnauthorizedAccessException("Please check email and password");
            // return Unauthorized(new {errorMessage = "Please check email and password"});
        }

        private string CreatedJwtToken(UserLoginSuccessModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim("Country", "USA"),
                new Claim("language", "english"),
                new Claim("isAdmin", (user.Id == 3) ? "true" : "false"),
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            // specify a secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secretKey"]));

            // specify the algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // specify the expiration of the token
            var tokenExpiration = DateTime.UtcNow.AddHours(2);

            // create and object with all the above information to create the token
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = tokenExpiration,
                SigningCredentials = credentials,
                Issuer = "MovieShop, Inc",
                Audience = "MovieShop Clients",
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodeJwt = tokenHandler.CreateToken(tokenDetails);
            return tokenHandler.WriteToken(encodeJwt);
        }
    }
}

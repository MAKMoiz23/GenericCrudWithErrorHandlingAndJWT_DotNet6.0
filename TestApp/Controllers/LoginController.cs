using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using DataAccess.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DataAccess.Data.IDataModel;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
		private readonly IConfiguration _configuration;
		private readonly ILogger<LoginController> _logger;
		private readonly IAuthData _authService ;


		public LoginController(IConfiguration configuration, ILogger<LoginController> logger, IAuthData authService)
		{
			_configuration = configuration;
			_logger = logger;
			_authService = authService;
		}

		[HttpGet("User/{email}/{password}")]
        public async Task<IActionResult> ValidateUser(string email, string password)
		{
			_logger.LogInformation("Getting all data...");
			if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");

			var result = await _authService.GetDataforUserAuth(email, password);

			if (result == null) return Unauthorized("Invalid credentials.");

			var jwt = GenerateJwt(result.ID.ToString(), email, "Admin");

			LoginModel loginModel = new()
			{
				Email = email,
				Password = password,
				Token = jwt,
				ID = result.ID
			};

			return Ok(loginModel);
		}
		[HttpGet("SubUser/{email}/{password}")]
        public async Task<IActionResult> ValidateSubUser(string email, string password)
		{
			_logger.LogInformation("Getting all data...");
			if (!ModelState.IsValid) return BadRequest("Model State is not Valid!");

			var result = await _authService.GetDataforSubUserAuth(email, password);

			if (result == null) return Unauthorized("Invalid credentials.");

			var jwt = GenerateJwt(result.ID.ToString(), email, "Admin");

			LoginModel loginModel = new()
			{
				Email = email,
				Password = password,
				Token = jwt,
				ID = result.ID
			};

			return Ok(loginModel);
		}

		[HttpGet("Logout")]
        public IActionResult Logout()
        {
			// Delete the JwtToken cookie by setting its value to null and its expiration date to a past date
			Response.Cookies.Append("JwtToken", "", new CookieOptions
			{
				Expires = DateTime.Now.AddDays(-1)
			});

			return Ok();
		}
		internal string GenerateJwt(string userId, string email, string role)
		{
			var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetValue<string>("Jwt:Subject")),
				    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
				    new Claim(ClaimTypes.Sid, userId),
				    new Claim(JwtRegisteredClaimNames.Email, email),
				    new Claim(ClaimTypes.Role, role),
					};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
			var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
						_configuration["Jwt:Issuer"],
						_configuration["Jwt:Audience"],
						claims,
						expires: DateTime.UtcNow.AddMinutes(10),
						signingCredentials: signIn);

			string data = new JwtSecurityTokenHandler().WriteToken(token);

			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				SameSite = SameSiteMode.None,
				Secure = true, // make sure to set this to true if using HTTPS
				Expires = DateTime.UtcNow.AddMinutes(10) // set the cookie expiration time
			};

			HttpContext.Response.Cookies.Append("JwtToken", data, cookieOptions);

			return data;
		}
	}
}

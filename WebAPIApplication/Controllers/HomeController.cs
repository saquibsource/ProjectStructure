using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using WebAPIApplication.Security;

namespace WebAPIApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITokenProvider _tokenProvider;
        private readonly IAuthorizationService _authorizationService;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ITokenProvider tokenProvider, IAuthorizationService authorizationService)
        {
            _logger = logger;
            _configuration = configuration;
            _tokenProvider = tokenProvider;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [Authorize]
        public IActionResult Get()
        {
            if (!_tokenProvider.IsTokenFromTrustedAudience(HttpContext))
            {
                return Unauthorized();
            }
            _logger.LogInformation("GetWeatherForecast Information");
            _logger.LogWarning("GetWeatherForecast Warning");
            _logger.LogError("GetWeatherForecast Error");
            return Ok("Token Authorizations Successfull");
        }

        [HttpGet]
        [Route("Generatetoken")]
        public IActionResult Generatetoken()
        {
           
            _logger.LogInformation("Generatetoken Information");

            return Ok(_tokenProvider.CreateToken(HttpContext.User, false,HttpContext.Request.Host.Value));
        }

        [HttpGet]
        [Route("LoggerTest")]
        public IActionResult LoggerTest()
        {
            _logger.LogInformation("GetWeatherForecast DataBase");
            _logger.LogWarning("GetWeatherForecast DataBase");
            _logger.LogError("GetWeatherForecast DataBase");

            return Ok(true);
        }



    }
}
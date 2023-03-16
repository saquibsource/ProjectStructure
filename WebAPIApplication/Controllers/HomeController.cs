using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ITokenProvider tokenProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _tokenProvider = tokenProvider;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [Authorize]
        public IActionResult Get()
        {
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

            return Ok(_tokenProvider.CreateToken(HttpContext.User, false));
        }
    }
}
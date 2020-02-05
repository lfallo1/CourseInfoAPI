using System;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using CourseInfoAPI.Models;
using CourseInfoAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CourseInfoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public LoginController(IConfiguration config, IAuthenticationService authenticationService, IMapper mapper)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto login)
        {
            var user = _authenticationService.AuthenticateUser(login);
            if(null == user)
            {
                return Unauthorized();
            }

            return Ok(new { token = _authenticationService.GenerateJsonWebToken(user), user = _mapper.Map<UserDto>(user)});
        }

        [Authorize]
        [HttpGet]
        public ActionResult<UserDto> getUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var username = identity.Claims.ToList()[0].Value;
            var user = _authenticationService.GetUser(username);
            return Ok(_mapper.Map<UserDto>(user));
        }
    }
}

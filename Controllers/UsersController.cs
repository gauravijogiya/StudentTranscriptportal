using Microsoft.AspNetCore.Mvc;
using StudentTranscriptPortal.Data;
using StudentTranscriptPortal.Helpers;
using StudentTranscriptPortal.Models;
using StudentTranscriptPortal.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentTranscriptPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserLoginService _service;
        private readonly IConfiguration _configuration;

        public UsersController(IUserLoginService service, IConfiguration configuration)
        {
            _service = service;
            _configuration=configuration;
        }
      
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        
        // POST api/<UsersController>
        [HttpPost("Login")]
             
        public IActionResult Login([FromBody] User model)
            {
            model.Role = "Admin";
            Authhelper authhelper=new Authhelper (_configuration);
            // Validate user credentials against the predefined list
            var user = _service.GetUserLoginDetail(model);
                if (user != null)
                {
                   // Generate JWT token if credentials are valid
                    var token = authhelper.GenerateToken(model);
                    return Ok(new { token });
                }

                return Unauthorized("Invalid credentials");
            }
        }

    }


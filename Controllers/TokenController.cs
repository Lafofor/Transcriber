using Microsoft.AspNetCore.Mvc;
using Transcriber.Interfaces;
using Transcriber.Models;

namespace Transcriber.Controllers
{

    [ApiController]
        [Route("api/[controller]")]
        public class TokenController : ControllerBase
        {
            private readonly IJwtGenerator _jwtGenerator;

            public TokenController(IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
            }

            [HttpGet("generate")]
            public IActionResult GenerateToken()
            {
                var user = new AppUser
                {
                    Id = "12345",
                    UserName = "testuser"
                };

                var token = _jwtGenerator.CreateToken(user);

                return Ok(new { Token = token });
            }
        }
    }


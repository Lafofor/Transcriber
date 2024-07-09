using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transcriber.Contracts;
using Transcriber.Interfaces;

namespace Transcriber.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContractRequestController : ControllerBase
    {
        private readonly ITranscriptionService _transcriptionService;

        public ContractRequestController(ITranscriptionService transcriptionService)
        {
            _transcriptionService = transcriptionService;
        }

        [HttpPost("transcription")]
        public async Task<IActionResult> Transcribe([FromBody] TranscriptionRequestDto requestDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _transcriptionService.ProcessAsync(requestDto, cancellationToken);
                var response = new { TranscribedText = result };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}

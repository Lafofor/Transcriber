using Microsoft.AspNetCore.Mvc;

namespace Web.SpeechToTextApi.Areas.v1.Controllers;

[ApiController]
[Route("{area}")]
public sealed class SpeechController : ControllerBase
{
    [HttpPost("{controller}:longrunningrecognize")]
    public async Task<IActionResult> CreateLongRunningRecognizeAsync(CancellationToken token = default)
    {
        return Ok("test");
    }
    
    [HttpPost("{controller}:recognize")]
    public async Task<IActionResult> RecognizeAsync(CancellationToken token = default)
    {
        return Ok("test");
    }
}
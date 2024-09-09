using Core.SpeechToText.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.SpeechToTextApi.Areas.v1.Contracts.Requests;
using Web.SpeechToTextApi.Areas.v1.Contracts.Responses;

namespace Web.SpeechToTextApi.Areas.v1.Controllers;

[Authorize]
[ApiController]
[Route("{area}")]
public sealed class SpeechController : ControllerBase
{
    private readonly ITranscriberService _transcriberService;

    public SpeechController(ITranscriberService transcriberService)
    {
        _transcriberService = transcriberService;
    }
    
    [HttpPost("{controller}:longrunningrecognize")]
    public async Task<IActionResult> CreateLongRunningRecognizeAsync
    (
        [FromBody] LongRunningRecognizeRequest request, 
        CancellationToken token = default
    )
    {
        return StatusCode
        (
            500,
            new { Message = "Disabled." }
        );
    }
    
    [HttpPost("{controller}:recognize")]
    public async Task<ActionResult<RecognizeResponse>> RecognizeAsync
    (
        [FromBody] RecognizeRequest request,
        CancellationToken token = default
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var audio = Convert.FromBase64String(request.Audio.EncodedContent);

        var speech = await _transcriberService.TranscribeAudioAsync(audio, token);

        var results = speech.SpeechSegments.Select
        (
            s => new RecognizeResponse.SpeechRecognitionResult()
            {
                Alternatives = [ 
                    new RecognizeResponse.SpeechRecognitionResult.SpeechRecognitionAlternative()
                    {
                        Transcript = s.Text
                    }
                ],
                LanguageCode = "ru",
                ResultEndTime = $"{s.End.TotalSeconds} s."
            }
        );

        return new RecognizeResponse()
        {
            Results = results,
            TotalBilledTime = $"{speech.Duration.TotalSeconds} s."
        };
    }
}
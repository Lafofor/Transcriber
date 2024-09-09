using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.SpeechToTextApi.Areas.v1.Controllers;

[Authorize]
[ApiController]
[Route("{area}/{controller}/")]
public class OperationsController : ControllerBase
{
    [HttpGet]
    public IActionResult ListOperationsAsync(uint page = 1, uint pageSize = 10, CancellationToken token = default)
    {
        return StatusCode
        (
            500,
            new { Message = "Disabled." }
        );
    }
    
    [HttpGet("{name}")]
    public IActionResult GetOperationAsync(string name, CancellationToken token = default)
    {
        return StatusCode
        (
            500,
            new { Message = "Disabled." }
        );
    }
    
    [HttpPost("{name}:cancel")]
    public IActionResult CancelOperationAsync(string name, CancellationToken token = default)
    {
        return StatusCode
        (
            500,
            new { Message = "Disabled." }
        );
    }
    
    [HttpDelete("{name}")]
    public IActionResult DeleteOperationAsync(string name, CancellationToken token = default)
    {
        return StatusCode
        (
            500,
            new { Message = "Disabled." }
        );
    }
}
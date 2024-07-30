using Microsoft.AspNetCore.Mvc;

namespace Web.SpeechToTextApi.Areas.v1.Controllers;

[ApiController]
[Route("{area}/{controller}/")]
public class OperationsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListOperationsAsync(int page = 1, int pageSize = 10, CancellationToken token = default)
    {
        return Ok($"Поиск всех");
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetOperationAsync(string name, CancellationToken token = default)
    {
        return Ok($"Ищем {name}");
    }
    
    [HttpPost("{name}:cancel")]
    public async Task<IActionResult> CancelOperationAsync(string name, CancellationToken token = default)
    {
        return Ok($"Отменяем {name}");
    }
    
    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteOperationAsync(string name, CancellationToken token = default)
    {
        return Ok($"Удаляем {name}");
    }
}
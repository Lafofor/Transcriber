using Web.SpeechToTextApi.Extensions;

namespace Web;

internal static class Program
{
    internal static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddSpeechToTextApi();
        
        var app = builder.Build();

        app.MapControllers();
        
        app.Run();
    }
}
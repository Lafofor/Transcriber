using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Services.Extensions;
using SoxAudioProcessor.Extensions;
using Web.SpeechToTextApi.Areas.v1.Extensions;
using WhisperSpeechRecognition.Extensions;

namespace Web;

internal static class Program
{
    internal static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        var key = new SymmetricSecurityKey(
            Encoding.UTF32.GetBytes(builder.Configuration["SecurityKey"] ?? throw new ArgumentNullException("Key")));

        var token = new JwtSecurityTokenHandler().CreateEncodedJwt("495-credit", "495-credit", null, null, DateTime.Now.AddDays(30), null, new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));
        
        Console.WriteLine(token);
        
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(builder.Configuration["SecurityKey"] ?? throw new ArgumentNullException("Key"))),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                };
            });

        
        builder.Services.AddSpeechToTextApi();
        builder.Services.AddTransriberService
        (
            b =>
            {
                b.UseWhisperRecognizer
                (
                    builder.Configuration["WhisperModelPath"] ?? throw new ArgumentNullException("modelpath"),
                    "russian",
                    4
                );

                b.AddSoxAudioProcessor
                (
                    builder.Configuration["SoxPath"] ?? throw new ArgumentNullException("sox path"),
                    sox =>
                    {
                        sox.InputFormat.Rate.Frequency = 8000;
                        sox.InputFormat.Type.AudioFormat = "wav";
                        sox.Input.FromStream = true;
        
                        sox.OutputFormat.Rate.Frequency = 16000;
                        sox.OutputFormat.Type.AudioFormat = "wav";
                        sox.Output.FromStream = true;

                        sox.Effects.Normalize.DecibelLevel = -0.5;
                        sox.Effects.Compand.AddTransferFunction(0.3, 1, -90, -90, -70, -70, -60, -20, 0, 0);
                        sox.Effects.Compand.Gain = -5;
                        sox.Effects.Compand.InitialLevel = 0;
                        sox.Effects.Compand.Delay = 0.2;
                    }
                );
            }
        );
        
        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
        app.Run();
    }
}
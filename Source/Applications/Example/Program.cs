using Pipeline.Implementations;
using SoxAudioProcessor.Extensions;
using WhisperSpeechRecognition.Extensions;

namespace Example;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = new RecognitionPipelineBuilder();

        builder.UseWhisperRecognizer
        (
            @"C:\Users\Анатолий\Desktop\ML\Whisper\ggml-large-ru.bin",
            "russian",
            8
        );

        builder.AddSoxAudioProcessor
        (
            @"C:\Program Files (x86)\sox-14-4-2\sox.exe",
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
        
        var pipeline = builder.Build();

        var speech = await pipeline.ProcessAsync
        (
            File.OpenRead(@"C:\Users\Анатолий\Desktop\ML\Аудио\Input.wav")
        );
    }
}
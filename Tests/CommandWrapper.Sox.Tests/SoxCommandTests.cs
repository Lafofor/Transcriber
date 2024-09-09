using CommandWrapper.Sox.Types;

namespace CommandWrapper.Sox.Tests;

public class SoxCommandTests
{
    [Fact]
    public async Task InputFileOutputFile()
    {
        var sox = new SoxCommand("C:\\Program Files (x86)\\sox-14-4-2\\sox.exe");

        sox.Input.Path = "C:\\Users\\Анатолий\\Desktop\\Основные\\Transcriber\\Tests\\CommandWrapper.Sox.Tests\\Assets\\Input.wav";
        
        sox.OutputFormat.Rate.Frequency = 16000;

        sox.Effects.Normalize.DecibelLevel = -0.5;
        sox.Effects.Compand.AddTransferFunction(0.3, 1, -90, -90, -70, -70, -60, -20, 0, 0);
        sox.Effects.Compand.Gain = -5;
        sox.Effects.Compand.InitialLevel = 0;
        sox.Effects.Compand.Delay = 0.2;
        
        sox.Output.Path = "C:\\Users\\Анатолий\\Desktop\\Основные\\Transcriber\\Tests\\CommandWrapper.Sox.Tests\\Assets\\Input_normalized_file2file.wav";

        using (var process = sox.Run())
        {
            await process.ProcessAudioAsync();
        }
    }
    
    [Fact]
    public async Task InputStreamOutputFile()
    {
        var sox = new SoxCommand("C:\\Program Files (x86)\\sox-14-4-2\\sox.exe");

        sox.Input.FromStream = true;
        
        sox.OutputFormat.Rate.Frequency = 16000;

        sox.Effects.Normalize.DecibelLevel = -0.5;
        sox.Effects.Compand.AddTransferFunction(0.3, 1, -90, -90, -70, -70, -60, -20, 0, 0);
        sox.Effects.Compand.Gain = -5;
        sox.Effects.Compand.InitialLevel = 0;
        sox.Effects.Compand.Delay = 0.2;
        
        sox.Output.Path = "C:\\Users\\Анатолий\\Desktop\\Основные\\Transcriber\\Tests\\CommandWrapper.Sox.Tests\\Assets\\Input_normalized_stream2file.wav";

        using (var process = sox.Run())
        {
            await process.ProcessAudioAsync(File.OpenRead("C:\\Users\\Анатолий\\Desktop\\Основные\\Transcriber\\Tests\\CommandWrapper.Sox.Tests\\Assets\\Input.wav"));
        }
    }
    
    [Fact]
    public async Task InputFileOutputStream()
    {
        var sox = new SoxCommand("C:\\Program Files (x86)\\sox-14-4-2\\sox.exe");

        sox.Input.Path = "C:\\Users\\Анатолий\\Desktop\\Основные\\Transcriber\\Tests\\CommandWrapper.Sox.Tests\\Assets\\Input.wav";
        
        sox.OutputFormat.Rate.Frequency = 16000;

        sox.Effects.Normalize.DecibelLevel = -0.5;
        sox.Effects.Compand.AddTransferFunction(0.3, 1, -90, -90, -70, -70, -60, -20, 0, 0);
        sox.Effects.Compand.Gain = -5;
        sox.Effects.Compand.InitialLevel = 0;
        sox.Effects.Compand.Delay = 0.2;

        sox.OutputFormat.Type.AudioFormat = "wav";
        sox.Output.FromStream = true;

        using (var process = sox.Run())
        {
            using var fromStdout = await process.ProcessAudioAsync();
            
            using var output = File.Create("C:\\Users\\Анатолий\\Desktop\\Основные\\Transcriber\\Tests\\CommandWrapper.Sox.Tests\\Assets\\Input_normalized_file2stream.wav");
            
            await fromStdout.CopyToAsync(output, CancellationToken.None);
        }
    }
    
    [Fact]
    public async Task InputStreamOutputStream()
    {
        var sox = new SoxCommand("C:\\Program Files (x86)\\sox-14-4-2\\sox.exe");

        sox.Input.FromStream = true;
        
        sox.OutputFormat.Rate.Frequency = 16000;

        sox.Effects.Normalize.DecibelLevel = -0.5;
        sox.Effects.Compand.AddTransferFunction(0.3, 1, -90, -90, -70, -70, -60, -20, 0, 0);
        sox.Effects.Compand.Gain = -5;
        sox.Effects.Compand.InitialLevel = 0;
        sox.Effects.Compand.Delay = 0.2;

        sox.OutputFormat.Type.AudioFormat = "wav";
        sox.Output.FromStream = true;

        using (var process = sox.Run())
        {
            using var fromStdout = await process.ProcessAudioAsync(File.OpenRead("C:\\Users\\Анатолий\\Desktop\\Основные\\Transcriber\\Tests\\CommandWrapper.Sox.Tests\\Assets\\Input.wav"));
            
            using var output = File.Create("C:\\Users\\Анатолий\\Desktop\\Основные\\Transcriber\\Tests\\CommandWrapper.Sox.Tests\\Assets\\Input_normalized_stream2stream.wav");
            
            await fromStdout.CopyToAsync(output, CancellationToken.None);
        }
    }
    
    [Fact]
    public void NormalizeAndCompandTest()
    {
        var expect = "record.wav -r 16000 record-normalized.wav norm -0.5 compand 0.3,1 -90,-90,-70,-70,-60,-20,0,0 -5 0 0.2";

        var sox = new SoxCommand("sox.exe");

        sox.Input.Path = "record.wav";
        
        sox.OutputFormat.Rate.Frequency = 16000;
        sox.Output.Path = "record-normalized.wav";

        sox.Effects.Normalize.DecibelLevel = -0.5;
        sox.Effects.Compand.AddTransferFunction(0.3, 1, -90, -90, -70, -70, -60, -20, 0, 0);
        sox.Effects.Compand.Gain = -5;
        sox.Effects.Compand.InitialLevel = 0;
        sox.Effects.Compand.Delay = 0.2;
        
        Assert.Equal(expect, sox.Arguments);
    }
}
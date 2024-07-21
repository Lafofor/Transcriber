using CommandWrapper.Sox.Types;

namespace CommandWrapper.Sox.Tests;

public class SoxCommandTests
{
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
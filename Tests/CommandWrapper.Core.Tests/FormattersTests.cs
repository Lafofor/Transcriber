using CommandWrapper.Core.Implementations.Formatters;
using CommandWrapper.Core.Tests.StubImplementations;

namespace CommandWrapper.Core.Tests;

public sealed class FormattersTests
{
    [Fact]
    public void DefaultFormat_OnlyValue()
    {
        var expect = "arg-value";

        var argument = new StubArgument(ArgumentFormatters.Default, null, "arg-value", true);

        Assert.Equal(expect, ArgumentFormatters.Default.Format(argument));
    }

    [Fact]
    public void DefaultFormat_OnlyName()
    {
        var expect = "arg";

        var argument = new StubArgument(ArgumentFormatters.Default, "arg", null, true);

        Assert.Equal(expect, ArgumentFormatters.Default.Format(argument));
    }

    [Fact]
    public void DefaultFormat_NameAndValue()
    {
        var expect = "arg arg-value";

        var argument = new StubArgument(ArgumentFormatters.Default, "arg", "arg-value", true);

        Assert.Equal(expect, ArgumentFormatters.Default.Format(argument));
    }

    [Fact]
    public void LinuxLongFormat_OnlyName()
    {
        var expect = "--arg";

        var argument = new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg", null, true);

        Assert.Equal(expect, ArgumentFormatters.Linux.LongArgument.Format(argument));
    }

    [Fact]
    public void LinuxLongFormat_NameAndValue()
    {
        var expect = "--arg arg-value";

        var argument = new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg", "arg-value", true);

        Assert.Equal(expect, ArgumentFormatters.Linux.LongArgument.Format(argument));
    }

    [Fact]
    public void LinuxShortFormat_OnlyName()
    {
        var expect = "-a";

        var argument = new StubArgument(ArgumentFormatters.Linux.ShortArgument, "a", null, true);

        Assert.Equal(expect, ArgumentFormatters.Linux.ShortArgument.Format(argument));
    }

    [Fact]
    public void LinuxShortFormat_NameAndValue()
    {
        var expect = "-a arg-value";

        var argument = new StubArgument(ArgumentFormatters.Linux.ShortArgument, "a", "arg-value", true);

        Assert.Equal(expect, ArgumentFormatters.Linux.ShortArgument.Format(argument));
    }
}
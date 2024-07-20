using CommandWrapper.Core.Abstractions;
using CommandWrapper.Core.Implementations.Formatters;
using CommandWrapper.Core.Tests.StubImplementations;

namespace CommandWrapper.Core.Tests;

public class CommandTests
{
    [Fact]
    public void Arguments_EmptyPosition_Test()
    {
        var expect = "-a1 --arg3 test INPUT OUTPUT";

        CommandArgument[] beforeInputArguments =
        [
            new StubArgument(ArgumentFormatters.Linux.ShortArgument, "a1", null, true),
            new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg2", null, false),
            new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg3", "test", true)
        ];

        var beforeInputPosition = new StubPosition(0, beforeInputArguments);

        var inputArgument = new StubArgument(ArgumentFormatters.Default, null, "INPUT", true);

        var inputPosition = new StubPosition(1, [inputArgument]);

        CommandArgument[] afterInputArguments =
        [
            new StubArgument(ArgumentFormatters.Linux.ShortArgument, "a4", null, false),
            new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg5", null, false),
            new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg6", "new_test", false)
        ];

        var afterInputPosition = new StubPosition(2, afterInputArguments);

        var outputArgument = new StubArgument(ArgumentFormatters.Default, null, "OUTPUT", true);

        var outputPosition = new StubPosition(3, [outputArgument]);

        SortedSet<CommandPosition> positions =
        [
            beforeInputPosition,
            inputPosition,
            afterInputPosition,
            outputPosition
        ];

        var command = new StubCommand
        (
            "cmd.exe",
            positions
        );

        Assert.Equal(expect, command.Arguments);
    }

    [Fact]
    public void Arguments_Partial_Test()
    {
        var expect = "-a1 --arg3 test INPUT -a4 --arg5 OUTPUT";

        CommandArgument[] beforeInputArguments =
        [
            new StubArgument(ArgumentFormatters.Linux.ShortArgument, "a1", null, true),
            new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg2", null, false),
            new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg3", "test", true)
        ];

        var beforeInputPosition = new StubPosition(0, beforeInputArguments);

        var inputArgument = new StubArgument(ArgumentFormatters.Default, null, "INPUT", true);

        var inputPosition = new StubPosition(1, [inputArgument]);

        CommandArgument[] afterInputArguments =
        [
            new StubArgument(ArgumentFormatters.Linux.ShortArgument, "a4", null, true),
            new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg5", null, true),
            new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg6", "new_test", false)
        ];

        var afterInputPosition = new StubPosition(2, afterInputArguments);

        var outputArgument = new StubArgument(ArgumentFormatters.Default, null, "OUTPUT", true);

        var outputPosition = new StubPosition(3, [outputArgument]);

        SortedSet<CommandPosition> positions =
        [
            beforeInputPosition,
            inputPosition,
            afterInputPosition,
            outputPosition
        ];

        var command = new StubCommand
        (
            "cmd.exe",
            positions
        );

        Assert.Equal(expect, command.Arguments);
    }

    [Fact]
    public void Arguments_All_Test()
    {
        var expect = "-a1 --arg2 --arg3 test INPUT -a4 --arg5 --arg6 new_test OUTPUT";

        CommandArgument[] beforeInputArguments =
        [
            new StubArgument(ArgumentFormatters.Linux.ShortArgument, "a1", null, true),
            new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg2", null, true),
            new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg3", "test", true)
        ];

        var beforeInputPosition = new StubPosition(0, beforeInputArguments);

        var inputArgument = new StubArgument(ArgumentFormatters.Default, null, "INPUT", true);

        var inputPosition = new StubPosition(1, [inputArgument]);

        CommandArgument[] afterInputArguments =
        [
            new StubArgument(ArgumentFormatters.Linux.ShortArgument, "a4", null, true),
            new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg5", null, true),
            new StubArgument(ArgumentFormatters.Linux.LongArgument, "arg6", "new_test", true)
        ];

        var afterInputPosition = new StubPosition(2, afterInputArguments);

        var outputArgument = new StubArgument(ArgumentFormatters.Default, null, "OUTPUT", true);

        var outputPosition = new StubPosition(3, [outputArgument]);

        SortedSet<CommandPosition> positions =
        [
            beforeInputPosition,
            inputPosition,
            afterInputPosition,
            outputPosition
        ];

        var command = new StubCommand
        (
            "cmd.exe",
            positions
        );

        Assert.Equal(expect, command.Arguments);
    }
}
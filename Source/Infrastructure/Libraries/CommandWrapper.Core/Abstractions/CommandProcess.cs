using System.Diagnostics;

namespace CommandWrapper.Core.Abstractions;

/// <summary>
///     Процесс команды
/// </summary>
public abstract class CommandProcess
{
    /// <summary>
    ///     Используемые аргументы
    /// </summary>
    public readonly string Arguments;

    /// <summary>
    ///     Порожденный процесс
    /// </summary>
    protected readonly Process CreatedProcess;

    /// <summary>
    ///     Путь к исполняемому файлу
    /// </summary>
    public readonly string ExecutePath;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="executePath">Путь к файлу</param>
    /// <param name="arguments">Аргументы</param>
    /// <exception cref="ArgumentNullException">Не удалось создать процесс</exception>
    protected internal CommandProcess(string executePath, string arguments)
    {
        ExecutePath = executePath;
        Arguments = arguments;

        var startInfo = new ProcessStartInfo
        {
            FileName = executePath,
            Arguments = arguments
        };

        CreatedProcess = Process.Start(startInfo) ?? throw new ArgumentNullException(nameof(CreatedProcess));
    }

    /// <summary>
    ///     Поток входа (stdin)
    /// </summary>
    protected internal StreamWriter StandardInput => CreatedProcess.StandardInput;

    /// <summary>
    ///     Поток выхода (stdout)
    /// </summary>
    protected internal StreamReader StandardOutput => CreatedProcess.StandardOutput;

    /// <summary>
    ///     Поток ошибок (stdout)
    /// </summary>
    protected internal StreamReader StandardError => CreatedProcess.StandardError;

    /// <summary>
    ///     Полное описание команды
    /// </summary>
    public virtual string FullCommand => $"{ExecutePath} {Arguments}";

    /// <summary>
    ///     Освобождение ресурсов
    /// </summary>
    public virtual void Dispose()
    {
        CreatedProcess?.Dispose();
    }
}
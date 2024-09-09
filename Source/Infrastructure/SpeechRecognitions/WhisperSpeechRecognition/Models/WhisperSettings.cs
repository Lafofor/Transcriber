﻿namespace WhisperSpeechRecognition.Models;

/// <summary>
/// Настройки модели
/// </summary>
public sealed class WhisperSettings
{
    /// <summary>
    /// Промпт к модели
    /// </summary>
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    /// Количество потоков
    /// </summary>
    public int Threads { get; set; } = 1;

    /// <summary>
    /// Язык
    /// </summary>
    public string Language { get; set; } = string.Empty;
}
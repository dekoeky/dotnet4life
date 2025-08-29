using Microsoft.Extensions.Logging;

namespace SharedLibrary.Logging;

public sealed class NoopProvider(bool enabled) : ILoggerProvider
{
    private sealed class NoopLogger(bool enabled) : ILogger
    {
        public IDisposable BeginScope<TState>(TState state) => default!;
        public bool IsEnabled(LogLevel logLevel) => enabled;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            /* discard */
        }
    }
    public ILogger CreateLogger(string categoryName) => new NoopLogger(enabled);
    public void Dispose() { }
}
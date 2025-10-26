using Microsoft.Extensions.Logging;

namespace Stofipy.App.Helpers;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly string _logFilePath;
    private readonly object _lock = new();

    public FileLoggerProvider(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    public ILogger CreateLogger(string categoryName)
        => new FileLogger(_logFilePath, _lock, categoryName);

    public void Dispose() { }

    private class FileLogger : ILogger
    {
        private readonly string _filePath;
        private readonly object _lock;
        private readonly string _category;

        public FileLogger(string filePath, object fileLock, string category)
        {
            _filePath = filePath;
            _lock = fileLock;
            _category = category;
        }

        public IDisposable BeginScope<TState>(TState state) => null!;
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId,
            TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {_category}: {formatter(state, exception)}";
            if (exception != null)
                message += Environment.NewLine + exception;

            lock (_lock)
            {
                File.AppendAllText(_filePath, message + Environment.NewLine);
            }
        }
    }
}
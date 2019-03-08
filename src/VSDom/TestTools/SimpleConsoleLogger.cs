using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions.Internal;
using System;

namespace VSDom.TestTools
{
    internal class SimpleConsoleLogger : ILogger
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) { return; }
            Guard.NotNull(formatter, nameof(formatter));

            var message = formatter(state, exception);

            if (!string.IsNullOrEmpty(message) || exception != null)
            {
                WriteMessage(logLevel, null, eventId.Id, message, exception);
            }
        }

        public virtual void WriteMessage(LogLevel logLevel, string logName, int eventId, string message, Exception exception)
        {
            Console.WriteLine($"{logLevel.ToString().ToUpperInvariant(),-10} {message}");
            
            if (exception != null)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;
        public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;
    }
}

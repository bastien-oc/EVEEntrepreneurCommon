using System;

namespace EntrepreneurCommon.Logging
{
    public enum LogLevel
    {
        Debug,
        Trace,
        Info,
        Warning,
        Error,
        Critical
    }
    public interface ILogger
    {
        bool OutputToConsole { get; set; }
        LogLevel ThresholdLevel { get; set; }
        
        void Log(object message, Exception exception = null, LogLevel logLevel = LogLevel.Info);
        
        void Debug(object message, Exception exception = null);
        void Trace(object message, Exception exception = null);
        void Info(object message, Exception exception = null);
        void Warn(object message, Exception exception = null);
        void Error(object message, Exception exception = null);
        void Critical(object message, Exception exception = null);
    }
    
    public class ConsoleLogger : ILogger
    {
        public bool OutputToConsole { get; set; } = true;
        public LogLevel ThresholdLevel { get; set; }
        
        public void Log(object message, Exception exception = null, LogLevel logLevel = LogLevel.Info)
        {
            if (OutputToConsole) {Console.WriteLine($"[{DateTime.Now}] [{logLevel}] {message}");}
        }

        public void Debug(object message, Exception exception = null)
        {
            #if DEBUG
            Log(message, exception, LogLevel.Debug);
            #endif
        }

        public void Trace(object message, Exception exception = null)
        {
            Log(message, exception, LogLevel.Trace);
        }

        public void Info(object message, Exception exception = null)
        {
            Log(message, exception, LogLevel.Info);
        }

        public void Warn(object message, Exception exception = null)
        {
            Log(message, exception, LogLevel.Warning);
        }

        public void Error(object message, Exception exception = null)
        {
            Log(message, exception, LogLevel.Error);
        }

        public void Critical(object message, Exception exception = null)
        {
            Log(message, exception, LogLevel.Critical);
        }
    }
}

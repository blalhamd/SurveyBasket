namespace Survey.Core.Logging
{
    public interface ILoggerService
    {
        void Log(LogType logType,string message,params object[]? parameters);
        void LogInfo(string message, params object[]? parameters);
        void LogError(string message, params object[]? parameters);
        void LogWarning(string message, params object[]? parameters);
        void LogDebug(string message, params object[]? parameters);
    }
}

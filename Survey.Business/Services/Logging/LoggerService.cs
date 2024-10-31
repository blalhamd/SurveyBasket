namespace Survey.Business.Services.Logging
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public LoggerService(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // will change before production to info or warning
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(_configuration, new ConfigurationReaderOptions() { SectionName = "Serilog" })
                .CreateLogger();
        }

        public void Log(LogType logType, string message, params object[]? parameters)
        {
            try
            {
                switch (logType)
                {
                    case LogType.INFO:
                        _logger.Information(message, parameters);
                        break;
                    case LogType.WARNING:
                        _logger.Warning(message, parameters);
                        break;
                    case LogType.DEBUG:
                        _logger.Debug(message, parameters);
                        break;
                    case LogType.ERROR:
                        _logger.Error(message, parameters);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    EventLog eventLog = new EventLog(this.GetType().FullName, System.Environment.MachineName);

                    eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                }
                catch { }
            }

        }

        public void LogDebug(string message, params object[]? parameters)
        {
            Log(LogType.DEBUG, message, parameters);
        }

        public void LogError(string message, params object[]? parameters)
        {
            Log(LogType.ERROR, message, parameters);
        }

        public void LogInfo(string message, params object[]? parameters)
        {
            Log(LogType.INFO, message, parameters);
        }

        public void LogWarning(string message, params object[]? parameters)
        {
            Log(LogType.WARNING, message, parameters);
        }
    }
}

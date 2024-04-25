namespace LibrarySystem.Client.LoggingFeature
{
    public class Logging : ILogging
    {
        private readonly ILogger<Logging> _logger;

        public Logging(ILogger<Logging> logger)
        {
            _logger = logger;
        }
        public void WriteMessage(string message)
        {
            _logger.LogInformation($"Logging.WriteMessage Message: {message}");
        }
    }
}

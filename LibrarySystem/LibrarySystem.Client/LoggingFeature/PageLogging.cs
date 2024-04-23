namespace LibrarySystem.Client.LoggingFeature
{
    public class PageLogging
    {
        private readonly ILogger _logger;

        public PageLogging(ILogger<PageLogging> logger)
        {
            _logger = logger;
        }

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
            Message = $"Home page visited at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
            Console.WriteLine("henlo worl");
        }
    }
}

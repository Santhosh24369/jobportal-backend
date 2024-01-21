using Microsoft.Extensions.Options;

namespace jobportal_backend
{
    public class LogURLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogURLMiddleware> _logger;
        public LogURLMiddleware(RequestDelegate next, ILoggerFactory loggerFactory
            )
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<LogURLMiddleware>() ??
            throw new ArgumentNullException(nameof(loggerFactory));


        }
        public async Task InvokeAsync(HttpContext context, IOptions<JobSeekDatabaseSettings> jobSeekDatabaseSettings)
        {
            string _fil = jobSeekDatabaseSettings.Value.Log_FilePath;

            _logger.LogInformation($"Request URL: {Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request)}");


            Console.WriteLine("hi45" + _fil);
            using (StreamWriter writer = new StreamWriter(_fil, true))
            {
                writer.WriteLine($" at {DateTime.Now}");
                writer.WriteLine($"{Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request)}");
                writer.WriteLine("--------------------------------------------------");
            }
            await this._next(context);

        }

    }
    public static class LogURLMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogUrl(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogURLMiddleware>();
        }
    }
}

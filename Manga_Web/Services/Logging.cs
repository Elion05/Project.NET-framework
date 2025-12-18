using MangaBook_Models;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using MangaBook_Models.Data;




namespace Manga_Web.Services
{
    public class DbLoggerOptions
    {
        public DbLoggerOptions()
        {
        }
    }

    [ProviderAlias("Database")]
    public class DbLoggerProvider : ILoggerProvider
    {
        public readonly DbLoggerOptions Options;
        private readonly MangaDbContext _context;

        public DbLoggerProvider(IOptions<DbLoggerOptions> options)
        {
            Options = options.Value;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(this);
        }

        public void Dispose()
        {
        }
    }

    public class DbLogger : ILogger
    {
        private readonly DbLoggerProvider _dbLoggerProvider;
        private readonly MangaDbContext _context;

        static public LogLevel DefaultLogLevel = LogLevel.Warning;


        public DbLogger([NotNull] DbLoggerProvider dbLoggerProvider)
        {
            _dbLoggerProvider = dbLoggerProvider;
            _context = new MangaDbContext();
        }


        //aanwezig voor interface, maar wordt niet gebruikt
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        //moet er ingelogd worden voor dit logniveau?
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= DefaultLogLevel && logLevel != LogLevel.None;
        }


        //de eigenlijke logica om te loggen naar de database
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
            {
                LogError error = new LogError
                {
                    Application = "MangaWeb",
                    DeviceName = Globals.App.Environment.EnvironmentName,
                    ThreadId = Environment.CurrentManagedThreadId,
                    LogLevel = logLevel.ToString(),
                    EventId = eventId.Id,
                    EventName = eventId.Name,
                    Message = formatter(state, exception),
                    ExceptionMessage = exception?.Message,
                    StackTrace = exception?.StackTrace,
                    Source = exception?.Source,
                    TimeStamp = DateTime.UtcNow
                };
                _context.LogErrors.Add(error);
                _context.SaveChanges();
            }
        }
    }

    public static class DbLoggerExtensions
    {
        public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder, Action<DbLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }

}
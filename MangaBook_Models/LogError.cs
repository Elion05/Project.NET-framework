using System.ComponentModel.DataAnnotations;

namespace MangaBook_Models
{
    public class LogError
    // Model toegevoegd om fouten te loggen in de database
    {
        public int Id { get; set; }
        [Display(Name = "TimeStamp", ResourceType = typeof(Resources.Models.LogError))]
        public DateTime TimeStamp { get; set; }
        
        [Display(Name = "DeviceName", ResourceType = typeof(Resources.Models.LogError))]
        public string DeviceName { get; set; } = string.Empty;
        
        [Display(Name = "Application", ResourceType = typeof(Resources.Models.LogError))]
        public string Application { get; set; }
        
        [Display(Name = "LogLevel", ResourceType = typeof(Resources.Models.LogError))]
        public string LogLevel { get; set; }
        public int ThreadId { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        [Display(Name = "ExceptionMessage", ResourceType = typeof(Resources.Models.LogError))]
        public string? ExceptionMessage { get; set; }
        public string? StackTrace { get; set; }
        public string? Source { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Message", ResourceType = typeof(Resources.Models.LogError))]
        public string Message { get; set; }
    }
}

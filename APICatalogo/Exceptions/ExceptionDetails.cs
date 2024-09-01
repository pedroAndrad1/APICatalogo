using System.Text.Json;

namespace APICatalogo.Exceptions
{
    public class ExceptionDetails
    {
        public int StatusCode { get; init; }
        public string? Message { get; init; } = string.Empty;
        public string? StackTrace {  get; init; } = string.Empty;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

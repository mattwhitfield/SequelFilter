namespace SequelFilter
{
    using System.Globalization;
    using Irony;

    public static class LogMessageExtensions
    {
        public static string ToErrorMessage(this LogMessage message)
        {
            return $"Error at ({message.Location.Line + 1}:{message.Location.Column + 1}): {message.Message}";
        }
    }
}

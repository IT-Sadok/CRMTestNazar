namespace CRM.Exceptions;
public class LogInFailedException : Exception
{
    public LogInFailedException(string message) : base(message) { }
}
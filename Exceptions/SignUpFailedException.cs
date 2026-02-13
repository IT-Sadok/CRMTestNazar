namespace CRM.Exceptions;
public class SignUpFailedException : Exception
{
    public SignUpFailedException(string message) : base(message) { }   
}
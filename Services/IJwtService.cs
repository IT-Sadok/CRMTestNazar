namespace CRM.Services;

public interface IJwtService
{
    public string GenereateJwtToken(string userName);
}
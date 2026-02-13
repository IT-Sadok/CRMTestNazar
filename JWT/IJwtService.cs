namespace CRM.Services.JWT;

public interface IJwtService
{
    public string GenereateJwtToken(string userName);
}
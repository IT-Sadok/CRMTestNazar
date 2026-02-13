using CRM.Models;


namespace CRM.Services.Account;
public interface ILogInService
{
    public Task<AuthenticationModel> LogInEmployeeAsync(string email, string password);
}
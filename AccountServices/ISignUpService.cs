using CRM.Data;
using CRM.Models;


namespace CRM.Services.Account;
public interface ISignUpService
{
    public Task<AuthenticationModel> RegisterEmployeeAsync(EmployeeDto employeeDto);
}
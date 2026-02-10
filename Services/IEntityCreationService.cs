using CRM.Data;
using CRM.Models;


namespace CRM.Services;
public interface IEntityCreationService
{
    public Task<AuthenticationModel> RegisterEmployeeAsync(EmployeeDto employeeDto);
}
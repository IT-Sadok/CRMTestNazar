using CRM.Data;
using CRM.Models;
using Microsoft.AspNetCore.Identity;


namespace CRM.Services;
public class EmployeeService : IEntityCreationService
{
    private readonly UserManager<EmployeeModel> _employeeManager;
    private readonly IJwtService _jwtService;
    

    public EmployeeService(UserManager<EmployeeModel> employeeManager, IJwtService jwtService)
    {
        _employeeManager = employeeManager; 
        _jwtService      = jwtService;
    }

    public async Task<AuthenticationModel> RegisterEmployeeAsync(EmployeeDto employeeDto)
    {
        var user = new EmployeeModel
        {
            UserName = employeeDto.Email.ToLowerInvariant(),
            Email    = employeeDto.Email.ToLowerInvariant(),
            AccountModel = new AccountModel()
        };

        var result = await _employeeManager.CreateAsync(user, employeeDto.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException(
                string.Join(", ", result.Errors.Select(e => e.Code))
            );
        }

        return new AuthenticationModel
        {
            AccessToken = _jwtService.GenereateJwtToken(user.UserName)
        };
    }
} 
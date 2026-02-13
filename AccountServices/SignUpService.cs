using CRM.Data;
using CRM.Exceptions;
using CRM.Models;
using Microsoft.AspNetCore.Identity;
using CRM.Services.JWT;
    

namespace CRM.Services.Account;
public class SignUpService : ISignUpService
{
    private readonly UserManager<EmployeeModel> _employeeManager;
    private readonly IJwtService _jwtService;
    

    public SignUpService(UserManager<EmployeeModel> employeeManager, IJwtService jwtService)
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

        if (!result.Succeeded) throw new SignUpFailedException("Sign Up failed");

        return new AuthenticationModel
        {
            AccessToken = _jwtService.GenereateJwtToken(user.UserName)
        };
    }
} 
using CRM.Exceptions;
using CRM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CRM.Services.JWT;
    

namespace CRM.Services.Account;
public class LogInService : ILogInService 
{
    private readonly UserManager<EmployeeModel> _employeeManager;
    private readonly IJwtService _jwtService;
    private readonly ApplicationContext _applicationContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    
    public LogInService
    (
        UserManager<EmployeeModel> employeeManager, 
        IJwtService jwtService,
        ApplicationContext applicationContext,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _employeeManager     = employeeManager;
        _jwtService          = jwtService;
        _applicationContext  = applicationContext;
        _httpContextAccessor = httpContextAccessor;
    }
    
    
    public async Task<AuthenticationModel> LogInEmployeeAsync(string email, string password)
    {
        var user = await _applicationContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        
        if (user == null) throw new LogInFailedException("User not found");
        
        bool isPasswordValid = await _employeeManager.CheckPasswordAsync(user, password);
        
        if (!isPasswordValid) throw new LogInFailedException("Password is not valid");

        return new AuthenticationModel
        {
            AccessToken = _jwtService.GenereateJwtToken(user.UserName!)
        };
    }
}
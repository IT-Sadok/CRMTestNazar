using CRM.Data;
using CRM.Services.Account;
using Microsoft.AspNetCore.Mvc;


namespace CRM.EndPoints;
public class LogInEndPointService : ILogInEndPointService
{
    public void LogIn(WebApplication app)
    {
        app.MapPost
        (
            "log-in", 
            (
                [FromServices] ILogInService logInService, 
                [FromBody] EmployeeDto model
            ) => logInService.LogInEmployeeAsync(model.Email, model.Password)
        );
    }
}
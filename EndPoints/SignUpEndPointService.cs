using CRM.Data;
using CRM.Services.Account;
using Microsoft.AspNetCore.Mvc;


namespace CRM.EndPoints;
public class SignUpEndPointService : ISignUpEndPointService
{
    public void SignUp(WebApplication app)
    {
        app.MapPost
        (
            "sign-up", 
            (
                [FromServices] ISignUpService signUpService,
                [FromBody] EmployeeDto model
            ) => signUpService.RegisterEmployeeAsync(model)
        );
    }
}
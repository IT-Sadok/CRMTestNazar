using Microsoft.AspNetCore.Identity;


namespace CRM.Models;
public class EmployeeModel : IdentityUser<int>
{
    public int AccountId { get; set; }
    public AccountModel AccountModel { get; set; }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;


namespace CRM.DependencyInjection;
public interface IDependencyInjectionService
{
    public void AddDbContext(ConfigurationManager configurationManager);
    public void AddEndpointsApi();
    public void AddSwagger();
    public void AddHttpContext();
    public void AddJwtService();
    public void AddAccountServices();
    public IdentityBuilder AddIdentity();
    public void AddEntityFrameworkStores(IdentityBuilder identityBuilder);
    public void AddAuthorization();
    public void ConfigurePasswordSettings();
    public AuthenticationBuilder AddAuthentication();
    public void AddJwtBearer(AuthenticationBuilder authenticationBuilder, ConfigurationManager configurationManager);
    public void ConfigureJwtOptions(ConfigurationManager configurationManager);
}
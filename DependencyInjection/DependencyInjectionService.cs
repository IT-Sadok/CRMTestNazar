using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using CRM.Options;
using CRM.Services.JWT;
using CRM.Services.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


namespace CRM.DependencyInjection;
public class DependencyInjectionService : IDependencyInjectionService
{
    private readonly IServiceCollection _services;
    
    
    public DependencyInjectionService(IServiceCollection services) => _services = services;

    public void AddDbContext(ConfigurationManager configurationManager)
    {
        _services.AddDbContext<ApplicationContext>
        (
            options => options.UseNpgsql(configurationManager.GetConnectionString("DataBase"))
        );
    }

    public void AddEndpointsApi() => _services.AddEndpointsApiExplorer();

    public void AddSwagger() => _services.AddSwaggerGen();
    
    public void AddHttpContext() => _services.AddHttpContextAccessor(); 
    
    public void AddJwtService() => _services.AddScoped<IJwtService, JwtService>();

    public void AddAccountServices()
    {
        _services.AddScoped<ISignUpService, SignUpService>();
        _services.AddScoped<ILogInService, LogInService>();
    }

    public IdentityBuilder AddIdentity()
    {
        return _services.AddIdentity<EmployeeModel, IdentityRole<int>>
        (
            options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            }
        );
    }

    public void AddEntityFrameworkStores(IdentityBuilder identityBuilder)
    {
        identityBuilder.AddEntityFrameworkStores<ApplicationContext>();
    }
    
    public void AddAuthorization() => _services.AddAuthorization();
    
    public void ConfigurePasswordSettings()
    {
        _services.Configure<IdentityOptions>
        (
            options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            }
        );
    }

    public AuthenticationBuilder AddAuthentication()
    {
        return _services.AddAuthentication
        (
            options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }
        );
    }
    
    public void AddJwtBearer(AuthenticationBuilder authenticationBuilder, ConfigurationManager configurationManager)
    {
        authenticationBuilder.AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configurationManager["JwtOptions:Issuer"],
                ValidAudience = configurationManager["JwtOptions:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configurationManager["JwtOptions:Key"] ?? string.Empty)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });
    }

    public void ConfigureJwtOptions(ConfigurationManager configurationManager)
    {
        _services.Configure<JwtOptions>(configurationManager.GetSection(nameof(JwtOptions)));
    }
}
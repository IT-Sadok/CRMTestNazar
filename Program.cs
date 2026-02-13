using CRM.DependencyInjection;
using CRM.EndPoints;
using Microsoft.AspNetCore.Authentication;


var builder = WebApplication.CreateBuilder(args);

DependencyInjectionService dependencyInjectionService = new (builder.Services);

dependencyInjectionService.AddDbContext(builder.Configuration);
dependencyInjectionService.AddEndpointsApi();
dependencyInjectionService.AddSwagger();
dependencyInjectionService.AddHttpContext();
dependencyInjectionService.AddJwtService();
dependencyInjectionService.AddAccountServices();
dependencyInjectionService.AddEntityFrameworkStores(dependencyInjectionService.AddIdentity());
dependencyInjectionService.AddAuthorization();
dependencyInjectionService.ConfigurePasswordSettings();
AuthenticationBuilder authenticationBuilder = dependencyInjectionService.AddAuthentication();
dependencyInjectionService.AddJwtBearer(authenticationBuilder, builder.Configuration);
dependencyInjectionService.ConfigureJwtOptions(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

new SignUpEndPointService().SignUp(app);
new LogInEndPointService().LogIn(app);

app.Run();
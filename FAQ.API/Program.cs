#region Usings
using FAQ.API.Startup;
using FAQ.DAL.Seeders;
using FAQ.SECURITY.ApplicationAuthorizationService.ServiceImplementation;
#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectServices(builder.Configuration);

var app = builder.Build();

await AfterAppBuildExtesion.Extension(app, builder.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
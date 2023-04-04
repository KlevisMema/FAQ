#region Usings
using FAQ.API.Startup;
using FAQ.DAL.Seeders;
#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectServices(builder.Configuration);

var app = builder.Build();

await AppBuildExtesion.CallSeedersAsync(app, builder.Configuration);

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
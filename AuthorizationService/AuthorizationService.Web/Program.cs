using AuthorizationService.Persistence;
using AuthorizationService.Web.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddApiAuthentification(builder.Configuration);
builder.Services.AddDependencyInjection();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AuthorizationServiceDbContext>();
    db.Database.Migrate();
}

app.UseCors();
app.UseGlobalExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapGroup("/api/v1").MapControllers();
app.Run();

using Application.Middlewares.ExceptionHandlerMiddleware;
using BuyIt.Infrastructure.Services.Extensions;
using BuyIt.Presentation.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRequiredApplicationServiceCollection();
builder.Services.AddRequiredExternalServiceCollection();
builder.Services.AddRequiredPersistenceServiceCollection(builder.Configuration);
builder.Services.AddRequiredIdentityServiceCollection(builder.Configuration);
builder.Services.AddRequiredSwaggerServiceCollection();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseRequiredSwaggerServiceCollection();
}

app.UseStaticFiles();

app.UseCors("ApplicationCorsPolicy");

app.MapControllers();

await using var scope =  app.Services.CreateAsyncScope();

var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
}
catch (OperationCanceledException e)
{
    logger.LogError(e, "An error occured during migration!");
}

app.Run();
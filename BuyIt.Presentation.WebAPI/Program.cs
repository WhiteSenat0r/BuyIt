using Application.Middlewares.ExceptionHandlerMiddleware;
using BuyIt.Presentation.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRequiredApplicationServiceCollection(builder.Configuration);
builder.Services.AddRequiredIdentityServiceCollection(builder.Configuration);
builder.Services.AddRequiredSwaggerServiceCollection(builder.Configuration);

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseRequiredSwaggerServiceCollection();
}

app.UseStaticFiles();

app.UseCors("ApplicationCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

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
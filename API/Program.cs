using System.Text.Json.Serialization;
using API.Middlewares.ExceptionHandlerMiddleware;
using API.Responses;
using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.Factories.ProductRelated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString
        ("DefaultConnection"));
});

builder.Services.AddScoped<IRepository<Product>>(provider =>
    new ProductRepositoryFactory().Create(
        provider.GetService<StoreContext>()!));

builder.Services.AddScoped<IRepository<ProductType>>(provider =>
    new ProductTypeRepositoryFactory().Create(
        provider.GetService<StoreContext>()!));

builder.Services.AddScoped<IRepository<ProductManufacturer>>(provider =>
    new ProductManufacturerRepositoryFactory().Create(
        provider.GetService<StoreContext>()!));

builder.Services.AddScoped<IRepository<ProductRating>>(provider =>
    new ProductRatingRepositoryFactory().Create(
        provider.GetService<StoreContext>()!));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(e => e.Value.Errors)
            .Select(e => e.ErrorMessage).ToList();

        var errorResponse = new ApiValidationErrorResponse(null)
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
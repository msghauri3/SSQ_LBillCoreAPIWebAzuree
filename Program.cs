using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebBilling_Lahore_ReactCore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger configuration
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Web Billing API",
        Version = "v1",
        Description = "API for fetching and generating Bahria Town bills"
    });
});

// Database connection
builder.Services.AddDbContext<SSQReactCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BTLBillingDB")));

// CORS - Allow all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");

// Enable Swagger in ALL environments
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web Billing API v1");
    options.RoutePrefix = ""; // Swagger root par open hoga
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
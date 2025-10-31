using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using WebBilling_Lahore_ReactCore.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Web Billing API",
        Version = "v1",
        Description = "API for fetching and generating Bahria Town bills"
    });
});

builder.Services.AddDbContext<SSQReactCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BTLBillingDB")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:3000",                          // Local development
                "https://softwaredemo.space"                      // Production
                )
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();

app.UseCors("AllowReactApp");

// ✅ Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web Billing API v1");
        c.RoutePrefix = string.Empty; // 👈 This makes Swagger UI open on https://localhost:7108/
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

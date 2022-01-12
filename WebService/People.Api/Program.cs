using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using People.Architecture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddArchitectureServiceRegistration(builder.Configuration);

builder.Services.AddControllers(options => {
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("ready")
    });
    endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("self")
    });
    endpoints.MapControllers();
});

// migrate database
app.MigrateDatabase();

app.Run();

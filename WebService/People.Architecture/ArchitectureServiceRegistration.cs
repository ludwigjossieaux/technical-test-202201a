using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using People.Architecture.Application.Behaviours;
using People.Architecture.Application.Contracts;
using People.Architecture.Infrastructure.Persistence;
using People.Architecture.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture
{
    [ExcludeFromCodeCoverage]
    public static class ArchitectureServiceRegistration
    {
        public static IServiceCollection AddArchitectureServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            // Application

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            // Infrastructure

            services.AddDbContext<PeopleDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IPersonRepository, PersonRepository>();

            // Health

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlite(
                    configuration.GetConnectionString("DefaultConnection"),
                    tags: new[] { "ready" },
                    timeout: TimeSpan.FromSeconds(30));

            return services;
        }

        public static void MigrateDatabase(this IHost host)
        {
            using(var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<PeopleDbContext>();
                db.Database.Migrate();
            }
        }
    }
}

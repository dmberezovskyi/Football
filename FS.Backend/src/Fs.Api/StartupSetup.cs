using System;
using System.Text.Json.Serialization;
using Fs.Api.Infrastructure;
using Fs.Api.Infrastructure.ModelBinding;
using Fs.BackgroundTasks;
using Fs.Infrastructure.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Fs.Api;

public class StartupSetup
{
    private readonly AppConfiguration _configuration;

    public StartupSetup(IConfiguration configuration)
    {
        _configuration = new AppConfiguration(configuration);
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews(options =>
            {
                options.ValueProviderFactories.Insert(0, new SeparatedQueryStringValueProviderFactory(","));
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Football API", Version = "v1" });
        });

        services
            .AddFilters()
            .AddApplication(_configuration.Origin)
            .AddInfrastructure(_configuration.DatabaseConnectionString)
            .AddABackgroundTasks();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
    {

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            dbContext.Database.MigrateAsync().Wait();
            
        }

        app
            .UseRequestCorrections(new Uri($"{_configuration.Origin}/"))
            .UseCacheControl()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseIdentityServer()
            .UseSwagger()
            .UseSwaggerUI()
            .UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
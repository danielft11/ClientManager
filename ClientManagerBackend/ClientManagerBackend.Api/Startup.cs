using ClientManagerBackend.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Linq;

namespace ClientManagerBackend.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var elasticUri = Configuration["ElasticConfiguration:Uri"];

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true,
                })
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "CorsPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "http://localhost:5173")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            #region Injeções de dependência
            services.AdicionarContextoDB();

            services.AdicionarServicos();

            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClientManagerBackend.Api", Version = "v1" });
                c.ResolveConflictingActions(x => x.First());
            });
            #endregion

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientManagerBackend.Api v1"));
            }

            loggerFactory.AddSerilog();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(policyName: "CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}

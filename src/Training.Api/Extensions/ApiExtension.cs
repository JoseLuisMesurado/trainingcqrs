﻿using FluentValidation;
using HealthChecks.UI.Client;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using System.Reflection;
using System.Runtime.CompilerServices;
using Training.API.Behaviors;
using Training.API.ProblemDetailsConfig;
using Training.NG.HttpResponse;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiExtension
    {
        public static IServiceCollection AddHealthchecksConfig(this IServiceCollection services)
        {
            services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy());

            return services;
        }
        public static IServiceCollection AddApiVersionConfig(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });
            services.AddVersionedApiExplorer(
              options =>
              {
                  options.GroupNameFormat = "'v'VVV";
                  options.SubstituteApiVersionInUrl = true;
              });
            return services;
        }

        public static IApplicationBuilder AddHealthChecksConfig(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/healthcheck", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecks("/ready", new HealthCheckOptions
            {
                Predicate = registration => registration.Tags.Contains("dependencies"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }

        public static IServiceCollection FluentValidationsModelsConfiguration(this IServiceCollection services, Assembly assembly)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(assembly);
            return services;
        }

        public static IServiceCollection ProblemDetailsConfiguration(this IServiceCollection services)
        {
            services.AddProblemDetails(delegate (ProblemDetailsOptions setup)
            {
                setup.Map(delegate (HttpContext ctx, ModelValidationException ex)
                {
                    DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new(19, 2);
                    defaultInterpolatedStringHandler.AppendLiteral("EndPoint: ");
                    defaultInterpolatedStringHandler.AppendFormatted(ctx.Request.Path);
                    defaultInterpolatedStringHandler.AppendLiteral(" . Error:");
                    defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
                    
                    return new AppProblemDetails
                    {
                        Type = ex.Type,
                        Title = ex.Title,
                        Detail = ex.Detail,
                        Status = StatusCodes.Status400BadRequest,
                        Instance = (string)ctx.Request.Path,
                        ErrorsMessages = ex.ErrorsMessages
                    };

                });
                setup.Map(delegate (HttpContext ctx, InternalErrorException ex)
                {
                    DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new(19, 2);
                    defaultInterpolatedStringHandler.AppendLiteral("EndPoint: ");
                    defaultInterpolatedStringHandler.AppendFormatted(ctx.Request.Path);
                    defaultInterpolatedStringHandler.AppendLiteral(" . Error:");
                    defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
                    var toReturn =new ProblemDetails
                    {
                        Title = ex.Title,
                        Status = ex.Status,
                        Type = ex.Type,
                        Instance = ex.Instance,
                        Detail = ex.Message,
                    };
                    return toReturn;
                });
                setup.Map(delegate (HttpContext ctx, NotFoundEntityException ex){

                    DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new(19, 2);
                    defaultInterpolatedStringHandler.AppendLiteral("EndPoint: ");
                    defaultInterpolatedStringHandler.AppendFormatted(ctx.Request.Path);
                    defaultInterpolatedStringHandler.AppendLiteral(" . Error:");
                    defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
                    
                    return new ProblemDetails
                    {
                        Title = ex.Title,
                        Status = StatusCodes.Status404NotFound,
                        Type = ex.Type,
                        Instance = (string)ctx.Request.Path,
                        Detail =  ex.Detail
                    };
                });

            });
            return services;
        }
        public static IApplicationBuilder SetAppConfiguration(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            app.UseProblemDetails();
            // if (app.Environment.IsDevelopment())
            // {
                app.UseSwagger();
                app.UseSwaggerUI();
            //}
            app.UseSerilogRequestLogging();
            app.UseCors("AllowAll");
            //app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.AddHealthChecksConfig();
            return app;
        }
    }
}

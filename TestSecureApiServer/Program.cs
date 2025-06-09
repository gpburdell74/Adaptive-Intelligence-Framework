using Adaptive.Intelligence.SecureApi.Services;
using Adaptive.SecureApi.Server;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(
    typeof(ISessionRepositoryService), new SesssionRepositoryService()));


builder.Services.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(
    typeof(ApplicationTokenRepositoryService), new ApplicationTokenRepositoryService()));

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
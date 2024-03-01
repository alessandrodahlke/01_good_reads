using GoodReads.Reviews.API.Configuration;
using GoodReads.Reviews.Application;
using GoodReads.Reviews.Infra;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfig()
                .AddApplication()
                .AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseApiConfig(app.Environment);

app.UseSerilogRequestLogging();

app.Run();
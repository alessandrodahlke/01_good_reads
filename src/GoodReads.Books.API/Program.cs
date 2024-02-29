using GoodReads.Books.API.Configuration;
using GoodReads.Books.Application;
using GoodReads.Books.Infra;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfig()
                .AddApplication()
                .AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(typeof(Program));

builder.Host.UseSerilog((context, configuration) => 
        configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseApiConfig(app.Environment);

app.UseSerilogRequestLogging();

app.Run();

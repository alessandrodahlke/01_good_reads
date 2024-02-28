using GoodReads.Books.API.Configuration;
using GoodReads.Books.Application;
using GoodReads.Books.Infra;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfig()
                .AddApplication()
                .AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();

app.UseApiConfig(app.Environment);

app.Run();

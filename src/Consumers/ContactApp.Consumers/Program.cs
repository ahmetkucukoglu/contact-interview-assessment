using ContactApp.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConsumers(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
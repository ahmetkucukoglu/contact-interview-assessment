using ContactApp.OutboxProcessor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProcessor(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
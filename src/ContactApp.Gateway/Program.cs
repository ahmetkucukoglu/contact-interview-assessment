using ContactApp.Gateway;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGatewayApi(builder.Configuration);

var app = builder.Build();

app.UseGatewayApi();
app.Run();
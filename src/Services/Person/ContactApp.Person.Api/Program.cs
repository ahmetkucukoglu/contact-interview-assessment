using ContactApp.Person.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersonApi(builder.Configuration);

var app = builder.Build();

app.UsePersonApi();
app.Run();
using ContactApp.Company.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCompanyApi(builder.Configuration);

var app = builder.Build();

app.UseCompanyApi();
app.Run();
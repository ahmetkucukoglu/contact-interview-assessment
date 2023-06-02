using ContactApp.Report.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReportApi(builder.Configuration);

var app = builder.Build();

app.UseReportApi();
app.Run();
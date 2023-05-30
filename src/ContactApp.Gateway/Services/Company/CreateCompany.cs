namespace ContactApp.Gateway.Services.Company;

public record CreateCompany(string Name);
public record CreateCompanyResponse(Guid Id, string Name);
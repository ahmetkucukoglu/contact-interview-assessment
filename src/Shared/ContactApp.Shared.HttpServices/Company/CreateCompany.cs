namespace ContactApp.Shared.HttpServices.Company;

public record CreateCompany(string Name);
public record CreateCompanyResponse(Guid Id, string Name);
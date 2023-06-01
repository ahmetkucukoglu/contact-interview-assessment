namespace ContactApp.Shared.HttpServices.Company;

public record GetCompany(Guid Id);
public record GetCompanyResponse(Guid Id, string Name);
namespace ContactApp.Gateway.Services.Company;

public record GetCompanies;
public record GetCompaniesResponse(IEnumerable<GetCompaniesData> Companies);
public record GetCompaniesData(Guid Id, string Name);

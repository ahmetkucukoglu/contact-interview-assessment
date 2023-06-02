using ContactApp.Company.Application.Queries.GetCompany;
using MediatR;

namespace ContactApp.Company.Application.Queries.GetCompanies;

public record GetCompanies : IRequest<GetCompaniesResponse>;
public record GetCompaniesResponse(IEnumerable<GetCompaniesData> Companies);
public record GetCompaniesData(Guid Id, string Name);

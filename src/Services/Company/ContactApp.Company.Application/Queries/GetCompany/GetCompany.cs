using MediatR;

namespace ContactApp.Company.Application.Queries.GetCompany;

public record GetCompany(Guid Id) : IRequest<GetCompanyResponse>;
public record GetCompanyResponse(Guid Id, string Name);
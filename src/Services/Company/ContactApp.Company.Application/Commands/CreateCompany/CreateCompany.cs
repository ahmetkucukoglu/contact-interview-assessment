using MediatR;

namespace ContactApp.Company.Application.Commands.CreateCompany;

public record CreateCompany(string Name) : IRequest<CreateCompanyResponse>;
public record CreateCompanyResponse(Guid Id, string Name);
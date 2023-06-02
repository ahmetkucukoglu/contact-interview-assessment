using ContactApp.Company.Domain.Aggregates;

namespace ContactApp.Company.Domain.Repositories;

public interface ICompanyRepository
{
    public Task Create(Aggregates.Company company, CancellationToken cancellationToken);
    public Task<Aggregates.Company> Get(CompanyId id, CancellationToken cancellationToken);
    public Task<List<Aggregates.Company>> GetAll(CancellationToken cancellationToken);
}
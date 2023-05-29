namespace ContactApp.Company.Domain.Repositories;

public interface ICompanyRepository
{
    public Task Create(Aggregates.Company company, CancellationToken cancellationToken);
}
using ContactApp.Person.Infrastructure.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ContactApp.Person.Infrastructure.Repositories;

public record GetReport(List<GetReportData> Data);
public record GetReportData(string Location, int TotalPerson, int TotalPhoneNumber);

public class LocationReportRepository
{
    private readonly IMongoCollection<Domain.Aggregates.Person> _collection;

    public LocationReportRepository(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

        _collection = database.GetCollection<Domain.Aggregates.Person>("persons");
    }

    public async Task<GetReport> GetReport(CancellationToken cancellationToken)
    {
        PipelineDefinition<Domain.Aggregates.Person, GetReportData> pipeline = new[]
        {
            new BsonDocument("$match",
                new BsonDocument("IsDeleted", false)),
            new BsonDocument("$unwind",
                new BsonDocument
                {
                    {"path", "$Contacts"},
                    {"preserveNullAndEmptyArrays", true}
                }),
            new BsonDocument("$match",
                new BsonDocument("Contacts.Type", 3)),
            new BsonDocument("$group",
                new BsonDocument
                {
                    {"_id", "$Contacts.Value"},
                    {
                        "totalPerson",
                        new BsonDocument("$count",
                            new BsonDocument())
                    }
                }),
            new BsonDocument("$lookup",
                new BsonDocument
                {
                    {"from", "persons"},
                    {"localField", "_id"},
                    {"foreignField", "Contacts.Value"},
                    {"as", "persons"}
                }),
            new BsonDocument("$unwind",
                new BsonDocument
                {
                    {"path", "$persons"},
                    {"preserveNullAndEmptyArrays", true}
                }),
            new BsonDocument("$unwind",
                new BsonDocument
                {
                    {"path", "$persons.Contacts"},
                    {"preserveNullAndEmptyArrays", true}
                }),
            new BsonDocument("$match",
                new BsonDocument("persons.Contacts.Type", 2)),
            new BsonDocument("$group",
                new BsonDocument
                {
                    {
                        "_id",
                        new BsonDocument
                        {
                            {"Location", "$_id"},
                            {"TotalPerson", "$totalPerson"}
                        }
                    },
                    {
                        "TotalPhoneNumber",
                        new BsonDocument("$count",
                            new BsonDocument())
                    }
                }),
            new BsonDocument("$addFields",
                new BsonDocument
                {
                    {"TotalPerson", "$_id.TotalPerson"},
                    {"Location", "$_id.Location"}
                }),
            new BsonDocument("$project",
                new BsonDocument("_id", false))
        };

        var cursor = await _collection.AggregateAsync(pipeline, cancellationToken: cancellationToken);
        var result = await cursor.ToListAsync(cancellationToken);

        return new GetReport(result);
    }
}
using Refit;

namespace ContactApp.Gateway.Services.Person;

public interface IPersonApi
{
    [Post("/persons")]
    Task<CreatePersonResponse> Create([Body]CreatePerson request);
    
    [Get("/persons/{id}")]
    Task<GetPersonResponse> Get(Guid id);
    
    [Get("/persons")]
    Task<GetPersonsResponse> GetAll(int page, int size);
    
    [Delete("/persons/{id}")]
    Task<DeletePersonResponse> Delete(Guid id);
    
    [Put("/persons/{id}/contacts")]
    Task<AddContactResponse> AddContact(Guid id, [Body]AddContact request);
    
    [Delete("/persons/{personId}/contacts/{id}")]
    Task<RemoveContactResponse> RemoveContact(Guid personId, Guid id);
}
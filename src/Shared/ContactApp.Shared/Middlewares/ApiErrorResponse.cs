namespace ContactApp.Shared.Middlewares;

public record ApiErrorResponse
{ 
    public List<string> Errors { get; set; } = new();
}
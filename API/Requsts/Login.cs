namespace API.Requsts;

public record Login()
{
    public required string Name { get; init; }
    public required string Password { get; init; }
}
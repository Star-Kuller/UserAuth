namespace API.Requsts;

public record Registration()
{
    public required string Name { get; init; }
    public string Surname { get; init; }
    public string Number { get; init; }
    public required string Password { get; init; }
}
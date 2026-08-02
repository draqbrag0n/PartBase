namespace PartBase.Domain.Entities;

public class Manufacturer
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string? Website { get; private set; }

    private Manufacturer() { }

    public Manufacturer(string name, string? website)
    {
        Id = Guid.NewGuid();
        Name = name;
        Website = website;
    }
}
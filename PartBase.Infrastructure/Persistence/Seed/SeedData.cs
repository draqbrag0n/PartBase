using PartBase.Domain.Entities;

namespace PartBase.Infrastructure.Persistence.Seed;

public static class SeedData
{
    public static async Task InitializeAsync(PartBaseDbContext context)
    {
        if (context.Manufacturers.Any())
            return;

        var texas = new Manufacturer("Texas Instruments", "https://www.ti.com");
        var st = new Manufacturer("STMicroelectronics", "https://www.st.com");
        var onSemi = new Manufacturer("onsemi", "https://www.onsemi.com");

        var opAmp = new Category("Operational Amplifier");
        var timer = new Category("Timer IC");
        var transistor = new Category("Transistor");

        context.Manufacturers.AddRange(texas, st, onSemi);
        context.Categories.AddRange(opAmp, timer, transistor);

        await context.SaveChangesAsync();

        var components = new[]
        {
            new Component(
                "LM358",
                "Dual Operational Amplifier",
                texas.Id,
                opAmp.Id,
                "DIP-8",
                "https://www.ti.com/lit/ds/symlink/lm358.pdf"),

            new Component(
                "NE555",
                "Timer IC",
                st.Id,
                timer.Id,
                "DIP-8",
                "https://www.st.com"),

            new Component(
                "BC547",
                "NPN Transistor",
                onSemi.Id,
                transistor.Id,
                "TO-92",
                "https://www.onsemi.com")
        };

        context.Components.AddRange(components);

        await context.SaveChangesAsync();
    }
}
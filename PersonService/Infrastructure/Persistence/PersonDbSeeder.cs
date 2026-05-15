using Microsoft.EntityFrameworkCore;
using PersonService.Api.Domain;

namespace PersonService.Api.Infrastructure.Persistence;

public static class PersonDbSeeder
{
    public static async Task SeedAsync(PersonDbContext context, CancellationToken ct)
    {
        if (await context.Persons.AnyAsync(ct))
            return;

        var firstNames = new[]
        {
            "Ali", "Reza", "Hossein", "Mehdi", "Amir",
            "Saeed", "Mohammad", "Hassan", "Arman", "Navid"
        };

        var lastNames = new[]
        {
            "Ahmadi", "Hosseini", "Karimi", "Moradi", "Rahimi",
            "Mohammadi", "Jafari", "Kazemi", "Ebrahimi", "Soleimani"
        };

        var random = new Random();

        var persons = new List<Person>();

        for (var i = 0; i < 20; i++)
        {
            var firstName = firstNames[random.Next(firstNames.Length)];
            var lastName = lastNames[random.Next(lastNames.Length)];

            var nationalCode = GenerateNationalCode(random);

            persons.Add(new Person(
                nationalCode,
                firstName,
                lastName));
        }

        await context.Persons.AddRangeAsync(persons, cancellationToken: ct);
        await context.SaveChangesAsync(cancellationToken: ct);
    }

    private static string GenerateNationalCode(Random random)
    {
        var digits = new int[10];

        for (var i = 0; i < 9; i++)
            digits[i] = random.Next(0, 9);

        var sum = 0;

        for (var i = 0; i < 9; i++)
            sum += digits[i] * (10 - i);

        var remainder = sum % 11;

        digits[9] = remainder < 2
            ? remainder
            : 11 - remainder;

        return string.Concat(digits);
    }
}
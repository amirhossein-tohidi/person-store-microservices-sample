namespace PersonService.Api.Domain;

public class Person
{
    public long Id { get; private set; }

    public string NationalCode { get; private set; } = default!;

    public string FirstName { get; private set; } = default!;

    public string LastName { get; private set; } = default!;

    private Person() { }

    public Person(string nationalCode, string firstName, string lastName)
    {
        NationalCode = nationalCode;
        FirstName = firstName;
        LastName = lastName;
    }

    public void ChangeName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
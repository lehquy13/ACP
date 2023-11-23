using ACP.Domain.Common.Primitives;

namespace ACP.Domain.Entities;

public class Address : ValueObject
{
    public string City { get; } = string.Empty;

    public string Country { get; } = string.Empty;

    public Address()
    {
    }

    public Address(string city, string country)
    {
        City = city;
        Country = country;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return Country;
    }
}
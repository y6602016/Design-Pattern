
namespace PatternDemoCore
{
  public class Person
  {
    public string StreetAddress, Postcode, City;
    public string CompanyName, Position;
    public int AnnualIncome;

    public override string ToString()
    {
      return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
    }
  }

  public class PersonBuilder // facade
  {
    // reference!
    protected Person person = new Person();

    // expose PersonJobBuilder here
    public PersonJobBuilder works => new PersonJobBuilder(person);

    // expose PersonAddressBuilder here
    public PersonAddressBuilder lives => new PersonAddressBuilder(person);

    public static implicit operator Person(PersonBuilder pb)
    {
      return pb.person;
    }
  }

  public class PersonAddressBuilder : PersonBuilder
  {
    public PersonAddressBuilder(Person person)
    {
      this.person = person;
    }

    public PersonAddressBuilder At(string streetAddress)
    {
      person.StreetAddress = streetAddress;
      return this;
    }

    public PersonAddressBuilder WithPostcode(string postcode)
    {
      person.Postcode = postcode;
      return this;
    }

    public PersonAddressBuilder InCity(string city)
    {
      person.City = city;
      return this;
    }
  }

  public class PersonJobBuilder : PersonBuilder
  {
    public PersonJobBuilder(Person person)
    {
      this.person = person;
    }

    public PersonJobBuilder At(string companyName)
    {
      person.CompanyName = companyName;
      return this;
    }

    public PersonJobBuilder AsA(string position)
    {
      person.Position = position;
      return this;
    }

    public PersonJobBuilder Earning(int amount)
    {
      person.AnnualIncome = amount;
      return this;
    }
  }

  static class Program
  {
    static void Main(string[] args)
    {
      var pb = new PersonBuilder();
      Person person = pb
        .works.At("MLP")
              .AsA("SDE")
              .Earning(500000)
        .lives.At("street 1")
              .WithPostcode("123")
              .InCity("NJ");

      Console.WriteLine(person);
    }
  }
}
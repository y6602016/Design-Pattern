namespace FluentBuilder
{
  // Fluent Builder with Recursive Generics:
  // When child fluent builder calls parent fluent builder, the fluent function return this,
  // so now it return "parent builder", which leads to problems. So we should use Recursive
  // generics to design it. We can use abstract class.
  public class Person
  {
    public string Name;
    public string Position;

    // create an inner class in Person and it inherented from PersonJobBuilder<Builder> 
    public class Builder : PersonJobBuilder<Builder>
    {
    }

    // a static method can be called from outside to build a builder
    public static Builder New => new Builder();

    public override string ToString()
    {
      return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
  }

  public abstract class PersonBuilder
  {
    protected Person person = new Person();
    public Person Build()
    {
      return person;
    }
  }

  // class Foo : Bar<Foo>
  public class PersonInfoBuilder<SELF>
    : PersonBuilder
    where SELF : PersonInfoBuilder<SELF> // constraint SELF should be inherented from PersonInfoBuilder<SELF>
  {
    public SELF Called(string name)
    {
      person.Name = name;
      return (SELF)this;
    }
  }

  // now we need to add new funcitonality but based on open-closed principle
  // we don't want to modify the existing builder, so we can create a new builder
  // and it inherent the existing builder
  // But the problem is when the child builder calls parent function, it return parent
  // builder, then the builder becomes parent builder, which is wrong
  // solve it with recursive generics
  public class PersonJobBuilder<SELF>
    : PersonInfoBuilder<PersonJobBuilder<SELF>>
    where SELF : PersonJobBuilder<SELF>
  {
    public SELF WorkAs(string position)
    {
      person.Position = position;
      return (SELF)this;
    }
  }

  internal class Program
  {
    static void Main(string[] args)
    {
      // var builder = new PersonJobBuilder();
      // builder.Called("Mike");
      // .WorkAs() can't be called since when we call "Called()", it returns
      // PersonInfoBuilder, and PersonInfoBuilder doesn't have .WorkAs() function

      var me = Person.New
        .Called("Mike")
        .WorkAs("SDE")
        .Build();

      System.Console.WriteLine(me);
    }
  }
}

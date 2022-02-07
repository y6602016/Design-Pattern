namespace DependencyInversion
{
  // Dependency Inversion:
  // High-level module should NOT depends on low-level module, use Abstractions
  // If the high level module wanna access the data of the low level module,
  // we make the high level module depends on "abstraction interface" instead of directly
  // depending on the low level module. Such that the low level module can change it's
  // inner implementation withot affecting the higher module.

  public enum Relationship
  {
    Parent, Child, Sibling
  }

  public class Person
  {
    public string name;
    // public string age;.....
  }

  // create an Interface for low level to use, no matter what data structure the low
  // level module uses, high level can use the interface to access the data
  public interface IRelationshipBrowser
  {
    IEnumerable<Person> FindAllChildrenOf(string name);
  }

  // low level
  public class Relationships : IRelationshipBrowser
  {
    private List<(Person, Relationship, Person)> relations
      = new List<(Person, Relationship, Person)>();

    public void AddParentChild(Person parent, Person child)
    {
      relations.Add((parent, Relationship.Parent, child));
      relations.Add((child, Relationship.Child, parent));
    }

    // better way: implement interface 
    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
      foreach (var r in relations.Where(
        x => x.Item1.name == name && x.Item2 == Relationship.Parent
      ))
      {
        yield return r.Item3;
      }
    }

    // Bad way: expose the private fields via a public method for higher level to access
    // But what if the low level module want to change it's data structure storing data?
    // The high level module alreay depends on this method so low level can't change the ds.
    // public List<(Person, Relationship, Person)> Relations => relations;
  }

  public class Relationships2 : IRelationshipBrowser
  {
    private Dictionary<Person, Person> relations
      = new Dictionary<Person, Person>();

    public void AddParentChild(Person parent, Person child)
    {
      relations.Add(parent, child);
    }
    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
      foreach (var item in relations.Where(
        x => x.Key.name == name
      ))
      {
        yield return item.Value;
      }
    }
  }

  public class Research // high level module
  {
    // How to find John's child in Research class?
    // if we want to access low level via high level, we may allow high level
    // to access some of the internals(public fields) of the low level modules
    // ===
    // High level module doesn't depend on the low level module, which is Relationships class
    // It depends on "abstraction", which is IRelationshipBrowser
    // ===


    // Bad way: depend on low level 
    // public Research(Relationships relationships)
    // {
    //   // we can use this way to access the low level data, but it only works when we use "Tuple"
    //   // in low level module. What if we change data structure to dictionary or array in low level?
    //   // The code here(high level module) can't work anymore!
    //   // A better way is create an interface then apply it on the low level module
    //   var relations = relationships.Relations;
    //   foreach (var r in relations.Where(
    //     x => x.Item1.name == "John" && x.Item2 == Relationship.Parent
    //   ))
    //   {
    //     System.Console.WriteLine($"Jogn has a child called {r.Item3.name}");
    //   }
    // }

    // Better way: depend on abstraction
    public Research(IRelationshipBrowser browser)
    {
      foreach (var p in browser.FindAllChildrenOf("John"))
      {
        System.Console.WriteLine($"John has a child called {p.name}");
      }
    }

    static void Main(string[] args)
    {
      var parent = new Person { name = "John" };
      var child1 = new Person { name = "Mike" };
      var child2 = new Person { name = "Marry" };

      var relationships = new Relationships();
      relationships.AddParentChild(parent, child1);
      relationships.AddParentChild(parent, child2);

      var child3 = new Person { name = "Tom" };
      var relationships2 = new Relationships2();
      relationships2.AddParentChild(parent, child3);

      // !!! IMPORTANT !!!
      // Even there are multiple low level modules implementing the same interface
      // because the high level module depends on the interface, so it will use the function of
      // the class that is passed in. relationships and relationships2 uses different ds
      // but the high level module doesn't care about it since it depends the interface
      new Research(relationships);
    }
  }
}
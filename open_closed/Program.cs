namespace OpenClosed
{
  // Open-closed principle:
  // The class should be opened for extension and should be closed for modification
  // In this case, betterFilter, it should be able to extended with various filters criterias without modify the
  // innder methods in the class, we just need to create new specification classes for betterFilter to use
  // The bad filter needs to create multiple similar methods to do various filter, which is bad!
  // How to achieve it? We can inherent Interface!
  public class Program
  {
    //=== Interface ===

    // allow to check whether a product satisfies some requirements
    // this has the same purpose with if (p.Size == size) in original filter method
    public interface ISpecification<T>
    {
      bool IsSatisfied(T t);
    }

    // according the the above specification to filter
    public interface IFilter<T>
    {
      IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    // we can create various specification classes
    public class ColorSpecification : ISpecification<Product>
    {
      private Color color;

      public ColorSpecification(Color color)
      {
        this.color = color;
      }

      public bool IsSatisfied(Product t)
      {
        return t.Color == color;
      }
    }

    public class SizeSpecification : ISpecification<Product>
    {
      private Size size;

      public SizeSpecification(Size size)
      {
        this.size = size;
      }

      public bool IsSatisfied(Product t)
      {
        return t.Size == size;
      }
    }

    // what if we want to filter color + size?
    // we can use combinator to combine them
    public class AndSpecification<T> : ISpecification<T>
    {
      ISpecification<T> first, second;

      // input params are specification objects
      public AndSpecification(ISpecification<T> first, ISpecification<T> second)
      {
        this.first = first;
        this.second = second;
      }

      public bool IsSatisfied(T t)
      {
        return first.IsSatisfied(t) && second.IsSatisfied(t);
      }
    }

    public class BetterFilter : IFilter<Product>
    {
      public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
      {
        foreach (var i in items)
        {
          if (spec.IsSatisfied(i))
          {
            yield return i;
          }
        }
      }
    }


    public enum Color
    {
      Red, Green, Blue
    }

    public enum Size
    {
      Small, Medium, Large, Yuge
    }

    public class Product
    {
      public string Name;
      public Color Color;
      public Size Size;

      public Product(string name, Color color, Size size)
      {
        this.Name = name;
        this.Color = color;
        this.Size = size;
      }
    }

    // bad filter
    public class ProductFilter
    {
      public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
      {
        foreach (var p in products)
        {
          if (p.Size == size)
          {
            yield return p;
          }
        }
      }

      public IEnumerable<Product> FilterByColore(IEnumerable<Product> products, Color color)
      {
        foreach (var p in products)
        {
          if (p.Color == color)
          {
            yield return p;
          }
        }
      }

      // there may be more filters, I don't wannna keep copy-paste..... :(((
    }

    static void Main(string[] args)
    {
      var apple = new Product("Apple", Color.Green, Size.Small);
      var tree = new Product("Tree", Color.Green, Size.Large);
      var house = new Product("House", Color.Blue, Size.Large);

      Product[] products = { apple, tree, house };

      var pf = new ProductFilter();
      System.Console.WriteLine("Green products (old):");
      foreach (var p in pf.FilterByColore(products, Color.Green))
      {
        System.Console.WriteLine($" - {p.Name} is green");
      }


      var bf = new BetterFilter();
      System.Console.WriteLine("Green products (new):");
      foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
      {
        System.Console.WriteLine($" - {p.Name} is green");
      }

      // filter by color and size, we still use bf, we only need to put combinator specification
      System.Console.WriteLine("Large blue items");
      foreach (var p in bf.Filter(
        products,
        new AndSpecification<Product>(
          new ColorSpecification(Color.Blue),
          new SizeSpecification(Size.Large))))
      {
        System.Console.WriteLine($" - {p.Name} is blue large");
      }
    }
  }
}

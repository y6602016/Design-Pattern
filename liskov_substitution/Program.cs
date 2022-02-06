namespace LiskovSubstitution
{
  // liskov substitution:
  // Be able to substitute a base type for a subtype, if we replace the child class with 
  // parent class, the reulst should remain correct. 
  // In this class, we declare square version props in square class, it leads that we can't
  // declare sqaure object with Rectangle class, which means it cann't be substituted by base class
  // So we use virtual and override, it allow bind reference at compile time, which means it uses 
  // object fields instead of class fields.

  // Not use "base normal fields + child new fields"
  // Should use "base virtual fields + child override fields"

  public class Rectangle
  {
    // if we don't declare prop as "virtual", when we use Rectangle to create Square object
    // The params will leads to error since the program use Rectangle rules to create Square

    public virtual int Width { get; set; }
    public virtual int Height { get; set; }
    public Rectangle()
    {
    }

    public Rectangle(int width, int height)
    {
      Width = width;
      Height = height;
    }

    public override string ToString()
    {
      return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
    }

  }

  public class Square : Rectangle
  {
    // Avoid to use new to create square version width and height prop, use override
    public override int Width
    {
      set
      {
        base.Width = base.Height = value;
      }
    }

    public override int Height
    {
      set
      {
        base.Width = base.Height = value;
      }
    }
  }

  public class Program
  {
    // we declare Area as static method since we call it in Main, which is a static method
    static public int Area(Rectangle r) => r.Width * r.Height;
    static void Main(string[] args)
    {
      Rectangle rc = new Rectangle(2, 3);
      System.Console.WriteLine($"{rc} has area {Area(rc)}");

      // If we use new field in child class, although Square is a subclass of Rectangle, 
      // if we declare Rectangle sq = new Square(), it references to class type
      // On the other hand, if we use virtual and override, it references to object type
      Square sq = new Square();
      sq.Width = 4;
      System.Console.WriteLine($"{sq} has area {Area(sq)}");
    }
  }
}

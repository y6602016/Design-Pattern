using System;

namespace PatternDemoCore
{
  public class Point
  {
    private double x, y;

    private Point(double x, double y)
    {
      this.x = x;
      this.y = y;
    }

    public override string ToString()
    {
      return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
    }

    public static Point Origin => new Point(0, 0); // property, return a new object whenever called
    public static Point Origin2 = new Point(0, 0); // field, only one

    // inner Factory class
    public static class Factory
    {
      public static Point NewCartesianPoint(double x, double y)
      {
        return new Point(x, y);
      }

      public static Point NewPolarPoint(double rho, double theta)
      {
        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
      }
    }
  }

  static class Program
  {
    static void Main(string[] args)
    {
      var point = Point.Factory.NewCartesianPoint(1, 2);
      Console.WriteLine(point);
      var point2 = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
      Console.WriteLine(point2);
    }
  }
}

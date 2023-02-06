using System;

namespace PatternDemoCore
{
  public class Point
  {
    // factory method
    public static Point NewCartesianPoint(double x, double y)
    {
      return new Point(x, y);
    }

    public static Point NewPolarPoint(double rho, double theta)
    {
      return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    }

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
  }

  static class Program
  {
    static void Main(string[] args)
    {
      var point = Point.NewCartesianPoint(1, 2);
      Console.WriteLine(point);
      var point2 = Point.NewPolarPoint(1.0, Math.PI / 2);
      Console.WriteLine(point2);
    }
  }
}

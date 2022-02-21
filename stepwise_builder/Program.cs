using System;

namespace StepwiseBuilder
{
  public enum CarType
  {
    Sedan,
    Crossover
  }
  public class Car
  {
    public CarType Type;
    public int WheelSize; // available sizes depneds on the type
  }

  public interface ISpecifyCarType
  {
    // return wheelsize
    ISpecifyWheelSize OfType(CarType type);
  }

  public interface ISpecifyWheelSize
  {
    // return buildCar
    IBuildCar WithWheels(int size);
  }

  public interface IBuildCar
  {
    // return a new car
    public Car Build();
  }

  public class CarBuilder
  {
    // a private class to implement the three interfaces
    private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
    {
      private Car car = new Car();
      public ISpecifyWheelSize OfType(CarType type)
      {
        car.Type = type;
        // Impl implements ISpecifyWheelSize, so we can return Impl
        return this;
      }

      public IBuildCar WithWheels(int size)
      {
        switch (car.Type)
        {
          case CarType.Crossover when size < 17 || size > 20:
          case CarType.Sedan when size < 15 || size > 17:
            throw new ArgumentException($"Wrong size of wheel for {car.Type}.");
        }
        car.WheelSize = size;
        return this;
      }

      public Car Build()
      {
        return car;
      }
    }
    public static ISpecifyCarType Create()
    {
      return new Impl();
    }

  }


  internal class Program
  {
    static void Main(string[] args)
    {
      // use the builder with properties to build the instance
      var car = CarBuilder.Create() // when call create(), get ISpecifyCarType
        .OfType(CarType.Crossover) // when call OfType, get ISpecifyWheelSize
        .WithWheels(18) // when call WithWheels, get IBuildCar
        .Build(); // when call Build, get car
    }
  }
}

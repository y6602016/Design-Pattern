namespace InterfaceSegregation
{
  // Interface Segregation:
  // Don't put too much into an interfce, split it!
  // Interface is too large and if some classes just need some of the fields of the interface?
  // When we design interface, we need to segerate(aka. split) interface as functional as possible
  public class Document
  {

  }

  // If we put all functions into a single interface
  public interface IMachine
  {
    void Print(Document d);
    void Scan(Document d);
    void Fax(Document d);
  }

  public class MultiFunctionPrinter : IMachine
  {
    public void Fax(Document d)
    {
      //
    }

    public void Print(Document d)
    {
      //
    }

    public void Scan(Document d)
    {
      //
    }
  }

  // But now we only need some functions...
  public class OldFasionPrinter : IMachine
  {
    public void Fax(Document d)
    {
      // it can't fax!
    }

    public void Print(Document d)
    {
      // 
    }

    public void Scan(Document d)
    {
      // it can't scan!
    }
  }

  // We can devide the IMachine interface to three interfaces
  public interface IPrinter
  {
    void Print(Document d);
  }

  public interface IScanner
  {
    void Scan(Document d);
  }

  public interface IFaxer
  {
    void Fax(Document d);
  }

  // The photocopier class may just need two functions
  public class Photocopier : IPrinter, IScanner
  {
    public void Print(Document d)
    {
      //
    }

    public void Scan(Document d)
    {
      //
    }
  }

  // or we even can create a higher level interface 
  interface IMultiFunctionDevice : IPrinter, IScanner, IFaxer //....
  { }

  // then we can use it on the class
  public class MultiFunctionMachine : IMultiFunctionDevice
  {
    private IPrinter printer;
    private IScanner scanner;
    private IFaxer faxer;

    public MultiFunctionMachine(IPrinter printer, IScanner scanner, IFaxer faxer)
    {
      this.printer = printer;
      this.scanner = scanner;
      this.faxer = faxer;
    }

    public void Fax(Document d)
    {
      faxer.Fax(d);
    }

    public void Print(Document d)
    {
      printer.Print(d);
    }

    public void Scan(Document d)
    {
      scanner.Scan(d);
    }
  }

  public class Program
  {
    static void Main(string[] args)
    {

    }
  }
}

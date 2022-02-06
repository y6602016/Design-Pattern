using System.Diagnostics;

namespace SingleResponsibility
{
  // single responsibility:
  // Any particular class should have just a single reason to change(has specific job to do)
  // all methods in the class are related to the class
  public class Journal
  {
    private readonly List<string> entries = new List<string>();
    private static int count = 0;
    public int AddEntry(string text)
    {
      entries.Add($"{++count}: {text}");
      return count; // memento
    }

    public void RemoveEntry(int index)
    {
      entries.RemoveAt(index);
    }

    public override string ToString()
    {
      return string.Join(Environment.NewLine, entries);
    }

    // ==
    // save or load are beyond "single reason", they should not be Journal's job
    // assign theses tasks to another class
    // ==
    // public void Save(string filename){
    //   File.WriteAllText(filename, ToString());
    // }

    // public static Journal Load(string filename){

    // }
  }

  public class Persistence
  {
    // assign load or save to file task to this class
    public void SaveToFile(Journal j, string filename, bool overwite = false)
    {
      if (overwite || !File.Exists(filename))
      {
        File.WriteAllText(filename, j.ToString());
      }
    }
  }

  public class Program
  {
    static void Main(string[] args)
    {
      var j = new Journal();
      j.AddEntry("I love it");
      j.AddEntry("I ate a bread");
      System.Console.WriteLine(j);

      // var p = new Persistence();
      // var filename = @"/Users/mike/Desktop";
      // p.SaveToFile(j, filename, true);
      // Process.Start(filename);
    }
  }
}

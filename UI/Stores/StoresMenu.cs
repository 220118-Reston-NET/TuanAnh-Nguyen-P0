namespace UI
{
  public class StoresMenu : IMenu
  {
    public void Display()
    {
      Console.WriteLine("Stores");
      Console.WriteLine("[1] - List all the Stores");
      Console.WriteLine("[2] - Search for a store");
      Console.WriteLine("-----");
      Console.WriteLine("[0] - Go back");
      Console.WriteLine("What would you like to do:");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "MainMenu";
        case "1":
          return "ListStores";
        case "2":
          return "SearchStore";
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "StoresMenu";
      }
    }
  }
}
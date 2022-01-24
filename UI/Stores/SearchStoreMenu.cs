using Model;

namespace UI
{
  public class SearchStoreMenu : IMenu
  {
    private static StoreFront _store = new StoreFront();
    public void Display()
    {
      Console.WriteLine("Enter store information that you want to search:");
      Console.WriteLine("[1] - Name: " + _store.Name);
      Console.WriteLine("[2] - Address: " + _store.Address);
      Console.WriteLine("-----");
      Console.WriteLine("[9] - Search");
      Console.WriteLine("[0] - Go back");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "StoresMenu";
        case "1":
          Console.WriteLine("Please enter store name:");
          _store.Name = Console.ReadLine();
          return "SearchStoreMenu";
        case "2":
          Console.WriteLine("Please enter store address:");
          _store.Address = Console.ReadLine();
          return "SearchStoreMenu";
        //Searching method
        case "9":
          Console.WriteLine("Searching store base on the given information...");
          //Shoule change to Show Information Page later
          return "SearchStoreMenu";
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "SearchStoreMenu";
      }
    }
  }
}
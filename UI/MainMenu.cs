namespace UI
{
  /*
    Mainmenu inherits IMenu interface but since it is a class it needs to give actual implementation details to the methods
    stated inside of the interface
  */
  public class MainMenu : IMenu
  {
    public void Display()
    {
      Console.WriteLine("Welcome to Our Shopping Mall");
      Console.WriteLine("---If you are new, try to start here---");
      Console.WriteLine("[1] - Customers");
      Console.WriteLine("[2] - Stores");
      Console.WriteLine("---If you are already a part of our shopping mall, use options below to start---");
      Console.WriteLine("[3] - Customer Manager");
      Console.WriteLine("[4] - Store Manager");
      // Console.WriteLine("---Shopping Mall Department ONLY---");
      // Console.WriteLine("[9] - Inventory Management");
      Console.WriteLine("-----");
      Console.WriteLine("[0] - Exit");
      Console.WriteLine("What would you like to do?");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      //Switch cases are just useful if you are doing a bunch of comparision
      switch (_userInput)
      {
        case "0":
          return "Exit";
        case "1":
          return "CustomersMenu";
        case "2":
          return "StoresMenu";
        case "3":
          return "CustomerManager";
        case "4":
          return "StoreManager";
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "MainMenu";
      }
    }
  }
}
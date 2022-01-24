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
      Console.WriteLine("What would you like to do?");
      Console.WriteLine("[1] - Customers");
      Console.WriteLine("[2] - Stores");
      Console.WriteLine("-----");
      Console.WriteLine("[0] - Exit");
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
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "MainMenu";
      }
    }
  }
}
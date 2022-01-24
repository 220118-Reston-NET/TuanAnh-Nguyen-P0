namespace UI
{
  public class CustomersMenu : IMenu
  {
    public void Display()
    {
      Console.WriteLine("What would you like to do as a Customer:");
      Console.WriteLine("[1] - Add a new customer");
      Console.WriteLine("[2] - Search for a customer");
      Console.WriteLine("-----");
      Console.WriteLine("[0] - Go back");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "MainMenu";
        case "1":
          return "AddNewCustomer";
        case "2":
          return "SearchCustomer";
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "CustomersMenu";
      }
    }
  }
}
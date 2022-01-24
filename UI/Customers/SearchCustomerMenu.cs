using Model;

namespace UI
{
  public class SearchCustomerMenu : IMenu
  {
    private static Customer _customer = new Customer();
    public void Display()
    {
      Console.WriteLine("Enter customer information that you want to search:");
      Console.WriteLine("[1] - Name: " + _customer.Name);
      Console.WriteLine("[2] - Address: " + _customer.Address);
      Console.WriteLine("[3] - Email: " + _customer.Email);
      Console.WriteLine("[4] - Phone Number: " + _customer.PhoneNumber);
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
          return "CustomersMenu";
        case "1":
          Console.WriteLine("Please enter customer name:");
          _customer.Name = Console.ReadLine();
          return "SearchCustomerMenu";
        case "2":
          Console.WriteLine("Please enter the address:");
          _customer.Address = Console.ReadLine();
          return "SearchCustomerMenu";
        case "3":
          Console.WriteLine("Please enter the email:");
          _customer.Email = Console.ReadLine();
          return "SearchCustomerMenu";
        case "4":
          Console.WriteLine("Please enter the phone number:");
          _customer.PhoneNumber = Console.ReadLine();
          return "SearchCustomerMenu";
        //Searching method
        case "9":
          Console.WriteLine("Searching customer base on the given information...");
          //Shoule change to Show Information Page later
          return "SearchCustomerMenu";
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "SearchCustomerMenu";
      }
    }
  }
}
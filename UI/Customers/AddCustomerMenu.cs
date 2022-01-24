using Model;

namespace UI
{
  public class AddCustomerMenu : IMenu
  {
    private static Customer _newCustomer = new Customer();
    public void Display()
    {
      Console.WriteLine("Enter new customer information");
      Console.WriteLine("[1] - Name: " + _newCustomer.Name);
      Console.WriteLine("[2] - Address: " + _newCustomer.Address);
      Console.WriteLine("[3] - Email: " + _newCustomer.Email);
      Console.WriteLine("[4] - Phone Number: " + _newCustomer.PhoneNumber);
      Console.WriteLine("-----");
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
          _newCustomer.Name = Console.ReadLine();

          // //Check if the input is empty
          // while (_newCustomer.Name == "")
          // {
          //   Console.WriteLine("Name must not be empty!");
          //   Console.WriteLine("Please enter customer name:");
          //   _newCustomer.Name = Console.ReadLine();
          // }
          return "AddNewCustomer";
        case "2":
          Console.WriteLine("Please enter the address:");
          _newCustomer.Address = Console.ReadLine();
          return "AddNewCustomer";
        case "3":
          Console.WriteLine("Please enter the email:");
          _newCustomer.Email = Console.ReadLine();
          return "AddNewCustomer";
        case "4":
          Console.WriteLine("Please enter the phone number:");
          _newCustomer.PhoneNumber = Console.ReadLine();
          return "AddNewCustomer";
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "AddNewCustomer";
      }
    }
  }
}
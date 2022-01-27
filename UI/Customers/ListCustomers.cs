using BL;
using Model;

namespace UI
{
  public class ListCustomersMenu : IMenu
  {
    private ICustomerBL _listCusBL;
    public ListCustomersMenu(ICustomerBL p_listCusBL)
    {
      _listCusBL = p_listCusBL;
    }
    private List<Customer> _listCustomers;
    public static Customer _currentCustomer = new Customer();
    public void DisplayAllCustomers()
    {
      _listCustomers = _listCusBL.GetALlCustomers();
      if (_listCustomers.Count() > 0)
      {
        Console.WriteLine("Here are all customers in shopping mall:");
        for (int i = 0; i < _listCustomers.Count(); i++)
        {
          Console.WriteLine("- " + _listCustomers[i].Name + " (" + _listCustomers[i].Address + ")");
        }
        Console.WriteLine("-----");
        Console.WriteLine("Please enter the customer name that you want to look at. Ex: '" + _listCustomers[0].Name + "'");
      }
      else
      {
        Console.WriteLine("We don't have any customer yet. Please try to sign up to start shopping!");
      }
    }
    public void Display()
    {
      //List of Store
      DisplayAllCustomers();
      Console.WriteLine("[0] - Go back");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      _listCustomers = _listCusBL.GetALlCustomers();

      switch (_userInput)
      {
        case "0":
          return "MainMenu";

        default:
          //Check if the input is existed in Name of the Customer
          for (int i = 0; i < _listCustomers.Count(); i++)
          {
            if (_userInput == _listCustomers[i].Name)
            {
              _currentCustomer = _listCustomers[i];
              Console.WriteLine("Logging in as " + _currentCustomer.Name);
              Log.Information("User just logged in as a Customer: " + _currentCustomer.Name);
              Console.WriteLine("Returning to the main menu...");
              System.Threading.Thread.Sleep(2000);

              return "MainMenu";
            }
          }
          Console.WriteLine("Please input a valid response!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "ListCustomers";
      }
    }
  }
}
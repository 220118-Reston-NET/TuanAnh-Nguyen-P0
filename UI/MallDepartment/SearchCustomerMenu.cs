using BL;
using Model;

namespace UI
{
  public class SearchCustomerMenu : IMenu
  {
    private ICustomerBL _listCusBL;
    public SearchCustomerMenu(ICustomerBL p_listCusBL)
    {
      _listCusBL = p_listCusBL;
    }
    private List<Customer> _listFilteredCus = new List<Customer>();
    public void Display()
    {
      Console.WriteLine("Enter customer information that you want to search by:");
      Console.WriteLine("[1] - Name");
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
          Console.WriteLine("Please enter customer name:");
          string _inputName = Console.ReadLine();
          _listFilteredCus = _listCusBL.SearchCustomersByName(_inputName);

          Log.Information("User just searched for a customer that have name contains: '" + _inputName + "'");
          Console.WriteLine("Here are all customer that have name contains '" + _inputName + "'");
          foreach (var cus in _listFilteredCus)
          {
            Console.WriteLine(cus);
            Console.WriteLine("-----------------------");
          }

          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
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
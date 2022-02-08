using BL;
using Model;

namespace UI
{
  public class SearchProductMenu : IMenu
  {
    private ProductBL _prodBL;
    public SearchProductMenu(ProductBL p_prodBL)
    {
      _prodBL = p_prodBL;
    }
    private List<Products> _listProds = new List<Products>();
    private List<StoreFront> _listAvailableStore = new List<StoreFront>();
    public void Display()
    {
      Console.WriteLine("Enter product information that you want to search by:");
      Console.WriteLine("[1] - Product name");
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
          Console.WriteLine("Please enter product name:");
          string _inputName = Console.ReadLine();
          _listProds = _prodBL.GetAllProductsByProductName(_inputName);

          Log.Information("User just searched for products that have name contains: '" + _inputName + "'");
          Console.WriteLine("Here are all products that have name contains '" + _inputName + "'");
          foreach (var prod in _listProds)
          {
            Console.WriteLine(prod);

            _listAvailableStore = _prodBL.GetAllStoreFrontsByProductID(prod.ProductID);
            if (_listAvailableStore.Count() == 0)
            {
              Console.WriteLine($"All Stores are currently out of stock of {prod.Name}! ");
            }
            else if (_listAvailableStore.Count() == 1)
            {
              Console.WriteLine($"The store have {prod.Name} instock is: ");
              foreach (var item in _listAvailableStore)
              {
                Console.Write($"- {item.Name}\n");
              }
            }
            else
            {
              Console.WriteLine($"All Stores have {prod.Name} instock are: ");
              foreach (var item in _listAvailableStore)
              {
                Console.Write($"- {item.Name}\n");
              }
            }

            Console.WriteLine("-----------------------");
          }

          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "SearchProductMenu";

        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "SearchProductMenu";
      }
    }
  }
}
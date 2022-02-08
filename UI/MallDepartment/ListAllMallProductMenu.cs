using BL;
using Model;

namespace UI
{
  public class ListAllMallProductMenu : IMenu
  {
    private IProductBL _listProdBL;
    private IInventoryBL _listInvenBL;
    public ListAllMallProductMenu(IProductBL p_listProdBL, IInventoryBL p_listInvenBL)
    {
      _listProdBL = p_listProdBL;
      _listInvenBL = p_listInvenBL;
    }
    private static List<Products> _listProducts;
    private static List<Inventory> _listProductOfStore = new List<Inventory>();
    public static string _selectProductID = "";
    public void DisplayAllProducts()
    {
      _listProducts = _listProdBL.GetAllProducts();
      if (_listProducts.Count() > 0)
      {
        Console.WriteLine("Here are all products in the mall:");
        for (int i = 0; i < _listProducts.Count(); i++)
        {
          Console.WriteLine("- " + _listProducts[i].Name + " - $" + _listProducts[i].Price + (_listProducts[i].MinimumAge == 0 ? "" : $" [AR-{_listProducts[i].MinimumAge}]"));
        }
        Console.WriteLine("-----");
        Console.WriteLine("Please enter the product name that you want to modify information. Ex: '" + _listProducts[0].Name + "'");
      }
      else
      {
        Console.WriteLine("Mall Inventory are empty!");
      }
    }

    public void Display()
    {
      DisplayAllProducts();
      Console.WriteLine("[0] - Go back");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "MallMenu";

        default:
          //Check if the input is existed in Name of the Product
          for (int i = 0; i < _listProducts.Count(); i++)
          {
            if (_userInput == _listProducts[i].Name)
            {

              Log.Information("Manager just choose to modify the product: " + _listProducts[i]);
              _selectProductID = _listProducts[i].ProductID;
              return "EditProductMallMenu";
            }
          }

          Console.WriteLine("Please input a valid response!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "ListAllMallProductsMenu";
      }
    }
  }
}
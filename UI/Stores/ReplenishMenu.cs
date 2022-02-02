using BL;
using Model;

namespace UI
{
  public class ReplenishMenu : IMenu
  {
    private IProductBL _listProdBL;
    private IInventoryBL _listInvenBL;
    public ReplenishMenu(IProductBL p_listProdBL, IInventoryBL p_listInvenBL)
    {
      _listProdBL = p_listProdBL;
      _listInvenBL = p_listInvenBL;
    }
    private List<Products> _listProducts;
    private List<Inventory> _listInvens;
    public static string _selectedReplProdID = "";
    public void DisplayAllProducts()
    {
      _listProducts = _listProdBL.GetAllProducts();
      _listInvens = _listInvenBL.GetAllProductsFromStore(ListStoresMenu._currentStoreFront.StoreID);
      string _prodName;
      if (_listProducts.Count() > 0)
      {
        Console.WriteLine("Here are all products in your store:");
        for (int i = 0; i < _listInvens.Count(); i++)
        {
          _prodName = _listProducts.Where(p => p.ProductID == _listInvens[i].ProductID).First().Name;
          Console.WriteLine("- " + _prodName + " (" + _listInvens[i].Quantity + " left)");
        }
        Console.WriteLine("-----");
        Console.WriteLine("Please enter the product name that you want to look at. Ex: '" + _listProducts[0].Name + "'");
      }
      else
      {
        Console.WriteLine("You don't have any products now. Please add a new product!");
      }
    }
    public void Display()
    {
      Console.WriteLine("You are currently manage the  " + ListStoresMenu._currentStoreFront.Name + "'s inventory");
      DisplayAllProducts();
      Console.WriteLine("[0] - Go back");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "InventoryMenu";

        default:
          //Check if the input is existed in Name of the Product
          for (int i = 0; i < _listProducts.Count(); i++)
          {
            if (_userInput == _listProducts[i].Name)
            {

              Log.Information("User just choose to edit the product: " + _listProducts[i]);
              _selectedReplProdID = _listProducts[i].ProductID;
              return "EditProduct";
            }
          }

          Console.WriteLine("Please input a valid response!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "ReplenishInventory";
      }
    }
  }
}
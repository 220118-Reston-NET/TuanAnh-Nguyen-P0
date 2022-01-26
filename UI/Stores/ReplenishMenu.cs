using BL;
using Model;

namespace UI
{
  public class ReplenishMenu : IMenu
  {
    private IProductBL _listProdBL;
    public ReplenishMenu(IProductBL p_listProdBL)
    {
      _listProdBL = p_listProdBL;
    }
    private List<Products> _listProducts;
    public static Products _selectedReplProd = new Products();
    public void DisplayAllProducts()
    {
      _listProducts = _listProdBL.GetAllProductsFromStore(ListStoresMenu._currentStoreFront.StoreID);
      if (_listProducts.Count() > 0)
      {
        Console.WriteLine("Here are all products in your store:");
        for (int i = 0; i < _listProducts.Count(); i++)
        {
          Console.WriteLine("- " + _listProducts[i].Name + " (" + _listProducts[i].Quantity + " left)");
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
              _selectedReplProd = _listProducts[i];
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
using BL;
using Model;

namespace UI
{
  public class ListAllMallProdMenu : IMenu
  {
    private IProductBL _listProdBL;
    private IInventoryBL _listInvenBL;
    public ListAllMallProdMenu(IProductBL p_listProdBL, IInventoryBL p_listInvenBL)
    {
      _listProdBL = p_listProdBL;
      _listInvenBL = p_listInvenBL;
    }
    private static List<Products> _listProducts;
    private static Inventory _newImportProduct;
    private static List<Inventory> _listProductOfStore = new List<Inventory>();
    public void DisplayAllProducts()
    {
      _listProducts = _listProdBL.GetAllProducts();
      if (_listProducts.Count() > 0)
      {
        Console.WriteLine("Here are all products in the mall:");
        for (int i = 0; i < _listProducts.Count(); i++)
        {
          Console.WriteLine("- " + _listProducts[i].Name + " - $" + _listProducts[i].Price);
        }
        Console.WriteLine("-----");
        Console.WriteLine("Please enter the product name that you want to import. Ex: '" + _listProducts[0].Name + "'");
      }
      else
      {
        Console.WriteLine("Mall Inventory are empty!");
      }
    }

    public void Display()
    {
      DisplayAllProducts();
      Console.WriteLine("[0] - Cancel & Go back");
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
              //Check if the quantity store want to import

              Console.WriteLine("How many do you want to buy:");
              string _userInputQuantity = Console.ReadLine();

              while (!_userInputQuantity.All(Char.IsDigit) || _userInputQuantity == "" || Convert.ToInt32(_userInputQuantity) == 0)
              {
                Console.WriteLine("The amount have to be a number, should not be empty and have to be greater than 0!");
                Console.WriteLine("Please enter again:");
                _userInputQuantity = Console.ReadLine();
              }

              _newImportProduct = new Inventory();
              _newImportProduct.ProductID = _listProducts[i].ProductID;
              _newImportProduct.StoreID = ListStoresMenu._currentStoreFront.StoreID;
              _newImportProduct.Quantity = Convert.ToInt32(_userInputQuantity);

              _listProductOfStore = _listInvenBL.GetAllProductsFromStore(ListStoresMenu._currentStoreFront.StoreID);

              if (_listProductOfStore.Any(p => p.ProductID == _listProducts[i].ProductID))
              {
                _newImportProduct = new Inventory();
                Console.WriteLine("You probably already have this product in your store! Please select another product!");
                System.Threading.Thread.Sleep(2000);
                Log.Information("Trying to import the product already have in inventory");
                return "ImportNewProduct";
              }
              else
              {
                _listInvenBL.ImportProduct(_newImportProduct);
                Log.Information("Imported new product succesfully to inventory: " + _newImportProduct);
                Console.WriteLine("Imported new product successfully!");
                Console.WriteLine("Returning back to the previous menu!");
                System.Threading.Thread.Sleep(2000);
                return "InventoryMenu";
              }
            }
          }

          Console.WriteLine("Please input a valid response!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "ImportNewProduct";
      }
    }
  }
}
using BL;
using Model;

namespace UI
{
  public class EditProductMenu : IMenu
  {
    private IInventoryBL _invenBL;
    private IProductBL _prodBL;
    public EditProductMenu(IProductBL p_prod, IInventoryBL p_invenBL)
    {
      _prodBL = p_prod;
      _invenBL = p_invenBL;
    }
    private Inventory _invenProd;
    private Products _prodDetail;
    public void Display()
    {
      _prodDetail = _prodBL.GetProductDetail(ReplenishMenu._selectedReplProdID);
      _invenProd = _invenBL.GetProductDetail(ReplenishMenu._selectedReplProdID, ListStoresMenu._currentStoreFront.StoreID);
      Console.WriteLine("Information of the product that you want to replenish:");
      Console.WriteLine("- Name:        " + _prodDetail.Name);
      Console.WriteLine("- Price:       " + _prodDetail.Price);
      Console.WriteLine("- Description: " + _prodDetail.Desc);
      Console.WriteLine("- Quantity:    " + _invenProd.Quantity);
      Console.WriteLine("-----");
      Console.WriteLine("[9] - Replenish");
      Console.WriteLine("[0] - Go back");
      Console.WriteLine("What do you want to do:");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "InventoryMenu";
        case "9":
          //Check if the quantity is <= 0
          Console.WriteLine("Please enter the amount that you want to replenish:");
          string _userInputQuantity = Console.ReadLine();

          //Check if the input is empty
          while (!_userInputQuantity.All(Char.IsDigit) || _userInputQuantity == "" || Convert.ToInt32(_userInputQuantity) == 0)
          {
            Console.WriteLine("The amount should not be empty, have to be a number and greater than 0!");
            Console.WriteLine("Please enter the amount again:");
            _userInputQuantity = Console.ReadLine();
          }

          //Save product to the database after modified
          Log.Information("Replenish the amount of product to the database: " + _invenProd);
          _invenBL.ReplenishProduct(_invenProd.InventoryID, Convert.ToInt32(_userInputQuantity));
          Log.Information("Replenish the product succesfully!");
          Console.WriteLine("Replenished the product succesfully!");
          Console.WriteLine("Returning back to the previous menu!");
          System.Threading.Thread.Sleep(2000);

          //Clear the information
          _prodDetail = new Products();
          _invenProd = new Inventory();
          return "InventoryMenu";

        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "EditProduct";
      }
    }
  }
}
using BL;
using Model;

namespace UI
{
  public class EditProductMenu : IMenu
  {
    private Products _prod = ReplenishMenu._selectedReplProd;
    private IProductBL _prodBL;
    public EditProductMenu(IProductBL p_prod)
    {
      _prodBL = p_prod;
    }
    public void Display()
    {
      Console.WriteLine("Please choose the information that you want to modify:");
      Console.WriteLine("[1] - Name: " + _prod.Name);
      Console.WriteLine("[2] - Quantity: " + _prod.Quantity);
      Console.WriteLine("[3] - Price: " + _prod.Price);
      Console.WriteLine("[4] - Description: " + _prod.Desc);
      Console.WriteLine("-----");
      Console.WriteLine("[9] - Save & Go back");
      Console.WriteLine("[0] - Go back");
      Console.WriteLine("What do you want to modify:");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "InventoryMenu";
        case "1":
          Console.WriteLine("Please enter the product name:");
          string _userInputName = Console.ReadLine();

          //Check if the input is empty
          while (_userInputName == "")
          {
            Console.WriteLine("Name should not be empty!");
            Console.WriteLine("Please enter the product name again:");
            _userInputName = Console.ReadLine();
          }
          _prod.Name = _userInputName;
          return "EditProduct";
        case "2":
          Console.WriteLine("Please enter the quantity:");
          string _userInputQuantity = Console.ReadLine();

          //Check if the input is empty
          while (!_userInputQuantity.All(Char.IsDigit))
          {
            Console.WriteLine("Quantity have to be a number and should not be empty!");
            Console.WriteLine("Please enter the quantity again:");
            _userInputQuantity = Console.ReadLine();
          }
          _prod.Quantity = Convert.ToInt32(_userInputQuantity);
          return "EditProduct";
        case "3":
          Console.WriteLine("Please enter the product price:");
          string _userInputPrice = Console.ReadLine();

          //Check if the input is empty
          while (!_userInputPrice.All(Char.IsDigit))
          {
            Console.WriteLine("Price have to be a number and should not be empty!");
            Console.WriteLine("Please enter the price again:");
            _userInputPrice = Console.ReadLine();
          }
          _prod.Price = Convert.ToInt32(_userInputPrice);
          return "EditProduct";
        case "4":
          Console.WriteLine("Please enter the product description:");
          string _userInputDesc = Console.ReadLine();

          //Check if the input is empty
          while (_userInputDesc == "")
          {
            Console.WriteLine("Description should not be empty!");
            Console.WriteLine("Please enter the product description again:");
            _userInputDesc = Console.ReadLine();
          }
          _prod.Desc = _userInputDesc;
          return "EditProduct";
        case "9":
          //Check if all information filled completely
          if (_prod.Name == "" || _prod.Quantity == 0 || _prod.Price == 0 || _prod.Desc == "")
          {
            Console.WriteLine("You need to fill every information above before saving!");
            System.Threading.Thread.Sleep(2000);
            return "EditProduct";
          }
          else
          {
            //Save product to the database after modified
            Log.Information("Save new product information to the database: " + _prod);
            _prodBL.SaveProduct(_prod);
            Log.Information("Saved the product succesfully!");
            Console.WriteLine("Saved the product succesfully!");
            Console.WriteLine("Returning back to the previous menu!");
            System.Threading.Thread.Sleep(2000);

            //Clear the input information after saved and create a new one
            _prod = new Products();
            return "InventoryMenu";
          }
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "EditProduct";
      }
    }
  }
}
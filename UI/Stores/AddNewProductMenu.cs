using BL;
using Model;

namespace UI
{
  public class AddNewProductMenu : IMenu
  {
    private static Products _newProduct = new Products();
    private IProductBL _prodBL;
    public AddNewProductMenu(IProductBL p_prod)
    {
      _prodBL = p_prod;
    }
    public void Display()
    {
      Console.WriteLine("Enter new product for store information");
      Console.WriteLine("[1] - Name: " + _newProduct.Name);
      Console.WriteLine("[2] - Quantity: " + _newProduct.Quantity);
      Console.WriteLine("[3] - Price: " + _newProduct.Price);
      Console.WriteLine("[4] - Description: " + _newProduct.Desc);
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
          _newProduct.Name = _userInputName;
          return "AddNewProduct";
        case "2":
          Console.WriteLine("Please enter the quantity:");
          string _userInputQuantity = Console.ReadLine();

          //Check if the input is empty
          while (!_userInputQuantity.All(Char.IsDigit) || _userInputQuantity == "")
          {
            Console.WriteLine("Quantity have to be a number and should not be empty!");
            Console.WriteLine("Please enter the quantity again:");
            _userInputQuantity = Console.ReadLine();
          }
          _newProduct.Quantity = Convert.ToInt32(_userInputQuantity);
          return "AddNewProduct";
        case "3":
          Console.WriteLine("Please enter the product price:");
          string _userInputPrice = Console.ReadLine();

          //Check if the input is empty
          while (!_userInputPrice.All(Char.IsDigit) || _userInputPrice == "")
          {
            Console.WriteLine("Price have to be a number and should not be empty!");
            Console.WriteLine("Please enter the price again:");
            _userInputPrice = Console.ReadLine();
          }
          _newProduct.Price = Convert.ToInt32(_userInputPrice);
          return "AddNewProduct";
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
          _newProduct.Desc = _userInputDesc;
          return "AddNewProduct";
        case "9":
          //Check if all information filled completely
          if (_newProduct.Name == "" || _newProduct.Quantity == 0 || _newProduct.Price == 0 || _newProduct.Desc == "")
          {
            Console.WriteLine("You need to fill every information above before saving!");
            System.Threading.Thread.Sleep(2000);
            return "AddNewProduct";
          }
          else
          {
            //Add product to the database
            _newProduct.StoreID = ListStoresMenu._currentStoreFront.StoreID;
            _prodBL.AddProduct(_newProduct);
            Console.WriteLine("Added new product succesfully!");
            Console.WriteLine("Returning back to the previous menu!");
            System.Threading.Thread.Sleep(2000);

            //Clear the input information after saved and create a new one
            _newProduct = new Products();
            return "InventoryMenu";
          }
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "AddNewProduct";
      }
    }
  }
}
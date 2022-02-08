using BL;
using Model;

namespace UI
{
  public class AddNewProductMallMenu : IMenu
  {
    private static Products _newProduct = new Products();
    private IProductBL _prodBL;
    public AddNewProductMallMenu(IProductBL p_prod)
    {
      _prodBL = p_prod;
    }
    public void Display()
    {
      Console.WriteLine("Enter new product information");
      Console.WriteLine("[1] - Name: " + _newProduct.Name);
      Console.WriteLine("[2] - Price: " + _newProduct.Price);
      Console.WriteLine("[3] - Description: " + _newProduct.Desc);
      Console.WriteLine("[4] - Minimum Age: " + _newProduct.MinimumAge);
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
          return "MallMenu";
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
          return "AddNewProductMallMenu";
        case "2":
          Console.WriteLine("Please enter the product price:");
          string _userInputPrice = Console.ReadLine();

          //Check if the input is empty
          while (!_userInputPrice.All(Char.IsDigit) || _userInputPrice == "" || Convert.ToInt32(_userInputPrice) == 0)
          {
            Console.WriteLine("Price have to be a number and should not be empty!");
            Console.WriteLine("Please enter the price again:");
            _userInputPrice = Console.ReadLine();
          }
          _newProduct.Price = Convert.ToInt32(_userInputPrice);
          return "AddNewProductMallMenu";
        case "3":
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
          return "AddNewProductMallMenu";
        case "4":
          Console.WriteLine("Please enter the product minimumn age:");
          string _userInputAge = Console.ReadLine();

          //Check if the input is empty
          while (!_userInputAge.All(Char.IsDigit) || _userInputAge == "")
          {
            Console.WriteLine("Minimum Age have to be a number and should not be empty!");
            Console.WriteLine("Please enter the minimum age again:");
            _userInputAge = Console.ReadLine();
          }
          _newProduct.MinimumAge = Convert.ToInt32(_userInputAge);
          return "AddNewProductMallMenu";
        case "9":
          //Check if all information filled completely
          if (_newProduct.Name == "" || _newProduct.Price == 0 || _newProduct.Desc == "")
          {
            Console.WriteLine("You need to fill every information above before saving!");
            System.Threading.Thread.Sleep(2000);
            return "AddNewProductMallMenu";
          }
          else
          {
            try
            {
              //Add product to the database
              Log.Information($"Adding new product: " + _newProduct);
              _prodBL.AddProduct(_newProduct);
              Log.Information("Added new product succesfully");
              Console.WriteLine("Added new product succesfully!");
              Console.WriteLine("Returning back to the previous menu!");
              System.Threading.Thread.Sleep(2000);

              //Clear the input information after saved and create a new one
              _newProduct = new Products();
              return "MallMenu";
            }
            catch (System.Exception exc)
            {
              Log.Warning("Failed to add a new product due to the product existed in the database");
              Console.WriteLine(exc.Message);
              Console.WriteLine("Please press Enter to continue");
              Console.ReadLine();
              return "AddNewProductMallMenu";
            }
          }

        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "AddNewProductMallMenu";
      }
    }
  }
}
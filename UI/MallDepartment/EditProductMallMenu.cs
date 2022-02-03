using BL;
using Model;

namespace UI
{
  public class EditProductMallMenu : IMenu
  {
    private IProductBL _prodBL;
    public EditProductMallMenu(IProductBL p_prod)
    {
      _prodBL = p_prod;
    }
    private Products _prodDetail;
    public void Display()
    {
      _prodDetail = _prodBL.GetProductDetail(ListAllMallProductMenu._selectProductID);
      Console.WriteLine("Information of the product that you want to modify:");
      Console.WriteLine("[1] - Name:        " + _prodDetail.Name);
      Console.WriteLine("[2] - Price:       " + _prodDetail.Price);
      Console.WriteLine("[3] - Description: " + _prodDetail.Desc);
      Console.WriteLine("-----");
      Console.WriteLine("[9] - Save");
      Console.WriteLine("[0] - Go back");
      Console.WriteLine("What do you want to do:");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "ListAllMallProductsMenu";
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
          _prodDetail.Name = _userInputName;
          return "EditProductMallMenu";
        case "2":
          Console.WriteLine("Please enter the product price:");
          string _userInputPrice = Console.ReadLine();

          //Check if the input is empty
          while (!_userInputPrice.All(Char.IsDigit) || _userInputPrice == "" || Convert.ToInt32(_userInputPrice) == 0)
          {
            Console.WriteLine("Price have to be a number, should not be empty and have to greater than 0!");
            Console.WriteLine("Please enter the price again:");
            _userInputPrice = Console.ReadLine();
          }
          _prodDetail.Price = Convert.ToInt32(_userInputPrice);
          return "EditProductMallMenu";
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
          _prodDetail.Desc = _userInputDesc;
          return "EditProductMallMenu";
        case "9":
          //Check if all information filled completely
          if (_prodDetail.Name == "" || _prodDetail.Price == 0 || _prodDetail.Desc == "")
          {
            Console.WriteLine("You need to fill every information above before saving!");
            System.Threading.Thread.Sleep(2000);
            return "EditProductMallMenu";
          }
          else
          {
            //Save product to the database after modified
            Log.Information("Save new product information to the database: " + _prodDetail);
            _prodBL.SaveProduct(_prodDetail);
            Log.Information("Saved the product succesfully!");
            Console.WriteLine("Saved the product succesfully!");
            Console.WriteLine("Returning back to the previous menu!");
            System.Threading.Thread.Sleep(2000);

            //Clear the input information after saved and create a new one
            _prodDetail = new Products();
            return "ListAllMallProductsMenu";
          }
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "EditProductMallMenu";
      }
    }
  }
}
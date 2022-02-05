using BL;
using Model;

namespace UI
{
  public class EditStoreFrontProfileMenu : IMenu
  {
    private IStoreFrontBL _storefBL;
    public EditStoreFrontProfileMenu(IStoreFrontBL p_storef)
    {
      _storefBL = p_storef;
    }
    private static StoreFront _storeFrontInfo = new StoreFront();
    public void Display()
    {
      if (_storeFrontInfo.Name == "")
      {
        _storeFrontInfo = _storefBL.GetStoreFrontInfoByID(ListStoresMenu._currentStoreFront.StoreID);
      }
      Console.WriteLine("Your Profile");
      Console.WriteLine("[1] - Name: " + _storeFrontInfo.Name);
      Console.WriteLine("[2] - Address: " + _storeFrontInfo.Address);
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
          return "MainMenu";
        case "1":
          Console.WriteLine("Please enter store front name:");
          string _userInputName = Console.ReadLine();

          //Check if the input is empty
          while (_userInputName == "")
          {
            Console.WriteLine("Name should not be empty!");
            Console.WriteLine("Please enter store front name again:");
            _userInputName = Console.ReadLine();
          }
          _storeFrontInfo.Name = _userInputName;
          return "EditStoreFrontProfile";
        case "2":
          Console.WriteLine("Please enter the address:");
          string _userInputAddress = Console.ReadLine();

          //Check if the input is empty
          while (_userInputAddress == "")
          {
            Console.WriteLine("Address should not be empty!");
            Console.WriteLine("Please enter store front address:");
            _userInputAddress = Console.ReadLine();
          }
          _storeFrontInfo.Address = _userInputAddress;
          return "EditStoreFrontProfile";
        case "9":
          //Check if all information filled completely
          if (_storeFrontInfo.Name == "" || _storeFrontInfo.Address == "")
          {
            Console.WriteLine("You need to fill every information above before saving!");
            System.Threading.Thread.Sleep(2000);
            return "EditStoreFrontProfile";
          }
          else
          {
            try
            {
              //Save store front to the database
              Log.Information("Saved StoreFront to the database: " + _storeFrontInfo);
              _storefBL.SaveStoreFront(_storeFrontInfo);
              Log.Information("Saved store front succesfully!");
              Console.WriteLine("Saved store front information succesfully!");
              Console.WriteLine("Returning back to the main menu!");
              System.Threading.Thread.Sleep(2000);

              //Clear the input information after saved and create a new one
              ListStoresMenu._currentStoreFront = _storeFrontInfo;
              _storeFrontInfo = new StoreFront();
              return "MainMenu";
            }
            catch (System.Exception exc)
            {
              Log.Warning("Failed to save StoreFront due to the name existed in the database");
              Console.WriteLine(exc.Message);
              Console.WriteLine("Please press Enter to continue");
              Console.ReadLine();
              return "EditStoreFrontProfile";
            }
          }
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "EditStoreFrontProfile";
      }
    }
  }
}
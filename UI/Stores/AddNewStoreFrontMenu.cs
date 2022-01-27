using BL;
using Model;

namespace UI
{
  public class AddNewStoreFrontMenu : IMenu
  {
    private static StoreFront _newStoreFront = new StoreFront();
    private IStoreFrontBL _storefBL;
    public AddNewStoreFrontMenu(IStoreFrontBL p_storef)
    {
      _storefBL = p_storef;
    }
    public void Display()
    {
      Console.WriteLine("Enter new store front information");
      Console.WriteLine("[1] - Name: " + _newStoreFront.Name);
      Console.WriteLine("[2] - Address: " + _newStoreFront.Address);
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
          _newStoreFront.Name = _userInputName;
          return "AddNewStoreFront";
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
          _newStoreFront.Address = _userInputAddress;
          return "AddNewStoreFront";
        case "9":
          //Check if all information filled completely
          if (_newStoreFront.Name == "" || _newStoreFront.Address == "")
          {
            Console.WriteLine("You need to fill every information above before saving!");
            System.Threading.Thread.Sleep(2000);
            return "AddNewStoreFront";
          }
          else
          {
            try
            {
              //Add store front to the database
              Log.Information("Adding a new StoreFront to the database: " + _newStoreFront);
              _storefBL.AddStoreFront(_newStoreFront);
              Log.Information("Added new store front succesfully!");
              Console.WriteLine("Added new store front succesfully!");
              Console.WriteLine("Returning back to the main menu!");
              System.Threading.Thread.Sleep(2000);

              //Clear the input information after saved and create a new one
              _newStoreFront = new StoreFront();
              return "MainMenu";
            }
            catch (System.Exception exc)
            {
              Log.Warning("Failed to add a new StoreFront due to the name existed in the database");
              Console.WriteLine(exc.Message);
              Console.WriteLine("Please press Enter to continue");
              Console.ReadLine();
              return "AddNewStoreFront";
            }
          }
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "AddNewStoreFront";
      }
    }
  }
}
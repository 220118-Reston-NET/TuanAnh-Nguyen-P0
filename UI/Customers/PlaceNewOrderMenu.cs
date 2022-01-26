using BL;
using Model;

namespace UI
{
  public class PlaceNewOrderMenu : IMenu
  {
    private IStoreFrontBL _listStoreFBL;
    public PlaceNewOrderMenu(IStoreFrontBL p_listStoreFBL)
    {
      _listStoreFBL = p_listStoreFBL;
    }
    private List<StoreFront> _listStores;
    public static StoreFront _selectedStore = new StoreFront();
    public void DisplayAllStore()
    {
      _listStores = _listStoreFBL.GetALlStoreFronts();
      if (_listStores.Count() > 0)
      {
        Console.WriteLine("Here are all stores in shopping mall:");
        for (int i = 0; i < _listStores.Count(); i++)
        {
          Console.WriteLine("- " + _listStores[i].Name + " (" + _listStores[i].Address + ")");
        }
        Console.WriteLine("-----");
        Console.WriteLine("Please enter the store name that you want to start shopping. Ex: '" + _listStores[0].Name + "'");
      }
      else
      {
        Console.WriteLine("All Stores are closed!");
      }
    }
    public void Display()
    {
      DisplayAllStore();
      Console.WriteLine("[0] - Go back");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "MainMenu";

        default:
          //Check if the input is existed in Name of the StoreFront
          for (int i = 0; i < _listStores.Count(); i++)
          {
            if (_userInput == _listStores[i].Name)
            {
              _selectedStore = _listStores[i];
              // Console.WriteLine("Start shopping at " + _selectedStore.Name);

              return "ListOrderableProdMenu";
            }
          }
          Console.WriteLine("Please input a valid response!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "PlaceNewOrderMenu";
      }
    }
  }
}
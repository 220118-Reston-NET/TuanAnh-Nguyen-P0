using BL;
using Model;

namespace UI
{
  public class ListStoreOrdersMenu : IMenu
  {
    private IOrderBL _listOrderBL;
    public ListStoreOrdersMenu(IOrderBL p_listOrderBL)
    {
      _listOrderBL = p_listOrderBL;
    }
    public void DisplayAllOrders()
    {
      _listOrders = _listOrderBL.GetAllOrdersByStoreID(ListStoresMenu._currentStoreFront.StoreID);
      if (_listOrders.Count() > 0)
      {
        Console.WriteLine("Here are all orders:");
        for (int i = 0; i < _listOrders.Count(); i++)
        {
          Console.WriteLine($"---Order {i + 1} on {_listOrders[i].OrderDate}-----------------------");
          for (int j = 0; j < _listOrders[i].ListLineItems.Count(); j++)
          {
            Console.WriteLine($"{j + 1}. {_listOrders[i].ListLineItems[j].ProductName} - {_listOrders[i].ListLineItems[j].Quantity}");
          }
          Console.WriteLine($"Total Price: ${_listOrders[i].TotalPrice}");
        }
        Console.WriteLine("-----------");
      }
      else
      {
        Console.WriteLine("Your store didn't receive any orders yet!");
      }
    }
    private List<Orders> _listOrders;
    public void Display()
    {
      //List of All Orders
      DisplayAllOrders();
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
          Console.WriteLine("Please input a valid response!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "ListCustomerOrdersMenu";
      }
    }
  }
}
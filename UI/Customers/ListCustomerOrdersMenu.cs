using BL;
using Model;

namespace UI
{
  public class ListCustomerOrdersMenu : IMenu
  {
    private IOrderBL _listOrderBL;
    public ListCustomerOrdersMenu(IOrderBL p_listOrderBL)
    {
      _listOrderBL = p_listOrderBL;
    }
    private List<Orders> _listOrders;
    public void DisplayAllOrders()
    {
      _listOrders = _listOrderBL.GetAllOrdersByCustomerID(ListCustomersMenu._currentCustomer.CustomerID);
      if (_listOrders.Count() > 0)
      {
        Console.WriteLine("Here are your order history:");
        for (int i = 0; i < _listOrders.Count(); i++)
        {
          Console.WriteLine($"---Order {i + 1} on {_listOrders[i].createdAt}-----------------------");
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
        Console.WriteLine("You didn't have any orders yet! Please try to start shopping first!");
      }
    }
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
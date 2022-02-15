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
    private static string _filter = "";
    public void DisplayAllOrders()
    {
      if (_filter == "")
      {
        _listOrders = _listOrderBL.GetAllOrdersByCustomerID(ListCustomersMenu._currentCustomer.CustomerID);
      }
      else
      {
        _listOrders = _listOrderBL.GetAllOrdersByCustomerIDWithFilter(ListCustomersMenu._currentCustomer.CustomerID, _filter);
        if (_listOrders.Count() == 0)
        {
          _filter = "";
          Console.WriteLine("None of orders have this filter");
          Console.WriteLine("Please press Enter to return back to ");
          Console.ReadLine();
          Console.Clear();
          _listOrders = _listOrderBL.GetAllOrdersByCustomerID(ListCustomersMenu._currentCustomer.CustomerID);
        }
      }

      if (_listOrders.Count() > 0)
      {
        Console.WriteLine("Here are your order history:");
        for (int i = 0; i < _listOrders.Count(); i++)
        {
          Console.WriteLine($"---Order {i + 1} on {_listOrders[i].createdAt}-----------------------");
          for (int j = 0; j < _listOrders[i].ListLineItems.Count(); j++)
          {
            Console.WriteLine($"{j + 1}. {_listOrders[i].ListLineItems[j].ProductName} - ${_listOrders[i].ListLineItems[j].PriceAtCheckedOut}({_listOrders[i].ListLineItems[j].Quantity})");
          }
          Console.WriteLine($"Total Price: ${_listOrders[i].TotalPrice}");
          Console.WriteLine("Order Status: " + _listOrders[i].Status);
          if (_listOrders[i].ListTrackings.Count() > 0)
          {
            for (int k = 0; k < _listOrders[i].ListTrackings.Count(); k++)
            {
              Console.WriteLine($"Tracking Number {k + 1}: {_listOrders[i].ListTrackings[k].TrackingNumber}");
            }
          }
        }
        Console.WriteLine("-----------");
        Console.WriteLine("You can use these keywords to filter the Order Status:");
        Console.WriteLine("['All', 'Order Placed', 'Shipped', 'Delivered', 'Cancelled']");
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
          _filter = "";
          return "MainMenu";
        case "All":
          _filter = "";
          Log.Information("Changed filter to All");
          return "ListCustomerOrdersMenu";
        case "Order Placed":
          _filter = "Order Placed";
          Log.Information("Changed filter to Order Placed");
          return "ListCustomerOrdersMenu";
        case "Shipped":
          _filter = "Shipped";
          Log.Information("Changed filter to Shipped");
          return "ListCustomerOrdersMenu";
        case "Delivered":
          _filter = "Delivered";
          Log.Information("Changed filter to Delivered");
          return "ListCustomerOrdersMenu";
        case "Cancelled":
          _filter = "Cancelled";
          Log.Information("Changed filter to Cancalled");
          return "ListCustomerOrdersMenu";

        default:
          Console.WriteLine("Please input a valid response!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "ListCustomerOrdersMenu";
      }
    }
  }
}
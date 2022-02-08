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
    private List<Orders> _listOrders;
    private static string _filter = "";
    public static string _selectOrderID = "";
    public void DisplayAllOrders()
    {
      if (_filter == "")
      {
        _listOrders = _listOrderBL.GetAllOrdersByStoreID(ListStoresMenu._currentStoreFront.StoreID);
      }
      else
      {
        _listOrders = _listOrderBL.GetAllOrdersByStoreIDWithFilter(ListStoresMenu._currentStoreFront.StoreID, _filter);
        if (_listOrders.Count() == 0)
        {
          _filter = "";
          Console.WriteLine("None of orders have this filter");
          Console.WriteLine("Please press Enter to return back to ");
          Console.ReadLine();
          Console.Clear();
          _listOrders = _listOrderBL.GetAllOrdersByStoreID(ListStoresMenu._currentStoreFront.StoreID);
        }
      }

      if (_listOrders.Count() > 0)
      {
        Console.WriteLine("Here are all orders:");
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
        Console.WriteLine("Or enter the order number that you want to look at");
      }
      else
      {
        Console.WriteLine("Your store didn't receive any orders yet!");
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
          return "ListStoreOrdersMenu";
        case "Order Placed":
          _filter = "Order Placed";
          return "ListStoreOrdersMenu";
        case "Shipped":
          _filter = "Shipped";
          return "ListStoreOrdersMenu";
        case "Delivered":
          _filter = "Delivered";
          return "ListStoreOrdersMenu";
        case "Cancelled":
          _filter = "Cancelled";
          return "ListStoreOrdersMenu";

        default:
          if (_listOrders.Count() > 0)
          {
            if (_userInput == "")
            {
              Console.WriteLine("Please input a valid response!");
              Console.WriteLine("Please press Enter to continue");
              Console.ReadLine();
              return "ListStoreOrdersMenu";
            }
            else if (_userInput.All(Char.IsDigit) && Convert.ToInt32(_userInput) <= _listOrders.Count())
            {
              _selectOrderID = _listOrders[Convert.ToInt32(_userInput) - 1].OrderID;
              _filter = "";
              return "OrderDetailMenu";
            }
          }

          Console.WriteLine("Please input a valid response!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "ListStoreOrdersMenu";
      }
    }
  }
}
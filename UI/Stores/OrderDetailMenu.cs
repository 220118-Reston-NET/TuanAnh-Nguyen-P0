using BL;
using Model;

namespace UI
{
  public class OrderDetailMenu : IMenu
  {
    private IOrderBL _orderBL;
    private ICustomerBL _cusBL;
    public OrderDetailMenu(IOrderBL p_orderBL, ICustomerBL p_cusBL)
    {
      _orderBL = p_orderBL;
      _cusBL = p_cusBL;
    }
    Orders _orderDetail;
    Customer _customerInfo;
    public void Display()
    {
      _orderDetail = _orderBL.GetOrderByOrderID(ListStoreOrdersMenu._selectOrderID);
      _customerInfo = _cusBL.GetCustomerInfoByID(_orderDetail.CustomerID);
      Console.WriteLine("Here are all the information about the order you selected:");
      Console.WriteLine($"This order was placed on {_orderDetail.createdAt}");
      for (int i = 0; i < _orderDetail.ListLineItems.Count(); i++)
      {
        Console.WriteLine($"{i + 1}. {_orderDetail.ListLineItems[i].ProductName} - {_orderDetail.ListLineItems[i].Quantity}");
      }
      Console.WriteLine("----------------------");
      Console.WriteLine("Customer Information:");
      Console.WriteLine($"Name: {_customerInfo.Name}\nAddress: {_customerInfo.Address}\nEmail: {_customerInfo.Email}\nPhone Number: {_customerInfo.PhoneNumber}");
      Console.WriteLine("----------------------");
      Console.WriteLine($"Order Status: {_orderDetail.Status}");
      if (_orderDetail.ListTrackings.Count() > 0)
      {
        for (int j = 0; j < _orderDetail.ListTrackings.Count(); j++)
        {
          Console.WriteLine($"Tracking Number {j + 1}: {_orderDetail.ListTrackings[j].TrackingNumber}");
        }
        Console.WriteLine("----------------------");
        Console.WriteLine("What do you want with this order:");
        Console.WriteLine("[1] - Add More Tracking Number To This Order");
      }
      else
      {
        Console.WriteLine("----------------------");
        Console.WriteLine("What do you want with this order:");
        Console.WriteLine("[1] - Fullfill and Ship");
      }
      Console.WriteLine("[2] - Cancel Order");
      Console.WriteLine("[0] - Go back");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "ListStoreOrdersMenu";

        case "1":
          Console.WriteLine("Please provide the tracking number:");
          string _userInputTracking = Console.ReadLine();

          while (_userInputTracking == "")
          {
            Console.WriteLine("Tracking number should not be empty! Please try again:");
            _userInputTracking = Console.ReadLine();
          }
          Log.Information($"Added new tracking number: '_userInputTracking' to the order: {ListStoreOrdersMenu._selectOrderID}");
          _orderBL.AddNewTrackingNumber(ListStoreOrdersMenu._selectOrderID, _userInputTracking);
          _orderBL.UpdateOrderDetail(ListStoreOrdersMenu._selectOrderID, "Shipped");
          return "OrderDetailMenu";

        case "2":
          _orderBL.UpdateOrderDetail(ListStoreOrdersMenu._selectOrderID, "Cancelled");
          Log.Information("Cancelled order: " + ListStoreOrdersMenu._selectOrderID);
          Console.WriteLine("Order cancelled successfully!");
          Console.WriteLine("Requested to return all the shipment(if shipped) back successfully!");
          _orderBL.RemoveAllTrackingByOrderID(ListStoreOrdersMenu._selectOrderID);
          Console.WriteLine("Please press Enter to return back to the previous menu");
          Console.ReadLine();

          return "ListStoreOrdersMenu";

        default:
          Console.WriteLine("Please input a valid response!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "OrderDetailMenu";
      }
    }
  }
}
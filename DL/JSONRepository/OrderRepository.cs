using System.Text.Json;
using Model;

namespace DL
{
  public class OrderRepository : IOrderRepository
  {
    private string _filepath = "../DL/Database/";
    private string _jsonString;

    public List<Orders> GetAllOrders()
    {
      string _path = _filepath + "Orders.json";
      List<Orders> _listOrders = new List<Orders>();
      //Check if the JSON file is exists.
      if (File.Exists(_path))
      {
        //Check if the file have values
        if (new FileInfo(_path).Length == 0)
        {
          return _listOrders;
        }
        else
        {
          string _jsonString2 = File.ReadAllText(_path);

          _listOrders = JsonSerializer.Deserialize<List<Orders>>(_jsonString2);
        }
      }
      else
      {
        return _listOrders;
      }
      return _listOrders;
    }

    public Orders PlaceOrder(List<LineItems> p_lineItems, string _storeID, string _customerID, int _totalPrice)
    {
      string _path = _filepath + "Orders.json";
      List<Orders> _listOrders = new List<Orders>();
      Orders _newOrder = new Orders();

      //Get a random ID for Order
      _newOrder.OrderID = Guid.NewGuid().ToString();

      //Get Date Time when place order
      DateTime _createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
      _newOrder.createdAt = _createdAt;

      _newOrder.CustomerID = _customerID;
      _newOrder.StoreID = _storeID;
      _newOrder.ListLineItems = p_lineItems;

      // int _totalPrice = 0;
      // InventoryRepository _subtract = new InventoryRepository();
      // for (int i = 0; i < p_lineItems.Count(); i++)
      // {
      //   _totalPrice += p_lineItems[i].Price * p_lineItems[i].Quantity;
      //   _subtract.SubtractProduct(p_lineItems[i].ProductID, _storeID, p_lineItems[i].Quantity);
      // }
      _newOrder.TotalPrice = _totalPrice;

      //Check if the JSON file is exists.
      if (File.Exists(_path))
      {
        //Check if the file have values
        if (new FileInfo(_path).Length == 0)
        {
          _listOrders.Add(_newOrder);
          _jsonString = JsonSerializer.Serialize(_listOrders, new JsonSerializerOptions { WriteIndented = true });

          File.WriteAllText(_path, _jsonString);
        }
        else
        {
          string _jsonString2 = File.ReadAllText(_path);

          _listOrders = JsonSerializer.Deserialize<List<Orders>>(_jsonString2);
          _listOrders.Add(_newOrder);

          _jsonString = JsonSerializer.Serialize(_listOrders, new JsonSerializerOptions { WriteIndented = true });
          File.WriteAllText(_path, _jsonString);
        }
      }
      else
      {
        _listOrders.Add(_newOrder);
        _jsonString = JsonSerializer.Serialize(_listOrders, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(_path, _jsonString);
      }

      return _newOrder;
    }
  }
}